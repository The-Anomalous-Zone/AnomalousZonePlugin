using HarmonyLib;
using Mirror;
using PlayerRoles.Spectating;
using PlayerRoles;
using PluginAPI.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoiceChat.Networking;
using NorthwoodLib.Pools;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.Voice;

namespace AnomalousZonePlugin.Classes.FunnyPills

{    
    internal sealed class ModdedVoiceChat
    {
        private Plugin plugin;
        public ModdedVoiceChat(Plugin plugin) => this.plugin = plugin;
        public void OnPlayerUsingVoiceChat(VoiceChattingEventArgs ev)
        {
            if (!Plugin.Instance.channels.ContainsKey(ev.Player))
            {
                return;
            }
            ev.IsAllowed = false; // Prevent default behavior from causing weird stuff

            VoiceMessage message = ev.VoiceMessage;
            message.Channel = Plugin.Instance.channels[ev.Player];

            foreach (ReferenceHub referenceHub in ReferenceHub.AllHubs)
            {
                if (referenceHub.roleManager.CurrentRole is IVoiceRole voiceRole)
                {
                    voiceRole.VoiceModule.ValidateReceive(message.Speaker, message.Channel);
                    referenceHub.connectionToClient.Send(message);
                }
            }
        }
    }

}
