using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Net.WebRequestMethods;

namespace AnomalousZonePlugin.EventHandlers
{
    // You saw StupidStuff
    // Now get really for *Drumroll*
           // *ready
    // no
    // just try again
    // Now GET READY FOR *Drumroll*
    // REALLY STUPID STUFF
    internal sealed class ReallyStupidStuff
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string Url = "nuh uh";
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣶⡆⠀⠀⠀⠀⠀⠀⢲⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⣾⣦⣷⣾⣶⣿⣷⣷⡄⠀⣶⡞⣶⢾⣿⣶⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⢸⣿⢿⣿⡿⣿⡇⣿⡇⠀⠿⣷⡿⢸⡿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡧⠀⢿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣷⠀⠀⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡀⠀⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⢸⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⣯⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠁⠀⢸⡇⢀⠔⣿⠀⠀⠀⠀⠀⠀⠀⠀⢐⣠⣤⠤⣤⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⣀⡠⢖⠯⣧⣠⣄⣿⠀⠀⠀⠀⠀⠀⠀⢸⣽⣿⣿⠀⢿⣾⣷⢾⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡴⢚⡡⠒⠁⠀⣽⣿⠔⣿⡀⠀⠀⠀⠀⠀⠀⣼⡀⠀⣣⠀⣆⠀⠀⣼⣬⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⢞⠵⠂⠀⠀⡀⠀⠀⠘⣇⡄⢻⠅⠀⠀⠀⠀⠀⢠⣯⢧⡐⠷⠴⠼⢃⣠⡟⢹⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡞⠁⠀⠀⠀⠀⠀⢆⠀⠀⠠⣟⣄⠘⡇⢀⠀⠀⠀⠀⠚⣇⠇⢻⡷⣦⡴⡟⠁⠀⣴⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢻⠰⠀⠠⡀⠀⠀⢸⠀⠀⠀⢻⡍⠈⡻⡄⠀⠀⠀⠀⠀⠙⣧⣄⠃⠀⠁⠀⢀⣾⡟⠗⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡘⡇⠀⠀⣇⠀⠀⠸⡇⠀⠀⠈⠹⢵⣱⡏⠀⠀⠀⢀⡠⢖⣿⣳⣄⣀⣀⣠⣾⡷⢧⣒⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠃⢻⡈⢀⠼⡆⠀⣤⣿⠀⠀⡀⣠⣾⠋⠁⠀⣀⠴⠋⠀⢼⣷⡊⠋⠙⠋⠋⢣⡋⣾⡜⠻⣴⠦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠂⠀⠓⣶⡿⠛⢾⠿⡿⣷⣾⣿⣿⣁⢀⡤⠚⠁⢠⠆⠂⡘⣯⠋⠀⠈⠢⠚⠁⠀⣼⡇⡕⢹⣿⡷⣟⡆⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⠀⠀⠀⠀⠀⠈⣸⠀⠀⠀⢠⣥⣿⡿⣾⣿⡞⠋⠀⢐⣱⠆⠺⣀⠄⢸⡷⢄⡀⠠⠀⠀⢀⣿⠡⡃⢨⣿⣿⣿⣟⢦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠁⠀⠈⣟⠀⢦⠀⠢⠫⣿⢇⣹⢿⢐⠔⡨⢺⢝⣠⢀⢩⠀⠘⣷⢐⠈⡓⠒⠉⢁⡟⡳⠇⢸⣿⣿⣿⣿⢀⠙⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⢸⡃⠀⠈⢠⠂⠷⠔⠁⣿⢸⡀⠐⠁⢻⠃⢰⡻⠌⡖⡀⢸⣇⠂⡉⡆⠆⣼⢣⢣⢀⣿⣿⣿⣿⡟⢨⠄⠈⠈⢳⡀⠀⠀⠀⠀⠀⠀⠀
//⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⡂⡀⢀⡟⠀⠠⠀⠀⢠⣾⠀⢰⣿⢒⠃⠀⠀⠀⠀⠈⠃⠀⢈⠀⠀⢿⡄⠉⡰⢰⡟⠆⠜⣸⣿⣿⣿⣿⡇⠏⢀⠀⠀⢀⢷⡀⠀⠀⠀⠀⠀⠀       
        private Plugin plugin;
        public ReallyStupidStuff(Plugin plugin) => this.plugin = plugin;

        public async void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player.UserId != "76561199016199424@steam")
            {
                return;
            }

            var payload = new
            {
                content = $"Damage type: {ev.DamageHandler.Type}"
            };

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Message sent");
            }
            else
            {
                Log.Error($"Failed to send mesage. code: {response.StatusCode}");
            }
        }

        public async void OnDying(DyingEventArgs ev)
        {
            // I dare you to use to dox me
            if (ev.Player.UserId != "76561199016199424@steam")
            {
                return;
            }
            var payload = new
            {
                content = $"Damage type: {ev.DamageHandler.Type}"
            };

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Message sent");
            }
            else
            {
                Log.Error($"Failed to send mesage. code: {response.StatusCode}");
            }

        }
    }
}