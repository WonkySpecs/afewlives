using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace AFewLives.Entities
{
    class Button : Entity
    {
        private readonly Collection<Activatable> targets;

        public Button(Sprite sprite, Vector2 pos, Collection<Activatable> targets) : base(sprite, pos) 
        {
            this.targets = targets;
        }

        public void Press()
        {
            foreach (Activatable a in targets)
            {
                a.Activate();
            }
        }
    }
}
