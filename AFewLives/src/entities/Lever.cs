using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Lever : InteractableObstacle
    {
        private readonly List<Activatable> targets;
        private bool on;

        public Lever(Sprite sprite, Vector2 pos, Rectangle hitbox, List<Activatable> targets, bool initialState) : base(sprite, pos, hitbox) 
        {
            this.targets = targets;
            on = initialState;
            spriteState = on ? SpriteState.Activated : SpriteState.Deactivated;
        }

        public override void InteractWith()
        {
            on = !on;
            spriteState = on ? SpriteState.Activated : SpriteState.Deactivated;
            foreach (Activatable t in targets)
            {
                t.Activate();
            }
        }
    }
}
