using AFewLives.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AFewLives
{
    class Physics
    {
        public static readonly float GRAVITY = 0.45f;

        public static Vector2 PositionCorrection(Vector2 pos, Vector2 vel, float delta, Rectangle hb, List<Obstacle> solids)
        {
            Vector2 correction = new Vector2();
            RectangleF projectedHb = RectangleF.FromPointAndOffset(pos + vel * delta, hb);
            foreach (Obstacle w in solids)
            {
                Vector2 relVel = vel - w.Vel;
                RectangleF collision = w.CollisionWith(projectedHb);
                if (collision.Width > 0 && collision.Height > 0)
                {
                    if (collision.Width < collision.Height)
                    {
                        correction.X = collision.X > pos.X ? -collision.Width : collision.Width;
                    }
                    else
                    {
                        correction.Y = collision.Y > pos.Y ? -collision.Height : collision.Height;
                    }
                }
            }
            return correction;
        }
    }
}
