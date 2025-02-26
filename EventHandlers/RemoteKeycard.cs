using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using AnomalousZonePlugin.Classes.RemoteKeycard;
using System;
using UnityEngine;

namespace AnomalousZonePlugin.EventHandlers
{
    public class RemoteKeycard
    {
        private Plugin plugin;
        public RemoteKeycard(Plugin plugin) => this.plugin = plugin;
        public void OnDoorInteract(InteractingDoorEventArgs ev)
        {
            if (UnityEngine.Random.value < .4 && DateTime.Now.Date == Plugin.Instance.date)
            {
                // Just annoy the player
                ev.IsAllowed = false;
                return;
            }
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission((KeycardPermissions)ev.Door.RequiredPermissions.RequiredPermissions) && !ev.Door.IsLocked)
                ev.IsAllowed = true;
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission(KeycardPermissions.Checkpoints) && ev.Door.DoorLockType == DoorLockType.None)
            {
                if (ev.Door.IsCheckpoint || ev.Door.IsPartOfCheckpoint)
                {
                    ev.IsAllowed = true;
                }
                if (ev.Player.Zone.ToString() == "HeavyContainment, Entrance")
                {
                    ev.IsAllowed = true;
                }
            }
        }

        public void OnWarheadUnlock(ActivatingWarheadPanelEventArgs ev)
        {
            if (UnityEngine.Random.value < .4 && DateTime.Now.Date == Plugin.Instance.date)
            {
                // Again annoy the player
                ev.IsAllowed = false;
                return;
            }
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission((KeycardPermissions)KeycardPermissions.AlphaWarhead))
                ev.IsAllowed = true;
        }
        public void OnGeneratorUnlock(UnlockingGeneratorEventArgs ev)
        {
            if (UnityEngine.Random.value < .4 && DateTime.Now.Date == Plugin.Instance.date)
            {
                // Just keep annoying the player
                ev.IsAllowed = false;
                return;
            }

            if (!ev.IsAllowed && (ev.Player.HasItem(ItemType.KeycardChaosInsurgency) || ev.Player.HasItem(ItemType.KeycardMTFCaptain) || ev.Player.HasItem(ItemType.KeycardMTFOperative) || ev.Player.HasItem(ItemType.KeycardMTFPrivate) || ev.Player.HasItem(ItemType.KeycardO5)))
                ev.IsAllowed = true;

            
        }

        public void OnLockerInteract(InteractingLockerEventArgs ev)
        {
            if (UnityEngine.Random.value < .4 && DateTime.Now.Date == Plugin.Instance.date)
            {
                // Just drive the player insane
                ev.IsAllowed = false;
                return;
            }

            if (!ev.IsAllowed && ev.Chamber != null &&
                (ev.Player.HasKeycardPermission((Exiled.API.Enums.KeycardPermissions)ev.Chamber.RequiredPermissions) &&
                ev.Player.HasKeycardPermission(Exiled.API.Enums.KeycardPermissions.Checkpoints)))

                    ev.IsAllowed = true;
        }
    }
}