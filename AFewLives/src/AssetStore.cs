using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class AssetStore
    {
        private readonly GraphicsDevice graphicsDevice;

        public Texture2D PlayerSpriteSheet { get; }

        public AssetStore(ContentManager content, GraphicsDevice graphicsDevice)
        {
            PlayerSpriteSheet = content.Load<Texture2D>("snek");
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D PlainColor(Vector2 size, Color c)
        {
            int w = (int)size.X;
            int h = (int)size.Y;
            Texture2D tex = new Texture2D(graphicsDevice, w, h);
            Color[] data = new Color[w * h];
            for (int i = 0; i < data.Length; i++) data[i] = c;
            tex.SetData(data);
            return tex;
        }

        public Texture2D Gradient(Vector2 size)
        {
            int w = (int)size.X;
            int h = (int)size.Y;
            Texture2D tex = new Texture2D(graphicsDevice, w, h);
            Color[] data = new Color[w * h];
            for (int i = 0; i < data.Length; i++)
            {
                float val = (float)i / (float)data.Length;
                data[i] = new Color(val, val, val);
            }
            tex.SetData(data);
            return tex;
        }
    }
}
