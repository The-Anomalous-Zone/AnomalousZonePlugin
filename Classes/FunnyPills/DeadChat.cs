using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class DeadChat
    {
        public static void Talk(Player player)
        {
            Timing.CallDelayed(3.5f, () =>
            {
                player.ShowHint($"<color=#od98ba>You can hear the voices of the dead!</color>");
                                            // no you can't, you're just crazy until I fix this
                player.VoiceChannel = VoiceChat.VoiceChatChannel.Scp1576;

                Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.DeadChatMinTime, Plugin.Instance.Config.DeadChatMaxTime), () =>
                {
                    if (!player.IsAlive)
                    {
                        player.VoiceChannel = VoiceChat.VoiceChatChannel.Spectator;
                        return;
                    }

                    if (player.VoiceChannel == VoiceChat.VoiceChatChannel.Scp1576)
                    {
                        player.VoiceChannel = VoiceChat.VoiceChatChannel.Proximity;
                        player.ShowHint($"<color=#od98ba>You are no longer being haunted by the dead</color>");
                    }
                });
            });
        }
    }
}
