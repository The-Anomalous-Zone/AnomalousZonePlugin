using AnomalousZonePlugin.Classes.SCP294;
using AnomalousZonePlugin.Configs.SCP294;
using MapEditorReborn.API.Features.Objects;
using System.Collections.Generic;
using System.IO;
using VoiceChat.Codec;

namespace AnomalousZonePlugin.EventHandlers.SCP294
{
    public class serverHandler
    {
        public void WaitingForPlayers()
        {
            Plugin.Instance.SpawnedSCP294s = new Dictionary<SchematicObject, bool>();
            Plugin.Instance.PlayersNear294 = new List<string>();
            Plugin.Instance.CustomDrinkItems = new Dictionary<ushort, DrinkInfo>();
            Plugin.Instance.PlayerVoicePitch = new Dictionary<string, float>();
            Plugin.Instance.Encoders = new Dictionary<ReferenceHub, OpusComponent>();
            SCP294Object.SpawnSCP294();
        }
    }
}