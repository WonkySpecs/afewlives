using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FNAExtensions;
using System.Runtime.Serialization;

namespace AFewLives.Entities
{
    class Obstacle : Entity
    {
        public bool inSpiritRealm;
        protected List<Obstacle> attached = new List<Obstacle>();

        public Obstacle(Sprite sprite, Vector2 pos, Rectangle hitbox,
                        bool inSpiritRealm=false) : base(sprite, pos, hitbox)
        {
            this.inSpiritRealm = inSpiritRealm;
        }
        public Obstacle(Texture2D tex, Vector2 pos, Rectangle hitbox,
                        bool inSpiritRealm = false) 
            : this(new Sprite(tex), pos, hitbox, inSpiritRealm) { }
        public Obstacle(Texture2D tex, Vector2 size, Vector2 pos,
                        bool inSpiritRealm=false) 
            : this(new Sprite(tex), pos, size.ToRectangle(), inSpiritRealm) {}
        public Obstacle(Texture2D tex, Vector2 pos, bool inSpiritRealm=false) 
            : this(new Sprite(tex), pos, tex.Bounds, inSpiritRealm) {}

        public void Attach(Obstacle o)
        {
            attached.Add(o);
        }
    }
}
