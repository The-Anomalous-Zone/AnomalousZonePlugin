using Exiled.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using LiteDB;

namespace AnomalousZonePlugin.Classes.SCPSuicide
{
    public static class DatabasePlayer
    {
            public static string GetAuthentication(this Player player)
            {
                return player.UserId.Split('@')[1];
            }

            public static string GetRawUserId(this Player player)
            {
                return player.UserId.GetRawUserId();
            }

            public static string GetRawUserId(this string player)
            {
                return player.Split('@')[0];
            }

            public static Player GetDatabasePlayer(this string player)
            {
                return Player.Get(player)?.GetDatabasePlayer() ??
                    Database.LiteDatabase.GetCollection.FindOne(queryPlayer => queryPlayer.Id == player.GetRawUserId() || queryPlayer.Name == player);
            }

            public static Player GetDatabasePlayer(this Player player)
            {
                if (player == null)
                {
                    return null;
                }
                else if (Database.PlayerData.TryGetValue(player, out Player databasePlayer))
                {
                    return databasePlayer;
                }
                else
                {
                    return Database.LiteDatabase.GetCollectionPlayer().FindOne(queryPlayer => queryPlayer.Id == player.GetRawUserId());
                }
            }

        }
}
