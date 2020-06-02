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
                minOffset = Vector2.Zero,
                maxOffset = Vector2.Zero,
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
                minLifetime = 5,
                maxLifetime = 60,
                minVel = new Vector2(-0.3f, 0),
                maxVel = new Vector2(0.3f, 1),
                minAccel = new Vector2(0, -0.2f),
                maxAccel = new Vector2(0, -0.05f),
                maxRotDelta = -0.3f, minRotDelta = 0.3f,
            };
            return new ParticleEmitter(pa, assets.Spark, 5f, pos, Color.White);
        }
    }
}
