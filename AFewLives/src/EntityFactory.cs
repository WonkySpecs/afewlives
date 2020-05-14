using AFewLives.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            return new Obstacle(assets.Gradient(size), size, pos);
        }

        public Lever Lever(Vector2 pos, List<Activatable> targets, bool inSpiritRealm)
        {
            return new Lever(assets.LeverSprite(), pos, new Rectangle(0, 0, 16, 8), targets, true);
        }

        public RetractableWall RetractableWall(Vector2 pos, Vector2 startSize, int retractTime)
        {
            return new RetractableWall(assets.Gradient(startSize), new Vector2(0, startSize.Y), startSize, pos, false, false, retractTime);
        }
    }
}
