using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    class Obstacle : Entity
    {
        public bool inSpiritRealm;
        public Obstacle(Texture2D tex, Vector2 size, Vector2 pos, bool inSpiritRealm) : base(new StaticSprite(tex),
                                                                                             pos,
                                                                                             new Rectangle(0, 0, 
                                                                                                           (int)size.X, (int)size.Y))
        {
            this.inSpiritRealm = inSpiritRealm;
        }

        public Obstacle(Texture2D tile, Vector2 size, Vector2 pos) : this(tile, size, pos, false) { }
    }
}
