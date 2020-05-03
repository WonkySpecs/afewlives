using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class Entity
    {
        public Vector2 Pos { get; set; }
        private readonly Sprite sprite;

        protected Entity(Sprite sprite, Vector2 pos)
        {
            this.sprite = sprite;
            Pos = pos;
        }

        protected virtual void Update(GameTime delta, SpriteState spriteState)
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
