using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FNAExtensions;

namespace AFewLives.Entities
{
    class MovingPlatform : Obstacle, Activatable
    {
        private float speed;
        private bool active;
        private int pathTarget;
        private List<Vector2> path;

        public MovingPlatform(Texture2D tex, Vector2 pos, Rectangle hitbox,
                              bool inSpiritRealm, List<Vector2> path,
                              float speed, bool active=true) 
            : base(tex, pos, hitbox, inSpiritRealm)
        {
            this.speed = speed;
            this.active = active;
            pathTarget = 0;
            this.path = path;
        }

        public void Update(float delta, Player player)
        {
            base.Update(delta);

            Vector2 startPos = Pos;
            Vector2 targetPoint = path[pathTarget];
            if (active) {
                Vector2 d = targetPoint - Pos;
                if(d.Length() > speed * delta)
                {
                    _pos += d.WithLength(speed * delta);
                }
                else
                {
                    // This will break if points are very close together.
                    // Don't do that.
                    _pos = targetPoint;
                    float remainder = speed - d.Length();
                    pathTarget = (pathTarget + 1) % path.Count;
                    targetPoint = path[pathTarget];
                    d = targetPoint - Pos;
                    _pos += d.WithLength(remainder);
                }
            }

            // If player is riding, move them by same amount
            RectangleF phb = player.Hitbox;
            if (Math.Abs(Hitbox.Top - phb.Bottom) < 0.02
                && phb.Right >= Hitbox.Left && phb.Left <= Hitbox.Right)
            {
                player.MoveWithoutCollision(Pos - startPos);
            }
        }

        public void Activate()
        {
            active = !active;
        }
    }
}
