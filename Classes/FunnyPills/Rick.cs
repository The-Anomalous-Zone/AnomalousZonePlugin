using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnomalousZonePlugin.Classes.SCP294;
using AudioPlayer.API.Container;
using Exiled.API.Features;
using SCPSLAudioApi.AudioCore;
using VoiceChat;

namespace AnomalousZonePlugin.Classes.FunnyPills
{
    public class Rick
    {        
        public static void play(Player player)
        {
            AudioPlayerBot audioPlayer = new(Plugin.Instance.id++, "Rick Astley", new AudioPlayerBase(), player);
            player.ShowHint($"There is a special announcement");
            Thread.Sleep(150);
            audioPlayer.PlayAudioFromFile("/scpslserver/Exiled/config/funnypills/rick.ogg", false, 100, VoiceChatChannel.Intercom);
            audioPlayer.Destroy();
        }       
    }
}
