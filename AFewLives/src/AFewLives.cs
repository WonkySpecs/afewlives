using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AFewLives
{
    class AFewLives : Game
    {
        float expectedFrameTime = 1f / 60f;
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
        private Controls controls = new Controls();
        private bool paused = false;
        private Camera2D cam = new Camera2D();
        private Effects effects;
        private Effect postProcessEffect;
        private RenderTarget2D prePostProcess;

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

            prePostProcess = new RenderTarget2D(GraphicsDevice,
                                                GraphicsDevice.PresentationParameters.BackBufferWidth,
                                                GraphicsDevice.PresentationParameters.BackBufferHeight);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets = new AssetStore(Content, GraphicsDevice, new AnimationFactory());
            postProcessEffect = Content.Load<Effect>("effects/PostProcess");
            effects = new Effects(Content.Load<Effect>("effects/SolidThing"),
                                  Content.Load<Effect>("effects/SpiritRealm"),
                                  Content.Load<Effect>("effects/Background"));
            world = new World(new EntityFactory(assets),
                              new RoomBackground(assets.BrickTile, assets.TorchLight, new Vector2(4000, 4000), spriteBatch)); // Hack
        }

        protected override void Update(GameTime gameTime)
        {
            controls.Update(Keyboard.GetState());
            if (controls.WasPressed(Control.Pause)) paused = !paused;
            if (!paused) world.Update((float)gameTime.ElapsedGameTime.TotalSeconds / expectedFrameTime, controls, cam);
            base.Update(gameTime);
        }

        int ms = 0;
        protected override void Draw(GameTime gameTime)
        {
            ms += gameTime.ElapsedGameTime.Milliseconds;

            world.Draw(spriteBatch, prePostProcess, effects, cam.GetTransform(GraphicsDevice.Viewport));

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
 
            postProcessEffect.Parameters["AlphaFade"].SetValue(world.FadeAmount);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, postProcessEffect);
            spriteBatch.Draw(prePostProcess, new Vector2(0, 0), Color.Black);
            spriteBatch.End();
        }
    }
}
