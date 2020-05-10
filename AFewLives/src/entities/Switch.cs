using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Button : InteractableEntity
    {
        private readonly List<Activatable> targets;
        private bool pressed;

        public Button(Sprite sprite, Vector2 pos, Rectangle hitbox, List<Activatable> targets) : base(sprite, pos, hitbox) 
        {
            this.targets = targets;
            pressed = false;
        }

        public override void InteractWith()
        {
            if (pressed) return;

            foreach (Activatable a in targets)
            {
                a.Activate();
            }
            pressed = true;
        }
    }
}
