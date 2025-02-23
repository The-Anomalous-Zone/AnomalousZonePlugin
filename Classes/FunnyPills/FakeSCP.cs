using Exiled.API.Extensions;
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
    public class FakeSCP
    {
        public static void Disguise(Player player)
        {
            RoleTypeId SCP = Plugin.Instance.Config.SCPNicknames.Keys.ToList().Random();
            player.ChangeAppearance(SCP);
            string name = Plugin.Instance.Config.SCPNicknames[SCP].Random();
            string a = $"a {name}";
            if (name.StartsWith("a") || name.StartsWith("e") || name.StartsWith("u") || name.StartsWith("o") || name.StartsWith("i"))
            {
                a = $"an {name}";
            }
            player.ShowHint($"<#color=#0d98ba>You've been disguised as</color><color=#F00> {a}</color><color=#0d98ba>!</color>");
            Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.PillsDisguiseMinTime, Plugin.Instance.Config.PillsDisguiseMaxTime), () =>
            {
                if (!player.IsAlive)
                {
                    return;
                }
                player.ShowHint($"<color=#0d98ba>You're no longer disguised</color>");
                player.ChangeAppearance(player.Role.Type);

            });
        }
    }
}
