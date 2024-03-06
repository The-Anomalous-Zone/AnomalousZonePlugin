using Exiled.API.Enums;
using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;

namespace AnomalousZonePlugin.Classes
{
    public class PlayerEffects
    {
        private Player player;
        private List<Effects> effects;
        
        public PlayerEffects(Player player) 
        {
            this.player = player;
            this.effects = new List<Effects>();
        }
        public void AddEffect(StatusEffectBase effect, byte intensity, byte currentIntensity, float duration)           
        {
            effects.Add(new Effects(effect, intensity, currentIntensity, duration));
        }
        public void RemoveEffect(StatusEffectBase effect)
        {
            int x = 0;
            while (x < effects.Count)
            {
                if (effects[x].GetEffect() == effect)
                {
                    effects.RemoveAt(x);
                }
                x++;
            }       
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
