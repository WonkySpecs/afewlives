using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using AFewLives.Entities;

namespace AFewLives
{
    class World
    {
        public readonly Player Player;
        private readonly List<Room> rooms = new List<Room>();
        private readonly RoomFactory roomFactory;
        private Room activeRoom;

        public World(AssetStore assets)
        {
            Player = new Player(assets.PlayerSprite,
                                new Vector2(400, 400));
            roomFactory = new RoomFactory(assets);
            rooms.Add(roomFactory.Room1());
            activeRoom = rooms[0];
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Player.Update(gameTime, keyboardState, activeRoom);
            activeRoom.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            activeRoom.Draw(spriteBatch, false);
        }
    }
}
