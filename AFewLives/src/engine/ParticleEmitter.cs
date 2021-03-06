﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FNAExtensions;

namespace AFewLives
{
    class ParticleEmitter
    {
        Random rng = new Random();
        /**
         * Ranges of starting attributes for new particles

            Probably going to replace this with custom function
         */
        public class ParticleAttrs
        {
            public float minLifetime, maxLifetime;
            public float minOffsetRadius = 0, maxOffsetRadius = 0;
            public Vector2 minVel = Vector2.Zero, maxVel = Vector2.Zero;
            public Vector2 minAccel = Vector2.Zero, maxAccel = Vector2.Zero;
            public float minRot = 0, maxRot = 0;
            public float minScale = 1, maxScale = 1;
            public float minDScale = 1, maxDScale = 1;
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
                spriteBatch.Draw(tex, p.x, null, p.col, p.r, rotOrig, p.scale, SpriteEffects.None, 0);
            }
        }

        private void SpawnParticle()
        {
            int freeSlot = -1;
            for (int i = 0; i < allocated; i++)
            {
                if (!particles[i].Active)
                {
                    freeSlot = i;
                    break;
                }
            }
            if (freeSlot == -1)
            {
                freeSlot = allocated;
                var newSize = allocated * 2;
                var newParticles = new Particle[newSize];
                particles.CopyTo(newParticles, 0);
                for (int i = allocated; i < newSize; i++)
                {
                    newParticles[i] = new Particle();
                }
                particles = newParticles;
                allocated = newSize;
            }
            float rand() => (float)rng.NextDouble();
            var offsetDir = Vector2.Normalize(new Vector2(rand() * 2 - 1, rand() * 2 - 1));
            particles[freeSlot].Init(
                MathHelper.Lerp(pAttrs.minLifetime, pAttrs.maxLifetime, rand()),
                offsetDir * MathHelper.Lerp(pAttrs.minOffsetRadius, pAttrs.maxOffsetRadius, rand()) + pos,
                VectorExtensions.LerpComponents(pAttrs.minVel, pAttrs.maxVel, rand(), rand()),
                VectorExtensions.LerpComponents(pAttrs.minAccel, pAttrs.maxAccel, rand(), rand()),
                MathHelper.Lerp(pAttrs.minRot, pAttrs.maxRot, rand()),
                MathHelper.Lerp(pAttrs.minRotDelta, pAttrs.maxRotDelta, rand()),
                MathHelper.Lerp(pAttrs.minScale, pAttrs.maxScale, rand()),
                MathHelper.Lerp(pAttrs.minDScale, pAttrs.maxDScale, rand()),
                col);
        }
    }
}
