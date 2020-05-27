using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Corpse : Entity
    {
        public Corpse(Sprite sprite, Vector2 pos, Vector2 vel) : base(sprite, pos)
        {
            _vel = vel;
        }

        public void Update(float delta, List<Obstacle> obstacles)
        {
            base.Update(delta);
            _vel.Y += Physics.GRAVITY * delta;
            _vel.X *= 0.96f;

            var newPos = _pos + _vel * delta;
            var correction = Physics.PositionCorrection(_pos, _vel, delta, staticHitbox, obstacles);
            _pos = newPos + correction;
            if (correction.X != 0) _vel.X = 0;
            if (correction.Y != 0) _vel.Y = 0;
        }
    }
}
