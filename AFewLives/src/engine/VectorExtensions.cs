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

        public static Vector2 Abs(this Vector2 v)
        {
            return new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
        }

        public static Vector2 Add(this Vector2 v, float a)
        {
            return new Vector2(v.X + a, v.Y + a);
        }

        public static Vector2 LerpComponents(Vector2 v1, Vector2 v2, float xAmount, float yAmount)
        {
            return new Vector2(MathHelper.Lerp(v1.X, v2.X, xAmount), MathHelper.Lerp(v1.Y, v2.Y, yAmount));
        }
    }
}
