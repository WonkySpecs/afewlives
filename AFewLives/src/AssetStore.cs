using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using FNAExtensions;

namespace AFewLives
{
    class AssetStore
    {
        private readonly Dictionary<string, Texture2D> textures;
        private readonly GraphicsDevice graphicsDevice;
        private readonly AnimationFactory animationFactory;

        public Sprite PlayerSprite { get => new Sprite(textures["player"], animationFactory.PlayerAnimations()); }
        public Sprite CorpseSprite { get => new Sprite(textures["player"], animationFactory.CorpseAnimations()); }

        public Texture2D LeverTexture { get => textures["lever"]; }
        public Sprite LeverSprite()
        { 
            return new Sprite(textures["lever"], animationFactory.LeverAnimations(), SpriteState.Deactivated);
        }

        public Texture2D SpikesTexture(int width)
        {
            Texture2D spikes = textures["spikes"];
            Color[] spikesData = new Color[spikes.Width * spikes.Height];
            spikes.GetData(spikesData);
            Texture2D tex = new Texture2D(graphicsDevice, width, spikes.Height);
            Color[] data = new Color[tex.Width * tex.Height];
            for (int y = 0; y < tex.Height; y++)
            {
                for(int x = 0; x < tex.Width; x++)
                {
                    data[y * tex.Width + x] = spikesData[y * spikes.Width + (x % spikes.Width)];
                }
            }
            tex.SetData(data);
            return tex;
        }

        public Texture2D BrickTile { get => textures["brick_tile"]; }
        public Texture2D TorchLight { get => textures["torch_light"]; }
        public Texture2D Spark { get => textures["spark"]; }

        public AssetStore(ContentManager content, GraphicsDevice graphicsDevice, AnimationFactory animationFactory)
        {
            textures = new Dictionary<string, Texture2D>();
            textures.Add("player", content.Load<Texture2D>("snek"));
            textures.Add("lever", content.Load<Texture2D>("lever"));
            textures.Add("spikes", content.Load<Texture2D>("spikes"));
            textures.Add("brick_tile", content.Load<Texture2D>("brick"));
            textures.Add("torch_light", content.Load<Texture2D>("torchLight"));
            textures.Add("spark", content.Load<Texture2D>("spark"));
            this.graphicsDevice = graphicsDevice;
            this.animationFactory = animationFactory;
        }

        public Texture2D SolidColor(Vector2 size, Color c)
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

        public Texture2D Stripy(Vector2 size, int numStripes=6)
        {
            int w = (int)size.X;
            int h = (int)size.Y;
            float stripeSize = (float)h / (float) numStripes;
            Texture2D tex = new Texture2D(graphicsDevice, w, h);
            Color[] data = new Color[w * h];
            for (int y = 0; y < h; y++)
            {
                int stripe = (int)(y / stripeSize);
                float val = (1 - (float)stripe / (float)numStripes);
                for(int x = 0; x < w; x++)
                {
                    data[y * w + x] = new Color(val, val, val);
                }
            }
            tex.SetData(data);
            return tex;
        }

        public Texture2D Patchy(Vector2 size, int patchW=25, int patchH=25)
        {
            if (size.X % patchW != 0 || size.Y % patchH != 0)
            {
                throw new Exception("Patchy texture patch sizes must divide into whole size");
            }

            int w = (int)size.X;
            int h = (int)size.Y;
            int gridW = w / patchW;
            int gridH = h / patchH;
            var rng = new Random();
            var patches = new Color[gridW, gridH];
            for (int u = 0; u < gridW; u++)
            {
                for (int v = 0; v < gridH; v++)
                {
                    double val = rng.NextDouble();
                    patches[u, v] = new Color((float)val, (float)val, (float)val, 1);
                }
            }

            Texture2D tex = new Texture2D(graphicsDevice, w, h);
            Color[] data = new Color[w * h];
            for (int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    data[y * w + x] = patches[x / patchW, y / patchH];
                }
            }
            tex.SetData(data);
            return tex;
        }

        public Texture2D DarkCentre(Vector2 size)
        {
            int w = (int)size.X;
            int h = (int)size.Y;
            Texture2D tex = new Texture2D(graphicsDevice, w, h);
            Color[] data = new Color[w * h];
            var rng = new Random();
            var bands = new float[] { 0, 0.1f, 0.3f, 0.6f, 0.7f, 0.8f, 1 };
            for (int y = 0; y < h; y++)
            {
                var r = (float)rng.NextDouble() * 0.1f - 0.05f;
                for(int x = 0; x < w; x++)
                {
                    var edgeness = (new Vector2(x, y) / size).Add(-0.5f).Abs();
                    edgeness.X = MathHelper.Clamp(edgeness.X + r, 0, 0.5f);
                    edgeness.Y = MathHelper.Clamp(edgeness.Y + r, 0, 0.5f);
                    var idx = (int)Math.Floor(Math.Max(edgeness.X, edgeness.Y) * 2 * (bands.Length - 1));
                    data[y * w + x] = new Color(bands[idx], bands[idx], bands[idx], 1f);
                }
            }
            tex.SetData(data);
            return tex;
        }
    }
}
