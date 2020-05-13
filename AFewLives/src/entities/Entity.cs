using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AFewLives.Entities
{
    class Entity
    {
        public Vector2 Pos { get => _pos; }
        protected Vector2 _pos;
        public Vector2 Vel { get => _vel; }
        protected Vector2 _vel;

        private readonly Sprite sprite;
        protected readonly Rectangle staticHitbox;
        protected SpriteState spriteState = SpriteState.Neutral;

        public RectangleF Hitbox
        {
            get => RectangleF.FromPointAndOffset(_pos, staticHitbox);
        }

        protected Entity(Sprite sprite, Vector2 pos, Rectangle hitbox)
        {
            this.sprite = sprite;
            this.staticHitbox = hitbox;
            this._vel = new Vector2(0, 0);
            this._pos = pos;
        }

        protected Entity(Sprite sprite, Vector2 pos)
            : this(sprite, pos, new Rectangle(0, 0, (int)sprite.Size().X, (int)sprite.Size().Y)) { }

        public virtual void Update(float delta)
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

        public bool CollidesWith(Entity e)
        {
            RectangleF collision = CollisionWith(e.Hitbox);
            return collision.Width > 0 && collision.Height > 0;
        }

        public RectangleF CollisionWith(RectangleF hb2)
        {
            RectangleF hb1 = Hitbox;
            float lowerTop = Math.Max(hb1.Y, hb2.Y);
            float righterLeft = Math.Max(hb1.X, hb2.X);
            float lefterRight = Math.Min(hb1.Right, hb2.Right);
            float higherBottom = Math.Min(hb1.Bottom, hb2.Bottom);
            return new RectangleF(righterLeft, lowerTop, lefterRight - righterLeft, higherBottom - lowerTop);
        }
    }
}
