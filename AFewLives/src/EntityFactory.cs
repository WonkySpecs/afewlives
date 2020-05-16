using AFewLives.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace AFewLives
{
    class EntityFactory
    {
        private readonly AssetStore assets;

        public EntityFactory(AssetStore assets)
        {
            this.assets = assets;
        }

        public Player Player(Vector2 pos)
        {
            return new Player(assets.PlayerSprite, pos);
        }

        public Obstacle Wall(Vector2 pos, Vector2 size)
        {
            return new Obstacle(assets.Stripy(size), size, pos);
        }

        public Lever Lever(Vector2 pos, List<Activatable> targets, bool inSpiritRealm)
        {
            return new Lever(assets.LeverSprite(), pos, new Rectangle(0, 0, 16, 8), targets, true);
        }

        public RetractableWall RetractableWall(Vector2 pos, Vector2 retractedSize, Vector2 fullSize, int retractTime)
        {
            return new RetractableWall(assets.Stripy(fullSize), retractedSize, fullSize, pos, false, false, retractTime);
        }

        public Spikes Spikes(Vector2 pos, Vector2 size)
        {
            Texture2D tex = assets.SpikesTexture((int)size.X);
            if (size.Y != tex.Height)
            {
                Console.WriteLine("Built a spikes with height different to the sprite height");
            }
            return new Spikes(tex, size, pos);
        }
    }
}
