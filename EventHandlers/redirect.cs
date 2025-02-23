using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalousZonePlugin.Classes
{
    //I don't know what this dumb test code was
    public class redirect
    {
        private Plugin plugin;
        public redirect(Plugin plugin) => this.plugin = plugin; 

        public void onJoin(JoinedEventArgs ev)
        {
            //if (Server.IpAddress != "anomalouszone.net")
            //{
            //    ev.Player.SendConsoleMessage("cn testing.anomalouszone.net", "white");
            //}
        }
    }
}
