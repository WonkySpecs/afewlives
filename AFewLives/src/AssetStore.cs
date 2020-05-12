using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AFewLives
{
    class AssetStore
    {
        private readonly Dictionary<string, Texture2D> textures;
        private readonly GraphicsDevice graphicsDevice;
        private readonly AnimationFactory animationFactory;

        public AnimatedSprite PlayerSprite { get => new AnimatedSprite(textures["player"], animationFactory.PlayerAnimations()); }
        public AnimatedSprite LeverSprite()
        { 
            return new AnimatedSprite(textures["lever"], animationFactory.LeverAnimations(), SpriteState.Deactivated);
        }
        public Texture2D LeverTexture { get => textures["lever"]; }

        public AssetStore(ContentManager content, GraphicsDevice graphicsDevice, AnimationFactory animationFactory)
        {
            textures = new Dictionary<string, Texture2D>();
            textures.Add("player", content.Load<Texture2D>("snek"));
            textures.Add("lever", content.Load<Texture2D>("lever"));
            this.graphicsDevice = graphicsDevice;
            this.animationFactory = animationFactory;
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
