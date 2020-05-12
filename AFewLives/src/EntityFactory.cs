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

        public StaticObstacle Wall(Vector2 pos, Vector2 size)
        {
            return new StaticObstacle(assets.Gradient(size), size, pos);
        }

        public Lever Lever(Vector2 pos, List<Toggleable> targets, bool inSpiritRealm)
        {
            return new Lever(assets.LeverSprite(), pos, new Rectangle(0, 0, 16, 8), targets);
        }
    }
}
