using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class World
    {
        public readonly Player Player;

        public World(AssetStore assets, AnimationFactory animationFactory)
        {
            Player = new Player(new AnimatedSprite(assets.PlayerSpriteSheet, animationFactory.PlayerAnimations()));
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime, SpriteState.Neutral);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
        }
    }
}
