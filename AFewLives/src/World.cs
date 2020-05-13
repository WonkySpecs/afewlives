using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using AFewLives.Entities;
using System;

namespace AFewLives
{
    class World
    {
        public readonly Player Player;
        private readonly List<Room> rooms = new List<Room>();
        private readonly RoomFactory roomFactory;
        private Room activeRoom;

        public World(EntityFactory entityFactory)
        {
            Player = entityFactory.Player(new Vector2(400, 400));
            roomFactory = new RoomFactory(entityFactory);
            rooms.Add(roomFactory.Room1());
            activeRoom = rooms[0];
        }

        public void Update(float delta, KeyboardState keyboardState)
        {
            Player.Update(delta, keyboardState, activeRoom);
            activeRoom.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            activeRoom.Draw(spriteBatch, false);
        }
    }
}
