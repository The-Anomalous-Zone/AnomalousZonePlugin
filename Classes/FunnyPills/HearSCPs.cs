using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using UnityEngine;
using MEC;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    // This stopped working
    public class HearSCPs
    {
        public bool Hear;
        private Player player;
        private static Dictionary<Player, HearSCPs> players = new Dictionary<Player, HearSCPs>();
        public HearSCPs(Player player)
        {
            this.player = player;
            players.Add(player, this);
        }
        public void Talk()
        {
            var SCPs = Player.List.Where(scp => scp.IsScp).ToList();
            var scp = SCPs.Random();
            player.ShowHint($"<color=#0d98ba>You can talk to your favorite </color><color=#F00>{Plugin.Instance.Config.SCPNicknames[scp.Role.Type].Random()}</color<color=#0d98ba>!</color>");

            Hear = true;
            player.VoiceChannel = VoiceChat.VoiceChatChannel.ScpChat;
            Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.SCPChatMinTime, Plugin.Instance.Config.SCPChatMaxTime), ()=>
            {
                Hear = false;
                if (!player.IsAlive)
                {
                    player.VoiceChannel = VoiceChat.VoiceChatChannel.Spectator;
                    return;
                }
                if (player.VoiceChannel == VoiceChat.VoiceChatChannel.ScpChat)
                {
                    player.ShowHint($"The SCPs didn't enjoy talking to you and are coming to murder you");
                    player.VoiceChannel = VoiceChat.VoiceChatChannel.Proximity;
                }
            });
        }

        public static HearSCPs Get(Player player)
        {
            return players[player];
        }
    }
}