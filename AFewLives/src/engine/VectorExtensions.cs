using Microsoft.Xna.Framework;
using System;

namespace FNAExtensions
{
    public static class VectorExtensions
    {
        public static Vector2 WithLength(this Vector2 v, float len)
        {
            if (v.X == 0 && v.Y == 0) return new Vector2();
            return (len / v.Length()) * v;
        }

        public static Rectangle ToRectangle(this Vector2 v)
        {
            return new Rectangle(0, 0, (int)Math.Round(v.X), (int)Math.Round(v.Y));
        }
    }
}
