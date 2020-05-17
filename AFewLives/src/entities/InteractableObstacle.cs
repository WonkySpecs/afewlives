using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    abstract class InteractableObstacle : Obstacle, Interactable
    {
        public InteractableObstacle(Texture2D tex, Vector2 pos) : base(tex, pos) { }
        public InteractableObstacle(Texture2D tex, Vector2 pos, Rectangle hitbox) : base(tex, pos, hitbox) { }
        public InteractableObstacle(Sprite sprite, Vector2 pos, Rectangle hitbox) : base(sprite, pos, hitbox) { }
        public abstract void InteractWith();
    }
}
