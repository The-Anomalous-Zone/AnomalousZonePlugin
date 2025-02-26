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
                player.ShowHint($"<color=#0d98ba>You can hear the voices of the dead!</color>");
                Plugin.Instance.channels.Add(player, VoiceChat.VoiceChatChannel.Scp1576);                

                Timing.CallDelayed(UnityEngine.Random.Range(Plugin.Instance.Config.DeadChatMinTime, Plugin.Instance.Config.DeadChatMaxTime), () =>
                {
                    Plugin.Instance.channels.Remove(player);
                    if (!player.IsAlive)
                    {                        
                        return;
                    }                   
                    player.ShowHint($"<color=#od98ba>You are no longer being haunted by the dead</color>");
                });
            });
        }
    }
}
