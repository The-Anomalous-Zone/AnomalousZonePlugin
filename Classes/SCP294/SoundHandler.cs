﻿using CentralAuth;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Components;
using MEC;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using SCPSLAudioApi.AudioCore;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VoiceChat;
using VoiceChat.Codec;
using YamlDotNet.Core.Tokens;

// SCP-294 was orginally made by Creepycats here: https://github.com/creepycats/Ultimate294
// I've made some changes (sounds, drinks, text, ect)

namespace AnomalousZonePlugin.Classes.SCP294
{
    public class SoundHandler
    {
        public OpusEncoder Encoder = new OpusEncoder(VoiceChat.Codec.Enums.OpusApplicationType.Voip);

        public static List<ReferenceHub> AudioPlayers = new List<ReferenceHub>();
        public static void PlayAudio(string audioFile, byte volume, bool loop, string soundName, Vector3 position, float dur = 0)
        {
            try
            {
                int id = 9999 + AudioPlayers.Count;
                Npc audioNpc = SpawnFix(soundName, position != Vector3.zero ? RoleTypeId.Tutorial : RoleTypeId.Spectator, id, "");
                audioNpc.Health = 9999f;
                audioNpc.ReferenceHub.characterClassManager._godMode = true;
                audioNpc.ReferenceHub.playerStats.GetModule<AdminFlagsStat>().SetFlag(AdminFlags.GodMode, true);
                var hubPlayer = audioNpc.ReferenceHub;
                AudioPlayers.Add(hubPlayer);

                hubPlayer.authManager.InstanceMode = ClientInstanceMode.Unverified;

                try
                {
                    hubPlayer.nicknameSync.SetNick(soundName);
                }
                catch (Exception) { }

                var audioPlayer = AudioPlayerBase.Get(hubPlayer);

                var path = Path.Combine(Path.Combine(Paths.Configs, "SCP294"), audioFile);

                audioPlayer.Enqueue(path, -1);
                audioPlayer.LogDebug = false;
                audioPlayer.BroadcastChannel = VoiceChatChannel.Intercom;
                if (position != Vector3.zero)
                {
                    audioPlayer.BroadcastChannel = VoiceChatChannel.Proximity;
                    try
                    {
                        hubPlayer.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.None, RoleSpawnFlags.None);
                        hubPlayer.gameObject.transform.position = position;

                        hubPlayer.gameObject.transform.localScale = Vector3.zero;
                        foreach (Player item in Player.List)
                        {
                            Server.SendSpawnMessage?.Invoke(null, new object[2] { hubPlayer.networkIdentity, item.Connection });
                        }
                    }
                    catch (Exception) { }
                }
                audioPlayer.Volume = volume;
                audioPlayer.Loop = loop;
                audioPlayer.Play(0);

                if (dur != 0)
                {
                    Timing.CallDelayed(dur, delegate
                    {
                        hubPlayer.transform.position = new Vector3(-99999, -99999, -99999);
                        AudioPlayers.Remove(hubPlayer);
                        if (audioPlayer.CurrentPlay != null)
                        {
                            audioPlayer.Stoptrack(true);
                            audioPlayer.OnDestroy();
                        }

                        hubPlayer.gameObject.transform.position = new Vector3(-9999f, -9999f, -9999f);
                        Timing.CallDelayed(0.5f, () =>
                        {
                            NetworkServer.Destroy(hubPlayer.gameObject);
                        });
                    });
                }

                Log.Debug($"Playing sound {path}");
            }
            catch (Exception e)
            {
                Log.Error($"Error on: {e.Data} -- {e.StackTrace}");
            }
        }
        public static void StopAudio()
        {
            foreach (var player in AudioPlayers)
            {
                if (!player) continue;
                var audioPlayer = AudioPlayerBase.Get(player);
                if (!audioPlayer) continue;

                if (audioPlayer.CurrentPlay != null)
                {
                    audioPlayer.Stoptrack(true);
                    audioPlayer.OnDestroy();
                }

                player.gameObject.transform.position = new Vector3(-9999f, -9999f, -9999f);
                Timing.CallDelayed(0.5f, () =>
                {
                    NetworkServer.Destroy(player.gameObject);
                });
            }
            AudioPlayers.Clear();
        }
        public static Npc SpawnFix(string name, RoleTypeId role, int id = 0, string userId = "", Vector3? position = null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(Mirror.NetworkManager.singleton.playerPrefab);
            Npc npc = new Npc(gameObject)
            {
                IsNPC = true
            };
            try
            {
                npc.ReferenceHub.roleManager.InitializeNewRole(RoleTypeId.None, RoleChangeReason.None, RoleSpawnFlags.None);
            }
            catch (Exception arg)
            {
                Log.Debug($"Ignore: {arg}");
            }


            if (RecyclablePlayerId.FreeIds.Contains(id))
            {
                RecyclablePlayerId.FreeIds.RemoveFromQueue(id);
            }
            else if (RecyclablePlayerId._autoIncrement >= id)
            {
                id = ++RecyclablePlayerId._autoIncrement;
            }

            NetworkServer.AddPlayerForConnection(new FakeConnection(id), gameObject);
            try
            {
                npc.ReferenceHub.authManager.NetworkSyncedUserId = null;
            }
            catch (Exception arg2)
            {
                Log.Debug($"Ignore: {arg2}");
            }

            npc.ReferenceHub.nicknameSync.Network_myNickSync = name;
            Player.Dictionary.Add(gameObject, npc);
            Timing.CallDelayed(0.3f, delegate
            {
                npc.Role.Set(role, SpawnReason.RoundStart, RoleSpawnFlags.None);
            });
            if (position.HasValue)
            {
                Timing.CallDelayed(0.5f, delegate
                {
                    npc.Position = position.Value;
                });
            }

            return npc;
        }
    }
}