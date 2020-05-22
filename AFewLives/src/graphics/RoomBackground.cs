
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace AFewLives
{
    class RoomBackground
    {
        private List<Vector2> lights;
        private Texture2D tex;
        private Texture2D torchLight;
        private RenderTarget2D lightMask;

        public RoomBackground(Texture2D tile, Texture2D torchLight, Vector2 size, SpriteBatch sb)
        {
            RenderTarget2D tex = new RenderTarget2D(sb.GraphicsDevice, (int)size.X, (int)size.Y);
            sb.GraphicsDevice.SetRenderTarget(tex);
            sb.Begin();
            for (int x = 0; x < size.X; x += tile.Width)
            {
                for (int y = 0; y < size.Y; y += tile.Height)
                {
                    sb.Draw(tile, new Vector2(x, y), Color.White);
                }    
            }
            sb.End();
            sb.GraphicsDevice.SetRenderTarget(null);
            this.tex = tex;
            this.torchLight = torchLight;
            lights = new List<Vector2> { new Vector2(200, 200), new Vector2(250, 300)};
            lightMask = new RenderTarget2D(sb.GraphicsDevice, tex.Width, tex.Height);
            Stream s = File.Create("test.png");
            s.Dispose();
        }

        public void Draw(SpriteBatch sb, RenderTarget2D target, Effect effect, Matrix transform)
        {
            sb.GraphicsDevice.SetRenderTarget(lightMask);
            sb.GraphicsDevice.Clear(Color.Black);
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transform);
            foreach (Vector2 lightPos in lights)
            {
                sb.Draw(torchLight, lightPos, Color.White);
            }
            sb.End();

            sb.GraphicsDevice.SetRenderTarget(target);
            sb.GraphicsDevice.Clear(Color.Black);

            effect.Parameters["lightMask"].SetValue(lightMask);
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, effect, transform);
            sb.Draw(tex, new Vector2(-1000, -1000), Color.Black);
            sb.End();
        }
    }
}
