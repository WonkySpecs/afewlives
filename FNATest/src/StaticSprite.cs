using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class StaticSprite : Sprite
    {
        private readonly Texture2D tex;
        private readonly Vector2 size;

        public StaticSprite(Texture2D tex) 
        { 
            this.tex = tex;
            size = new Vector2(tex.Width, tex.Height);
        }

        public void Update(GameTime delta, SpriteState state) {  }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Color tint)
        {
            spriteBatch.Draw(tex, pos, tint);
        }

        public Vector2 Size() => size;
    }
}
