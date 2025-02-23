using Exiled.API.Features;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    // This somehow worked first time
    // Also I could probably optimize it but it will probably break
    public class FakeDeath
    {
        public static void kill(Player player)
        {
            Player SCP = RandomPlayer.GetRandomPlayer(Team.SCPs);
            player.ShowHint($"<color=#0D98BA>You have a good feeling</color>");
            SCP.ShowHint($"You have a strange feeling");
            string nameToShow = " ";
            string nameToSay = " ";
            if (SCP.Role.Type == RoleTypeId.Scp049)
            {
                nameToShow = "<color=red>SCP-049</color>";
                nameToSay = "SCP 0 4 9";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp079)
            {
                nameToShow = "<color=red>SCP-079</color>";
                nameToSay = "SCP 0 7 9";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp096)
            {
                nameToShow = "<color=red>SCP-096</color>";
                nameToSay = "SCP 0 9 6";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp106)
            {
                nameToShow = "<color=red>SCP-106</color>";
                nameToSay = "SCP 1 0 6";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp173)
            {
                nameToShow = "<color=red>SCP-173</color>";
                nameToSay = "SCP 1 7 3";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp939)
            {
                nameToShow = "<color=red>SCP-939</color>";
                nameToSay = "SCP 9 3 9";
            }
            else if (SCP.Role.Type == RoleTypeId.Scp3114)
            {
                nameToShow = "<color=red>SCP-3114</color>";
                nameToSay = "SCP 3 1 1 4";
            }
            else
            {
                nameToShow = "<color=orange>Go bother coollogan876 on Discord to fix this</color>";
                nameToSay = "some SCP";
            }
            Timing.CallDelayed(5, () =>
            {
                Cassie.MessageTranslated($"{nameToSay} successfully terminated by Automatic Security System",
                    $"{nameToShow} <color=green>successfully <b>terminated</b></color> by Automatic Security System.", false, true, true);
            });
        }
    }
}
