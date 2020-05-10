using Microsoft.Xna.Framework;

namespace AFewLives.Entities
{
    abstract class InteractableEntity : Entity, Interactable
    {
        public InteractableEntity(Sprite sprite, Vector2 pos, Rectangle hitbox) : base(sprite, pos, hitbox) { }
        public abstract void InteractWith();
    }
}
