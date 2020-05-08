using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class RectangleF
    {
        public Vector2 TopLeft { get; set; }
        public Vector2 Size { get; set; }
        public float Top { get => TopLeft.Y;  }
        public float Left { get => TopLeft.X;  }
        public float Bottom { get => TopLeft.Y + Size.Y;  }
        public float Right { get => TopLeft.X + Size.X;  }

        public float X { get => TopLeft.X; }
        public float Y { get => TopLeft.Y; }
        public float Width { get => Size.X; }
        public float Height { get => Size.Y ; }

        public RectangleF(float x, float y, float w, float h)
        {
            this.TopLeft = new Vector2(x, y);
            this.Size = new Vector2(w, h);
        }

        public Rectangle Round()
        {
            return new Rectangle((int)Math.Round(TopLeft.X),
                                 (int)Math.Round(TopLeft.Y),
                                 (int)Math.Round(Size.X),
                                 (int)Math.Round(Size.Y));
        }

        public static RectangleF FromPointAndOffset(Vector2 pos, Rectangle offset)
        {
            return new RectangleF(pos.X + offset.X, pos.Y + offset.Y, offset.Width, offset.Height);
        }

        public static RectangleF operator +(RectangleF r, Vector2 update) => new RectangleF(r.X + update.X, r.Y + update.Y, r.Width, r.Height);

        public new String ToString()
        {
            return String.Format("RectangleF {{ X: {0}, Y: {1}, Width: {2}, Height: {3} }}", X, Y, Width, Height);
        }
    }
}
