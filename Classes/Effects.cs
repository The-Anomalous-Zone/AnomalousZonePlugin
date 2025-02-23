using Exiled.API.Enums;
using System.Collections.Generic;
using Exiled.API.Features;
using CustomPlayerEffects;
using Exiled.API.Extensions;

namespace AnomalousZonePlugin.Classes
{
    // Please don't look at this mess that doesn't work
    public class Effects
    {
        private StatusEffectBase Effect;
        private byte Intensity;
        private byte CurrentIntensity;
        private float Duration;

        public Effects(StatusEffectBase Effect, byte Intensity, byte CurrentIntensity, float Duration)
        {
            this.Effect = Effect;
            this.Intensity = Intensity;
            this.CurrentIntensity = CurrentIntensity;
            this.Duration = Duration;
        }
        public StatusEffectBase GetEffect()
        {
            return Effect;
        }
        public EffectType GetEffectType()
        {
            return Effect.GetEffectType();
        }
        public byte GetIntensity()
        {
            return Intensity;
        }
        public byte GetCurrentIntensity()
        {
            return CurrentIntensity;
        }
        public float GetDuration()
        {
            return Duration;
        }
    }
}