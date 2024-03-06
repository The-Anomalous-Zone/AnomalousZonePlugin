using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using AnomalousZonePlugin.Classes.RemoteKeycard;

namespace AnomalousZonePlugin.EventHandlers
{
    public class RemoteKeycard
    {
        private Plugin plugin;
        public RemoteKeycard(Plugin plugin) => this.plugin = plugin;
        public void OnDoorInteract(InteractingDoorEventArgs ev)
        {
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
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission((KeycardPermissions)KeycardPermissions.AlphaWarhead))
                ev.IsAllowed = true;
        }
    }
}
