using Exiled.API.Enums;
using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;

namespace AnomalousZonePlugin.Classes
{
    public class PlayerEffects
    {
        private List<StatusEffectBase> effects;
        private Player player;
        public PlayerEffects(Player player) 
        {
            this.player = player;
            effects = new List<StatusEffectBase>();
        }
        public void AddEffect(StatusEffectBase effect)           
        {
            effects.Add(effect);
        }
        public void RemoveEffect(StatusEffectBase effect)
        {
            effects.Remove(effect);
        }
        public void ClearEffects()
        {
            effects.Clear();
        }
        public static Effect[] GetEffects(PlayerEffects player)
        {
            return player.effects.ToArray();
        }

    }
}
