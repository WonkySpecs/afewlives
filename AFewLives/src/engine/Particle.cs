using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class Particle
    {
        public float lifetime = -1, elapsed = 0;
        public Vector2 x, dx, dv;
        public float r, dr;
        public float scale;
        public float dScale;
        public Color col;

        public void Init(
            float lifetime,
            Vector2 pos, Vector2 vel, Vector2 accel,
            float r, float dr,
            float scale, float dScale,
            Color col)
        {
            this.lifetime = lifetime;
            elapsed = 0;
            x = pos;
            dx = vel;
            dv = accel;
            this.r = r;
            this.dr = dr;
            this.scale = scale;
            this.dScale = dScale;
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
            scale *= dScale;
        }
    }
}
