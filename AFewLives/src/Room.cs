using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using AFewLives.Entities;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class Room
    {
        public readonly List<Wall> walls = new List<Wall>();
        public readonly List<InteractableEntity> interactables = new List<InteractableEntity>();

        public void Update(GameTime delta)
        {
            foreach(InteractableEntity e in interactables)
            {
                e.Update(delta);
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool drawInvisible)
        {
            foreach (Wall wall in walls)
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
        private readonly AssetStore assets;
        public RoomFactory(AssetStore assets)
        {
            this.assets = assets;
        }

        public Room Room1()
        {
            Room room = new Room();
            Vector2 hWallSize = new Vector2(1500, 300);
            room.walls.Add(new Wall(assets.Gradient(hWallSize), hWallSize, new Vector2(-100, -100)));
            room.walls.Add(new Wall(assets.Gradient(hWallSize), hWallSize, new Vector2(-100, 800)));

            Vector2 vWallSize = new Vector2(300, 1200);
            room.walls.Add(new Wall(assets.Gradient(vWallSize), vWallSize, new Vector2(-200, -100)));
            room.walls.Add(new Wall(assets.Gradient(vWallSize), vWallSize, new Vector2(1000, -100)));

            Vector2 pitSideSize = new Vector2(250, 100);
            room.walls.Add(new Wall(assets.Gradient(pitSideSize), pitSideSize, new Vector2(0, 750)));
            room.walls.Add(new Wall(assets.Gradient(pitSideSize), pitSideSize, new Vector2(700, 750)));

            Vector2 cliffSize = new Vector2(200, 300);
            room.walls.Add(new Wall(assets.Gradient(cliffSize), cliffSize, new Vector2(900, 500)));

            room.interactables.Add(new Lever(assets.LeverSprite(false),
                                   new Vector2(200, 742),
                                   new Rectangle(0, 0, 16, 8),
                                   new List<Toggleable>(),
                                   false));
            return room;
        }
    }
}
