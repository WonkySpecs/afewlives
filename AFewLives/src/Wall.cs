using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.src
{
    class Wall : Entity
    {
        public Wall(Texture2D tile, Vector2 size, Vector2 pos) : base(new TilingStaticSprite(tile, size), pos) { }
    }
}
