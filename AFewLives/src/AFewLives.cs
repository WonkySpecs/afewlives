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
        private AnimationFactory animationFactory = new AnimationFactory();
        private World world;

        private AFewLives()
        {
            GraphicsDeviceManager gdm = new GraphicsDeviceManager(this);
            gdm.PreferredBackBufferWidth = 800;
            gdm.PreferredBackBufferHeight = 600;
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
            assets = new AssetStore(Content, GraphicsDevice);
            world = new World(assets, animationFactory);
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
