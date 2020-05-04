using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AFewLives
{
    class AssetStore
    {
        public Texture2D PlayerSpriteSheet { get; }

        public AssetStore(ContentManager content)
        {
            PlayerSpriteSheet = content.Load<Texture2D>("snek");
        }
    }
}
