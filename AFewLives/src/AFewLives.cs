using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class AFewLives : Game
    {
        static void Main(string[] args)
        {
            using(AFewLives g = new AFewLives())
            {
                g.Run();
            }
        }

        private KeyboardState prevKeyboardState = new KeyboardState();
        private MouseState prevMouseState = new MouseState();
        private SpriteBatch spriteBatch;
        private AssetStore assets;
        private AnimationFactory animationFactory = new AnimationFactory();
        private World world;

        private AFewLives()
        {
            GraphicsDeviceManager gdm = new GraphicsDeviceManager(this);
            gdm.PreferredBackBufferWidth = 1800;
            gdm.PreferredBackBufferHeight = 900;
            gdm.IsFullScreen = false;
            gdm.SynchronizeWithVerticalRetrace = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets = new AssetStore(Content);
            world = new World(assets, animationFactory);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            var moveKeys = new Dictionary<Keys, Vector2>
            {
                { Keys.W, new Vector2(0, -1) },
                { Keys.S, new Vector2(0, 1) },
                { Keys.A, new Vector2(-1, 0) },
                { Keys.D, new Vector2(1, 0) }
            };
            var ms = new Vector2(0, 0);

            foreach (var entry in moveKeys)
            {
                if (keyboardState.IsKeyDown(entry.Key))
                {
                    ms += entry.Value;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Space)) { ms *= 10;  }
            world.Player.Vel = ms;

            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;

            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);
            spriteBatch.Begin();
            world.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
