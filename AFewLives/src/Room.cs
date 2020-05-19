using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class Room
    {
        public readonly List<Obstacle> walls = new List<Obstacle>();
        public readonly List<InteractableObstacle> interactables = new List<InteractableObstacle>();
        public readonly List<Spikes> spikes = new List<Spikes>();
        public readonly List<MovingPlatform> platforms = new List<MovingPlatform>();
        public readonly List<Door> doors = new List<Door>();

        public readonly List<Entity> thingsInSpiritRealm = new List<Entity>();
        public readonly List<Entity> solidThings = new List<Entity>();
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
            foreach (MovingPlatform p in platforms)
            {
                p.Update(delta, player);
            }
            if (!player.IsGhost)
            {
                foreach(Spikes spike in spikes)
                {
                    spike.Update(player);
                }
            }
        }

        public void DrawSolidThings(SpriteBatch spriteBatch)
        {
            foreach (Obstacle o in solidThings)
            {
                o.Draw(spriteBatch, Color.DarkBlue);
            }
        }
 
        public void DrawSpiritThings(SpriteBatch spriteBatch)
        {
            Console.WriteLine(thingsInSpiritRealm.Count);
            foreach (Obstacle o in thingsInSpiritRealm)
            {
                o.Draw(spriteBatch, Color.DarkBlue);
            }
        }

        public void PartitionThings()
        {
            List<Obstacle> all = new List<Obstacle>(walls);
            all.AddRange(interactables);
            all.AddRange(platforms);
            all.AddRange(doors);
            all.AddRange(spikes);
            foreach(Obstacle e in all)
            {
                (e.inSpiritRealm ? thingsInSpiritRealm : solidThings).Add(e);
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
            List<Activatable> leverTargets = new List<Activatable>(someRetractables);

            room.spikes.Add(entityFactory.Spikes(new Vector2(250, 750), new Vector2(450, 50)));

            List<Vector2> path = new List<Vector2>() {
                new Vector2(100, 300),
                new Vector2(500, 300),
                new Vector2(200, 600),
            };
            MovingPlatform platform = entityFactory.MovingPlatform(path, new Vector2(50, 50));
            room.platforms.Add(platform);
            leverTargets.Add(platform);

            room.interactables.Add(entityFactory.Lever(new Vector2(200, 742), leverTargets, false));

            room.doors.Add(entityFactory.Door(new Vector2(400, 284), room));

            room.walls.Add(entityFactory.SpiritWall(new Vector2(750, 300), new Vector2(100, 100)));
            room.PartitionThings();
            return room;
        }

        public Room Room2()
        {
            Room room = new Room();
            Vector2 hWallSize = new Vector2(1500, 300);
            room.walls.Add(entityFactory.Wall(new Vector2(-100, -100), hWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(-100, 800), hWallSize));
            room.doors.Add(entityFactory.Door(new Vector2(100, 784), room));
            room.doors.Add(entityFactory.Door(new Vector2(150, 784), room));
            room.PartitionThings();
            return room;
        }

        public Room Room3()
        {
            Room room = new Room();
            Vector2 hWallSize = new Vector2(1500, 300);
            room.walls.Add(entityFactory.Wall(new Vector2(-100, -100), hWallSize));
            room.walls.Add(entityFactory.Wall(new Vector2(-100, 800), hWallSize));
            room.doors.Add(entityFactory.Door(new Vector2(100, 784), room));
            room.PartitionThings();
            return room;
        }
    }
}
