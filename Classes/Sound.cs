using Exiled.API.Features;
using Exiled.API.Features.Components;
using MEC;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Events;
using SCPSLAudioApi.AudioCore;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AnomalousZonePlugin.Classes
{
    public class Sound
    {
        // It should have been audioPlayers not players
        // Don't care about the ambiguity
        // Too lazy to fix
        public static List<ReferenceHub> players = new List<ReferenceHub>();

        public static void PlaySound(string file, byte volume, bool loop, string name, Vector3 position, float dur = 0)
        {
            try
            {
                var newPlayer = UnityEngine.Object.Instantiate(CustomNetworkManager.singleton.playerPrefab);
                int id = 9999 + players.Count;
                var fakeConnection = new FakeConnection(id++);
                var hubPlayer = newPlayer.GetComponent<ReferenceHub>();
                players.Add(hubPlayer);
                // We're not violating the VSR 
                NetworkServer.AddPlayerForConnection(fakeConnection, newPlayer);

                Npc audio = SpawnFix(name, position != Vector3.zero ? RoleTypeId.Tutorial : RoleTypeId.Spectator, id, "");
                if (audio == null || audio.ReferenceHub == null)
                {
                    Log.Error("Failed to spawn or couldn't get reference hub");
                    return;
                }
                //audio.IsGodModeEnabled = true;
                //audio.ReferenceHub.characterClassManager.GodMode = true;
                hubPlayer = audio.ReferenceHub;
                players.Add(hubPlayer);
                if (hubPlayer == null)
                {
                    Log.Error("Failed to get reference hub");
                    return;
                }

                var audioPlayer = AudioPlayerBase.Get(hubPlayer);
                var path = Path.Combine(Path.Combine(Paths.Configs, "FunnyPills"), file);

                if (audioPlayer == null)
                {
                    Log.Error("AudioPlayer is null.");
                    return;
                }

                audioPlayer.Enqueue(path, -1);
                audioPlayer.LogDebug = false;
               // audioPlayer.BroadcastChannel = VoiceChat.VoiceChatChannel.Intercom;

                audioPlayer.Volume = volume;
                audioPlayer.Loop = loop;
                audioPlayer.Play(0);

                if (dur != 0 && hubPlayer != null)
                {
                    Timing.CallDelayed(dur, ()=>
                    {
                      //  hubPlayer.transform.position = new Vector3(-99999, -99999, -99999);
                        players.Remove(hubPlayer);
                        if (audioPlayer.CurrentPlay != null)
                        {
                            audioPlayer.Stoptrack(true);
                            audioPlayer.OnDestroy();
                        }

                       // hubPlayer.gameObject.transform.position = new Vector3(-99999, -99999, -99999);
                        Timing.CallDelayed(0.5f, () =>
                        {
                            //NetworkServer.Destroy(hubPlayer.gameObject);
                        });
                    });
                }

                Log.Debug($"Playing sound {path}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error in PlaySound: {ex}");
            }
        }

        public static void Stop()
        {
            foreach (var player in players)
            {
                if (player == null) continue;

                var audioPlayer = AudioPlayerBase.Get(player);
                if (audioPlayer == null) continue;

                if (audioPlayer.CurrentPlay != null)
                {
                    audioPlayer.Stoptrack(true);
                    audioPlayer.OnDestroy();
                }

                player.gameObject.transform.position = new Vector3(-99999, -99999, -99999);
                Timing.CallDelayed(0.5f, () =>
                {
                    NetworkServer.Destroy(player.gameObject);
                });
            }
            players.Clear();
        }

        private static Npc SpawnFix(string name, RoleTypeId role, int id = 0, string userId = "", Vector3? position = null)
        {
            try
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
                catch (Exception e)
                {
                    Log.Debug($"Probably can ignore: {e}");
                }
                NetworkServer.AddPlayerForConnection(new FakeConnection(id), gameObject);

                try
                {
                    npc.ReferenceHub.authManager.NetworkSyncedUserId = null;
                }
                catch (Exception e)
                {
                    Log.Debug($"Probably can ignore: {e}");
                }
                npc.ReferenceHub.nicknameSync.Network_myNickSync = name;

                Player.Dictionary.Add(gameObject, npc);

                Timing.CallDelayed(0.3f, ()=>
                {
                    npc.Role.Set(role, Exiled.API.Enums.SpawnReason.RoundStart, RoleSpawnFlags.None);
                });

                if (position.HasValue)
                {
                    Timing.CallDelayed(0.5f, ()=>
                    {
                        npc.Position = position.Value;
                        npc.Scale = Vector3.zero;
                    });
                }

                return npc;
            }
            catch (Exception ex)
            {
                Log.Error($"Error in SpawnFix: {ex}");
                return null;
            }
        }
    }
}
