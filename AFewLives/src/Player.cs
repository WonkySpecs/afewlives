using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AFewLives
{
    class Player : Entity
    {
        private Vector2 vel;
        private bool isOnGround;

        public Player(Sprite sprite) : base(sprite, new Vector2(200, 200), new Rectangle(0, 0, 16, 16)) 
        {
            isOnGround = true;
        }

        public void Update(GameTime delta, KeyboardState inputs)
        {
            base.Update(delta);
            if (!isOnGround)
            {
                vel.Y += 0.2f;
            }
            else
            {
                if(inputs.IsKeyDown(Keys.W))
                {
                    vel.Y = -4f;
                    isOnGround = false;
                }
                vel.X = 0;
                vel.X += inputs.IsKeyDown(Keys.D) ? 0.2f : 0;
                vel.X -= inputs.IsKeyDown(Keys.A) ? 0.2f : 0;
            }

            vel.Y *= 0.8f;
            Pos += vel * delta.ElapsedGameTime.Milliseconds;

            if (Pos.Y >= 300)
            {
                isOnGround = true;
                Pos = new Vector2(Pos.X, 300);
                vel.Y = 0f;
            }

            if (vel.X > 0.02f)
            {
                spriteState = SpriteState.WalkingRight;
            }
            else if (vel.X < -0.02f)
            {
                spriteState = SpriteState.WalkingLeft;
            }
            else
            {
                spriteState = SpriteState.Neutral;
            }
        }
    }
}
