using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Lever : InteractableEntity
    {
        private readonly List<Toggleable> targets;
        private bool on;

        public Lever(Sprite sprite, Vector2 pos, Rectangle hitbox, List<Toggleable> targets) : base(sprite, pos, hitbox) 
        {
            this.targets = targets;
            on = false;
            spriteState = SpriteState.Deactivated;
        }

        public override void InteractWith()
        {
            on = !on;
            spriteState = on ? SpriteState.Activated : SpriteState.Deactivated;
            foreach (Toggleable t in targets)
            {
                if (on)
                {
                    t.Activate();
                } else
                {
                    t.Deactivate();
                }
            }
        }
    }
}
