using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class Room
    {
        public readonly List<Obstacle> walls = new List<Obstacle>();
        public readonly List<InteractableEntity> interactables = new List<InteractableEntity>();

        public void Update(float delta)
        {
            foreach(InteractableEntity e in interactables)
            {
                e.Update(delta);
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool drawInvisible)
        {
            foreach (Obstacle wall in walls)
            {
                if(!wall.inSpiritRealm || drawInvisible)
                {
                    wall.Draw(spriteBatch, drawInvisible ? Color.White : Color.DarkBlue);
                }
            }

            foreach (InteractableEntity i in interactables)
            {
                i.Draw(spriteBatch, Color.White);
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

            room.interactables.Add(entityFactory.Lever(new Vector2(200, 742), new List<Toggleable>(), false));
            return room;
        }
    }
}
