using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features.Serializable;
using MapEditorReborn.Commands.ModifyingCommands.Scale;
using SCP294.Types;
using SCP294.Utils;
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

namespace SCP294.Classes
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
                        if (!SCP294.Instance.PlayersNear294.Contains(player.UserId))
                        {
                            SchematicObject scp294 = GetClosest294(player);
                            if (SCP294.Instance.SCP294UsesLeft.Keys.Contains(scp294) && SCP294.Instance.SCP294UsesLeft[scp294] == 0)
                            {
                                player.ShowHint("<color=#0d98ba><size=300>\n</size>\n<size=35>You Approach SCP-294.</size>\n<size=30>It seems to have lost all power, rendering it unusable for now...</size></color>", 3);
                            }
                            else
                            {
                                player.ShowHint("<color=#0d98ba><size=300>\n</size>\n<size=35>You Approach SCP-294.</size>\n<size=30>If you had a coin, you could buy a drink...</size>\n<size=20>(Hold a Coin, Open Console, Use the command '.scp294 <drink>' to dispense your drink of choice)</size></color>", 3);
                            }
                            SCP294.Instance.PlayersNear294.Add(player.UserId);
                        }
                    }
                    else
                    {
                        if (SCP294.Instance.PlayersNear294.Contains(player.UserId))
                        {
                            SCP294.Instance.PlayersNear294.Remove(player.UserId);
                        }
                    }
                }
            }
        }
        public static void SpawnSCP294()
        {
            SCP294.Instance.spawned = new Dictionary<SchematicObject, bool>();

            for (int i = 0; i < SCP294.Instance.Config.SpawningLocations.SpawnAmount; i++)
            {
                Room SpawnRoom = RoomHandler.GetRandomRoom(SCP294.Instance.Config.SpawningLocations.SpawnRooms.Keys.ToList());
                if (!SpawnRoom) continue;
                SpawnTransform relativeOffsetTransform = SCP294.Instance.Config.SpawningLocations.SpawnRooms[SpawnRoom.Type].RandomItem();

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
            SCP294.Instance.lightSource.Add(scp294, ObjectSpawner.SpawnLightSource(new LightSourceSerializable()
            {
                Color = "#FFF",
                Intensity = 0.25f,
                Shadows = true,
                Range = 1
            }, lightPos));
        
            SCP294.Instance.spawned.Add(scp294, false);
            SCP294.Instance.SCP294UsesLeft.Add(scp294, SCP294.Instance.Config.MaxUsesPerMachine);
        }
        public static void RemoveSCP294(SchematicObject scp294)
        {
            if (scp294 != null && SCP294.Instance.SCP294UsesLeft.Keys.Contains(scp294))
            {
                SCP294.Instance.spawned.Remove(scp294);
                SCP294.Instance.SCP294UsesLeft.Remove(scp294);
                try
                {
                    if (SCP294.Instance.lightSource.Keys.Contains(scp294))
                    {
                        SCP294.Instance.lightSource[scp294].Destroy();
                        SCP294.Instance.lightSource.Remove(scp294);
                    }
                }
                catch (Exception err) { }
                scp294.Destroy();
            }
        }
        public static void SetSCP294Uses(SchematicObject scp294, int useCount)
        {
            if (scp294 != null && SCP294.Instance.SCP294UsesLeft.Keys.Contains(scp294))
            {
                SCP294.Instance.SCP294UsesLeft[scp294] = useCount;
                // Disable and Enable
                SCP294.Instance.lightSource[scp294].Light.Range = useCount == 0 ? 0 : 1;
                SCP294.Instance.lightSource[scp294].Light.Intensity = useCount == 0 ? 0 : 0.25f;
            }
        }
        public static void PlayDispensingSound(Player player, DrinkSound soundType)
        {
            SchematicObject scp294 = GetClosest294(player);

            SoundHandler.PlayAudio(new DrinkSoundFiles().List[(int)soundType], 50, false, "SCP-294", scp294.Position + new Vector3(0, 1, 0), 6f);
        }
        public static bool CanPlayerUse294(Player player)
        {
            foreach (SchematicObject scp294 in SCP294.Instance.spawned.Keys)
            {
                if (!scp294) continue;
                if (Vector3.Distance(player.Position, scp294.Position) < SCP294.Instance.Config.UseDistance) return true;
            }
            return false;
        }
        public static SchematicObject GetClosest294(Player player)
        {
            float closestDistance = 99999f;
            SchematicObject closestObject = null;
            foreach (SchematicObject scp294 in SCP294.Instance.spawned.Keys)
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
