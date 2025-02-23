using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features.Serializable;
using MapEditorReborn.Commands.ModifyingCommands.Scale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using UnityEngine;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Toys;
using InventorySystem.Items.Pickups;
using InventorySystem.Items;
using Exiled.API.Features.Items;
using MapEditorReborn.Commands.ModifyingCommands.Position;
using MapEditorReborn.Commands.ModifyingCommands.Rotation;
using AnomalousZonePlugin.Classes.SCP294;
using AnomalousZonePlugin.Configs.SCP294;
using AnomalousZonePlugin.EventHandlers.SCP294Handlers;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Classes.SCP294
{
    public class SCP294Object
    {
        public static IEnumerator<float> Handle294Hint()
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(0.1f);

                foreach (Player player in Player.List)
                {
                    if (player == null) continue;
                    if (player.IsNPC) continue;
                    if (CanPlayerUse294(player))
                    {
                        if (!Plugin.Instance.PlayersNear294.Contains(player.UserId))
                        {
                            SchematicObject scp294 = GetClosest294(player);
                            if (Plugin.Instance.SCP294UsesLeft.Keys.Contains(scp294) && Plugin.Instance.SCP294UsesLeft[scp294] == 0)
                            {
                                player.ShowHint("<color=#0d98ba><size=300>\n</size>\n<size=35>You Approach SCP-294.</size>\n<size=30>It seems to have lost all power, rendering it unusable for now...</size></color>", 3);
                            }
                            else
                            {
                                player.ShowHint("<color=#0d98ba><size=300>\n</size>\n<size=35>You Approach SCP-294.</size>\n<size=30>If you had a coin, you could buy a drink...</size>\n<size=20>(Hold a Coin, Open Console, Use the command '.scp294 <drink>' to dispense your drink of choice)</size></color>", 3);
                            }
                            Plugin.Instance.PlayersNear294.Add(player.UserId);
                        }
                    }
                    else
                    {
                        if (Plugin.Instance.PlayersNear294.Contains(player.UserId))
                        {
                            Plugin.Instance.PlayersNear294.Remove(player.UserId);
                        }
                    }
                }
            }
        }
        public static void SpawnSCP294()
        {
            Plugin.Instance.spawned = new Dictionary<SchematicObject, bool>();

            for (int i = 0; i < Plugin.Instance.Config.SpawningLocations.SpawnAmount; i++)
            {
                Room SpawnRoom = RoomHandler.GetRandomRoom(Plugin.Instance.Config.SpawningLocations.SpawnRooms.Keys.ToList());
                if (!SpawnRoom) continue;
                SpawnTransform relativeOffsetTransform = Plugin.Instance.Config.SpawningLocations.SpawnRooms[SpawnRoom.Type].RandomItem();

                CreateSCP294(SpawnRoom.Position + (SpawnRoom.Rotation * relativeOffsetTransform.Position), Quaternion.Euler(SpawnRoom.Rotation.eulerAngles + Quaternion.Euler(relativeOffsetTransform.Rotation.x, relativeOffsetTransform.Rotation.y, relativeOffsetTransform.Rotation.z).eulerAngles), relativeOffsetTransform.Scale);
            }
        }
        public static void CreateSCP294(Vector3 Position, Quaternion Rotation, Vector3 Scale)
        {
            SchematicObject scp294 = ObjectSpawner.SpawnSchematic("scp294", Vector3.zero, Quaternion.identity, Vector3.one);
            scp294.Position = Position;
            scp294.Rotation = Rotation;
            scp294.Scale = Scale;

            Vector3 lightPos = scp294.Position;
            lightPos += scp294.Rotation * new Vector3(0f, 1.25f, -1.25f);
            Plugin.Instance.lightSource.Add(scp294, ObjectSpawner.SpawnLightSource(new LightSourceSerializable()
            {
                Color = "#FFF",
                Intensity = 0.25f,
                Shadows = true,
                Range = 1
            }, lightPos));

            Plugin.Instance.spawned.Add(scp294, false);
            Plugin.Instance.SCP294UsesLeft.Add(scp294, Plugin.Instance.Config.MaxUsesPerMachine);
        }
        public static void RemoveSCP294(SchematicObject scp294)
        {
            if (scp294 != null && Plugin.Instance.SCP294UsesLeft.Keys.Contains(scp294))
            {
                Plugin.Instance.spawned.Remove(scp294);
                Plugin.Instance.SCP294UsesLeft.Remove(scp294);
                try
                {
                    if (Plugin.Instance.lightSource.Keys.Contains(scp294))
                    {
                        Plugin.Instance.lightSource[scp294].Destroy();
                        Plugin.Instance.lightSource.Remove(scp294);
                    }
                }
                catch (Exception err) { }
                scp294.Destroy();
            }
        }
        public static void SetSCP294Uses(SchematicObject scp294, int useCount)
        {
            if (scp294 != null && Plugin.Instance.SCP294UsesLeft.Keys.Contains(scp294))
            {
                Plugin.Instance.SCP294UsesLeft[scp294] = useCount;
                // Disable and Enable
                Plugin.Instance.lightSource[scp294].Light.Range = useCount == 0 ? 0 : 1;
                Plugin.Instance.lightSource[scp294].Light.Intensity = useCount == 0 ? 0 : 0.25f;
            }
        }
        public static void PlayDispensingSound(Player player, DrinkSound soundType)
        {
            SchematicObject scp294 = GetClosest294(player);

            SoundHandler.PlayAudio(new DrinkSoundFiles().List[(int)soundType], 50, false, "SCP-294", scp294.Position + new Vector3(0, 1, 0), 6f);
        }
        public static bool CanPlayerUse294(Player player)
        {
            foreach (SchematicObject scp294 in Plugin.Instance.spawned.Keys)
            {
                if (!scp294) continue;
                if (Vector3.Distance(player.Position, scp294.Position) < Plugin.Instance.Config.UseDistance) return true;
            }
            return false;
        }
        public static SchematicObject GetClosest294(Player player)
        {
            float closestDistance = 99999f;
            SchematicObject closestObject = null;
            foreach (SchematicObject scp294 in Plugin.Instance.spawned.Keys)
            {
                if (!scp294) continue;
                if (Vector3.Distance(player.Position, scp294.Position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(player.Position, scp294.Position);
                    closestObject = scp294;
                };
            }
            return closestObject;
        }
        public static Pickup CreateDrinkPickup(Item item, Vector3 position, Quaternion rotation = default(Quaternion), bool spawn = true)
        {
            ItemPickupBase itemPickupBase = UnityEngine.Object.Instantiate(item.Base.PickupDropModel, position, rotation);
            itemPickupBase.Info = new PickupSyncInfo(item.Type, item.Weight, item.Serial);
            itemPickupBase.gameObject.transform.localScale = item.Scale;
            Pickup pickup = Pickup.Get(itemPickupBase);
            if (spawn)
            {
                pickup.Spawn();
            }

            return pickup;
        }
    }
}