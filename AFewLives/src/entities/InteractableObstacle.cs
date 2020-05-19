using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    abstract class InteractableObstacle : Obstacle, Interactable
    {
        public InteractableObstacle(Texture2D tex, Vector2 pos, bool inSpiritRealm=false) 
            : base(tex, pos, inSpiritRealm) { }
        public InteractableObstacle(Texture2D tex, Vector2 pos, Rectangle hitbox, bool inSpiritRealm=false) 
            : base(tex, pos, hitbox, inSpiritRealm) { }
        public InteractableObstacle(Sprite sprite, Vector2 pos, Rectangle hitbox, bool inSpiritRealm=false)
            : base(sprite, pos, hitbox, inSpiritRealm) { }
        public abstract void InteractWith();
    }
}
