using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.EventHandlers
{
    internal sealed class Welcome
    {
        private Plugin plugin;
        public Welcome(Plugin plugin) => this.plugin = plugin;

        public void OnJoined(JoinedEventArgs ev)
        {
            while (ev.Player == null) 
            { }

            ev.Player.Broadcast(10, $"Welcome to <b><color=#ff0000>The </color></b><color=#00226b>A</color><color=#003f9b>n</color><color=#0057ca>o</color><color=#006fe6>m</color><color=#0091ff>a</color><color=#00aaff>l</color><color=#00c5ff>o</color><color=#00e1ff>u</color><color=#00ffff>s </color><color=#00e1ff>Z</color><color=#00c5ff>o</color><color=#00aaff>n</color><color=#0091ff>e</color></b> {ev.Player.Nickname}\nDon't forget to join our <color=#7289DA>Discord</color> server!");
            ev.Player.Broadcast(5, $"This is a <b>heavily modded</b> server");
        }
    }
}
