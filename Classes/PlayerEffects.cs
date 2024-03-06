using Exiled.API.Enums;
using System.Collections.Generic;
using Exiled.API.Features;

namespace AnomalousZonePlugin.Classes
{
    public class PlayerEffects
    {
        private List<Effect> effects;
        private Player player;
        public PlayerEffects(Player player) 
        {
            this.player = player;
            effects = new List<Effect>();
        }
        public void AddEffect(Effect effect)           
        {
            effects.Add(effect);
        }
        public void RemoveEffect(Effect effect)
        {
            effects.Remove(effect);
        }
        public static Effect[] GetEffects(PlayerEffects player)
        {
            return player.effects.ToArray();
        }

    }
}
