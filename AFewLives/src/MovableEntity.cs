using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    class MovableEntity : Entity
    {
        public Vector2 Vel { get; set; }

        protected MovableEntity(Sprite sprite, Vector2 pos) : base(sprite, pos)
        {
            Vel = new Vector2(0, 0);
        }

        public new void Update(GameTime delta, SpriteState drawState)
        {
            base.Update(delta, drawState);
            Pos += Vel;
        }
    }
}
