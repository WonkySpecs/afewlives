using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Lever : InteractableEntity
    {
        private readonly List<Toggleable> targets;
        private bool on;
        private int timer;

        public Lever(Sprite sprite, Vector2 pos, Rectangle hitbox, List<Toggleable> targets, bool initialState) : base(sprite, pos, hitbox) 
        {
            this.targets = targets;
            on = initialState;
            spriteState = initialState ? SpriteState.Activated : SpriteState.Deactivated;
        }

        public override void Update(GameTime delta)
        {
            base.Update(delta);
            timer += delta.ElapsedGameTime.Milliseconds;
        }

        public override void InteractWith()
        {
            if (timer < 200) return;
            timer = 0;
            on = !on;
            Console.WriteLine(on);
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
