using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class Entity
    {
        public Vector2 Pos { get; set; }
        private readonly Sprite sprite;
        private readonly Rectangle hitbox;
        protected SpriteState spriteState = SpriteState.Neutral;

        public Rectangle Hitbox
        {
            get => new Rectangle((int)Pos.X + hitbox.X, (int)Pos.Y + hitbox.Y, hitbox.Width, hitbox.Height);
        }

        protected Entity(Sprite sprite, Vector2 pos, Rectangle hitbox)
        {
            this.sprite = sprite;
            Pos = pos;
            this.hitbox = hitbox;
        }

        protected virtual void Update(GameTime delta)
        {
            sprite.Update(delta, spriteState);
        }

        public void Draw(SpriteBatch batch, Color tint)
        {
            sprite.Draw(batch, Pos, tint);
        }

        public void Draw(SpriteBatch batch)
        {
            Draw(batch, Color.White);
        }
    }
}
