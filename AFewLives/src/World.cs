using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class World
    {
        public readonly Entities.Player Player;

        public World(AssetStore assets, AnimationFactory animationFactory)
        {
            Player = new Entities.Player(new AnimatedSprite(assets.PlayerSpriteSheet, animationFactory.PlayerAnimations()));
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Player.Update(gameTime, keyboardState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }
    }
}
