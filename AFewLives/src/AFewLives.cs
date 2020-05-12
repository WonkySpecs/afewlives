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

        private SpriteBatch spriteBatch;
        private AssetStore assets;
        private World world;

        private AFewLives()
        {
            GraphicsDeviceManager gdm = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1080,
                PreferredBackBufferHeight = 900,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets = new AssetStore(Content, GraphicsDevice, new AnimationFactory());
            world = new World(new EntityFactory(assets));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            world.Update(gameTime, keyboardState);
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
