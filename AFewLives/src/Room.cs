using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class Room
    {
        public readonly List<Wall> walls = new List<Wall>();

        public void Draw(SpriteBatch spriteBatch, bool drawInvisible)
        {
            foreach (Wall wall in walls)
            {
                if(!wall.inSpiritRealm || drawInvisible)
                {
                    wall.Draw(spriteBatch, drawInvisible ? Color.White : Color.DarkBlue);
                }
            }
        }
    }

    class RoomFactory
    {
        private readonly AssetStore assets;
        public RoomFactory(AssetStore assets)
        {
            this.assets = assets;
        }

        public Room Room1()
        {
            Room room = new Room();
            Vector2 wallSize = new Vector2(200, 200);
            Vector2 smallWallSize = new Vector2(50, 50);
            room.walls.Add(new Wall(assets.Gradient(wallSize), wallSize, new Vector2(200, 300)));
            room.walls.Add(new Wall(assets.Gradient(smallWallSize), smallWallSize, new Vector2(200, 250)));
            room.walls.Add(new Wall(assets.Gradient(smallWallSize), smallWallSize, new Vector2(150, 200)));
            room.walls.Add(new Wall(assets.Gradient(smallWallSize), smallWallSize, new Vector2(250, 150)));
            room.walls.Add(new Wall(assets.Gradient(smallWallSize), smallWallSize, new Vector2(500, 500)));
            return room;
        }
    }
}
