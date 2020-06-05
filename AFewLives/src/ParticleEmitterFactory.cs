using Microsoft.Xna.Framework;

namespace AFewLives
{
    class ParticleEmitterFactory
    {
        AssetStore assets;

        public ParticleEmitterFactory(AssetStore assets)
        {
            this.assets = assets;
        }

        public ParticleEmitter GhostShimmer()
        {
            ParticleEmitter.ParticleAttrs pa = new ParticleEmitter.ParticleAttrs
            {
                minLifetime = 60,
                maxLifetime = 60,
                minVel = new Vector2(-1, -3),
                maxVel = new Vector2(1, -1),
                maxAccel = new Vector2(0, 0.1f),
                maxRotDelta = -0.05f, minRotDelta = 0.05f,
            };
            return new ParticleEmitter(pa, assets.LeverTexture, 0.1f, new Vector2(300, 300), Color.OrangeRed);
        }

        public ParticleEmitter Flame(Vector2 pos)
        {
            ParticleEmitter.ParticleAttrs pa = new ParticleEmitter.ParticleAttrs
            {
                minLifetime = 15,
                maxLifetime = 80,
                minVel = new Vector2(-0.15f, 0.2f),
                maxVel = new Vector2(0.15f, 0.8f),
                minAccel = new Vector2(0, -0.06f),
                maxAccel = new Vector2(0, -0.03f),
                minScale = 0.2f,
                maxScale = 0.35f,
                minDScale = 0.93f,
                maxDScale = 0.98f,
                maxOffsetRadius = 0.3f,
            };
            return new ParticleEmitter(pa, assets.Spark, 2f, pos, Color.White);
        }
    }
}
