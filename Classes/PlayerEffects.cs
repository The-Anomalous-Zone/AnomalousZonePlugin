using Exiled.API.Enums;
using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;
using Exiled.API.Extensions;

namespace AnomalousZonePlugin.Classes
{
    // Please don't look at this mess that doesn't work
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
        public void RemoveEffect(EffectType effect)
        {
            int x = 0;
            while (x < effects.Count)
            {
                if (effects[x].GetEffect().GetEffectType() == effect)
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
            Effects[] customEffects = player.effects.ToArray();
            Effect[] exiledEffects = new Effect[customEffects.Length];

            for (int i = 0; i < customEffects.Length; i++)
            {                
                exiledEffects[i] = ConvertToExiledEffect(customEffects[i]);
            }

            return exiledEffects;
        }
        private static Effect ConvertToExiledEffect(Effects effect)
        {
            Effect exiledEffect = new Effect(effect.GetEffectType(), effect.GetIntensity());
            return exiledEffect;
        }

    }
}