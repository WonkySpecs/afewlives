using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

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
            controls.Update(Keyboard.GetState());
            if (controls.WasPressed(Control.Pause)) paused = !paused;
            if (!paused) world.Update((float)gameTime.ElapsedGameTime.TotalSeconds / expectedFrameTime, controls, cam);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, cam.GetTransform(GraphicsDevice.Viewport));
            world.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
