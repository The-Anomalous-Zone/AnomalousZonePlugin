using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using UnityEngine;
using MEC;
using VoiceChat.Networking;
using VoiceChat;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    // This stopped working
    public class HearSCPs
    {
        public static void hearSCPs(Player player)
        {
            var SCPs = Player.List.Where(scp => scp.IsScp).ToList();
            var scp = SCPs.Random();
            player.ShowHint($"<color=#0d98ba>You can talk to your favorite </color><color=#F00>{Plugin.Instance.Config.SCPNicknames[scp.Role.Type].Random()}</color<color=#0d98ba>!</color>");

            Plugin.Instance.channels.Add(player, VoiceChat.VoiceChatChannel.ScpChat);
            Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.SCPChatMinTime, Plugin.Instance.Config.SCPChatMaxTime), () =>
            {
                Plugin.Instance.channels.Remove(player);
                if (!player.IsAlive)
                {
                    return;
                }
                player.ShowHint($"The SCPs didn't enjoy talking to you and are coming to murder you");
                                 // Good 👍
            });

        }       
    }
}