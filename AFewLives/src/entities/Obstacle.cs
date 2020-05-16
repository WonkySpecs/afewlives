using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    class Obstacle : Entity
    {
        public bool inSpiritRealm;
        public Obstacle(Sprite sprite, Vector2 pos, Rectangle hitbox, bool inSpiritRealm=false) : base(sprite, pos, hitbox)
        {
            this.inSpiritRealm = inSpiritRealm;
        }

        public Obstacle(Texture2D tex, Vector2 pos, Rectangle hitbox, bool inSpiritRealm = false) : this(new Sprite(tex), pos, hitbox) { }
        public Obstacle(Texture2D tex, Vector2 size, Vector2 pos, bool inSpiritRealm=false) : this(new Sprite(tex),
                                                                                             pos,
                                                                                             new Rectangle(0, 0, 
                                                                                                           (int)size.X, (int)size.Y)) { }

    }
}
