using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace AFewLives.Entities
{
    class Button : Entity, Interactable
    {
        private readonly Collection<Activatable> targets;
        private bool pressed;

        public Button(Sprite sprite, Vector2 pos, Collection<Activatable> targets) : base(sprite, pos) 
        {
            this.targets = targets;
            pressed = false;
        }

        public void InteractWith()
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
