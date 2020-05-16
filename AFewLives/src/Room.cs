using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class Room
    {
        public readonly List<Obstacle> walls = new List<Obstacle>();
        public readonly List<InteractableObstacle> interactables = new List<InteractableObstacle>();
        public readonly List<Spikes> spikes = new List<Spikes>();

        public void Update(float delta, Player player)
        {
            foreach(InteractableObstacle e in interactables)
            {
                e.Update(delta);
            }
            foreach (Obstacle wall in walls)
            {
                wall.Update(delta);
            }
            if (!player.IsGhost)
            {
                foreach(Spikes spike in spikes)
                {
                    spike.Update(player);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool playerIsDead)
        {
            List<Obstacle> all = new List<Obstacle>(walls);
            all.AddRange(spikes);
            all.AddRange(interactables);
            Color tint = playerIsDead ? Color.White : Color.DarkBlue;
            foreach (Obstacle o in all)
            {
                if(!o.inSpiritRealm || playerIsDead)
                {
                    o.Draw(spriteBatch, tint);
                }
            }
        }
    }

    class RoomFactory
    {
        private readonly EntityFactory entityFactory;
        public RoomFactory(EntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        public Room Room1()
        {
            Room room = new Room();
            Vector2 hWallSize = new Vector2(1500, 300);
            room.walls.Add(entityFactory.Wall(new Vector2(-100, -100), hWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(-100, 800), hWallSize));

            Vector2 vWallSize = new Vector2(300, 1200);
            room.walls.Add(entityFactory.Wall(new Vector2(-200, -100), vWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(1000, -100), vWallSize));

            Vector2 pitSideSize = new Vector2(250, 100);
            room.walls.Add(entityFactory.Wall(new Vector2(0, 750), pitSideSize));
            room.walls.Add(entityFactory.Wall(new Vector2(700, 750), pitSideSize));

            room.walls.Add(entityFactory.Wall(new Vector2(900, 500), new Vector2(200, 300)));

            List<RetractableWall> someRetractables = new List<RetractableWall>();
            for (int y = 300; y <= 750; y += 50)
            {
                someRetractables.Add(entityFactory.RetractableWall(new Vector2(250, y), new Vector2(0, 20), new Vector2(450, 20), y / 50));
            }
            someRetractables.Add(entityFactory.RetractableWall(new Vector2(150, 450), new Vector2(20, 50), new Vector2(20, 350), 100));
            room.walls.AddRange(someRetractables);
            room.interactables.Add(entityFactory.Lever(new Vector2(200, 742), new List<Activatable>(someRetractables), false));

            room.spikes.Add(entityFactory.Spikes(new Vector2(250, 750), new Vector2(200, 50)));
            return room;
        }
    }
}
