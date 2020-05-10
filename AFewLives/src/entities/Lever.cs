using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace AFewLives.Entities
{
    class Lever : Entity, Interactable
    {
        private readonly Collection<Toggleable> targets;
        private bool on;

        public Lever(Sprite sprite, Vector2 pos, Collection<Toggleable> targets, bool initialState) : base(sprite, pos) 
        {
            this.targets = targets;
            on = initialState;
        }

        public void InteractWith()
        {
            on = !on;
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
