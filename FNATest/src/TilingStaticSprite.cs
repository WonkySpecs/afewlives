using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class TilingStaticSprite : Sprite
    {
        private readonly Texture2D tex;
        private readonly Vector2 totalSize;
        private readonly Vector2 gridSize;

        public TilingStaticSprite(Texture2D tex, Vector2 size)
        {
            this.tex = tex;
            this.totalSize = size;
            this.gridSize = new Vector2(tex.Width / totalSize.X, tex.Height / totalSize.Y);
        }

        public void Update(GameTime delta, SpriteState state) { }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Color tint)
        {
            for(int i = 0; i < gridSize.X; i++)
            {
                for (int j = 0; j < gridSize.Y; j++)
                {
                    spriteBatch.Draw(tex, pos + new Vector2(i * tex.Width, j * tex.Height), tint);
                }
            }
        }

        public Vector2 Size() => totalSize;
    }
}
