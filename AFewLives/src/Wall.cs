using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class Wall : Entity
    {
        public Wall(Texture2D tile, Vector2 size, Vector2 pos) : base(new TilingStaticSprite(tile, size),
                                                                      pos, 
                                                                      new Rectangle(0, 0, (int)size.X, (int)size.Y)) { }
    }
}
