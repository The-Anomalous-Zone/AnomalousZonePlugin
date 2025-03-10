﻿using Exiled.API.Features;
using HarmonyLib;
using InventorySystem.Items;
using InventorySystem.Items.Usables;
using Mirror;
using NorthwoodLib.Pools;
using PlayerRoles.Voice;
using AnomalousZonePlugin.Configs.SCP294;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Networking;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Classes.SCP294
{

    internal static class SCP207OnEffectsActivated
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            Label skip = generator.DefineLabel();

            newInstructions.Add(new CodeInstruction(OpCodes.Ret));
            newInstructions[newInstructions.Count - 1].labels.Add(skip);

            newInstructions.InsertRange(0, new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DrinkInfo), nameof(DrinkInfo.IsCustomDrink), new[] { typeof(ItemBase) })),
                new CodeInstruction(OpCodes.Brtrue_S, skip),
            });

            foreach (CodeInstruction instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }


    internal static class AntiSCP207OnEffectsActivated
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            Label skip = generator.DefineLabel();

            newInstructions.Add(new CodeInstruction(OpCodes.Ret));
            newInstructions[newInstructions.Count - 1].labels.Add(skip);

            newInstructions.InsertRange(0, new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(DrinkInfo), nameof(DrinkInfo.IsCustomDrink), new[] { typeof(ItemBase) })),
                new CodeInstruction(OpCodes.Brtrue_S, skip),
            });

            foreach (CodeInstruction instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }

    internal static class ServerReceiveMessage
    {
        [HarmonyPrefix]
        private static bool Prefix(NetworkConnection conn, VoiceMessage msg)
        {
            if (!SCP294.Instance.Config.EnableVoiceEffects) return true;

            if (msg.SpeakerNull || msg.Speaker.netId != conn.identity.netId)
            {
                return false;
            }
            IVoiceRole voiceRole = msg.Speaker.roleManager.CurrentRole as IVoiceRole;
            if (voiceRole == null)
            {
                return false;
            }
            if (!voiceRole.VoiceModule.CheckRateLimit())
            {
                return false;
            }
            if (VoiceChatMutes.IsMuted(msg.Speaker, false))
            {
                return false;
            }
            VoiceChatChannel voiceChatChannel = voiceRole.VoiceModule.ValidateSend(msg.Channel);
            if (voiceChatChannel == VoiceChatChannel.None)
            {
                return false;
            }
            voiceChatChannel = voiceRole.VoiceModule.CurrentChannel;

            Player plr = Player.Get(msg.Speaker);
            if (SCP294.Instance.PlayerVoicePitch.TryGetValue(plr.UserId, out float pitchShift) && pitchShift != 1f)
            {
                float[] message = new float[48000];
                OpusComponent comp = OpusComponent.Get(plr.ReferenceHub);
                comp.Decoder.Decode(msg.Data, msg.DataLength, message);

                comp.PitchShift(pitchShift, (long)480, 48000, message);

                msg.DataLength = comp.Encoder.Encode(message, msg.Data, 480);
            }

            foreach (ReferenceHub referenceHub in ReferenceHub.AllHubs)
            {
                IVoiceRole voiceRole2 = referenceHub.roleManager.CurrentRole as IVoiceRole;
                if (voiceRole2 != null)
                {
                    VoiceChatChannel voiceChatChannel2 = voiceRole2.VoiceModule.ValidateReceive(msg.Speaker, voiceChatChannel);
                    if (voiceChatChannel2 != VoiceChatChannel.None)
                    {
                        msg.Channel = voiceChatChannel2;
                        referenceHub.connectionToClient.Send<VoiceMessage>(msg, 0);
                    }
                }
            }
            return false;
        }
    }
}