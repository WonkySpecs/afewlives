using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class Particle
    {
        private float lifetime = 0, elapsed = -1;
        public Vector2 x, dx, dv;
        public float r, dr;
        public Color col;

        public void Init(
            float lifetime,
            Vector2 pos, Vector2 vel, Vector2 accel,
            float r, float dr,
            Color col)
        {
            this.lifetime = lifetime;
            elapsed = 0;
            x = pos;
            dx = vel;
            dv = accel;
            this.r = r;
            this.dr = dr;
            this.col = col;
        }

        public bool Active { get => elapsed < lifetime;  }
        public void Update(float delta)
        {
            if (!Active) return;
            elapsed += delta;
            dx += dv * delta;
            x += dx * delta;
            r += dr * delta;
        }
    }
}
