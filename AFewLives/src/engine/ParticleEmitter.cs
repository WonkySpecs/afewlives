using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AFewLives
{
    class ParticleEmitter
    {
        /**
         * Ranges of starting attributes for new particles
         */
        public class ParticleAttrs
        {
            public float minLifetime, maxLifetime;
            public Vector2 minOffset = Vector2.Zero, maxOffset = Vector2.Zero;
            public Vector2 minVel = Vector2.Zero, maxVel = Vector2.Zero;
            public Vector2 minAccel = Vector2.Zero, maxAccel = Vector2.Zero;
            public float minRot = 0, maxRot = 0;
            public float minRotDelta = 0, maxRotDelta = 0;
        }

        private static readonly int INITIAL_PARTICLES_ALLOCATED = 200;

        private Particle[] particles = new Particle[INITIAL_PARTICLES_ALLOCATED];
        private int allocated = INITIAL_PARTICLES_ALLOCATED;

        private ParticleAttrs pAttrs;
        private float lifetime = -1, elapsed = 0;
        private Texture2D tex;
        private float perDelta = 1;
        private float sinceLastSpawn = 0;
        public Vector2 pos;
        private Color col;

        public bool Finished { get => lifetime > 0 && elapsed > lifetime; }

        public ParticleEmitter(ParticleAttrs pa, Texture2D tex, float perDelta, Vector2 pos, Color col)
        {
            pAttrs = pa;
            this.tex = tex;
            this.perDelta = perDelta;
            this.pos = pos;
            this.col = col;
            for(int i=0; i < INITIAL_PARTICLES_ALLOCATED; i++)
            {
                particles[i] = new Particle();
            }
        }

        public void Update(float delta)
        {
            elapsed += delta;
            sinceLastSpawn += delta;
            if (Finished) return;
            while (sinceLastSpawn > 1 / perDelta)
            {
                SpawnParticle();
                sinceLastSpawn -= 1 / perDelta;
            }

            foreach (var p in particles)
            {
                if (!p.Active) continue;
                p.Update(delta);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rotOrig = new Vector2(tex.Width / 2, tex.Height / 2);
            foreach (var p in particles)
            {
                if (!p.Active) continue;
                spriteBatch.Draw(tex, p.x, null, p.col, p.r, rotOrig, Vector2.One, SpriteEffects.None, 0);
            }
        }

        private void SpawnParticle()
        {
            int freeSlot = -1;
            for (int i=0; i < allocated; i++)
            {
                if (!particles[i].Active)
                {
                    freeSlot = i;
                    break;
                }
            }
            if (freeSlot == -1)
            {
                // TODO: Allocate more
                freeSlot = allocated;
                Console.WriteLine("Allocating more particles");
            }
            var rng = new Random();
            float rand() => (float)rng.NextDouble();
            particles[freeSlot].Init(
                MathHelper.Lerp(pAttrs.minLifetime, pAttrs.maxLifetime, rand()),
                Vector2.Lerp(pAttrs.minOffset, pAttrs.maxOffset, rand()) + pos,
                Vector2.Lerp(pAttrs.minVel, pAttrs.maxVel, rand()),
                Vector2.Lerp(pAttrs.minAccel, pAttrs.maxAccel, rand()),
                MathHelper.Lerp(pAttrs.minRot, pAttrs.maxRot, rand()),
                MathHelper.Lerp(pAttrs.minRotDelta, pAttrs.maxRotDelta, rand()),
                col);
        }
    }
}
