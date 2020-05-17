﻿using Microsoft.Xna.Framework;
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
            Player = entityFactory.Player(new Vector2(400, 200));
            roomFactory = new RoomFactory(entityFactory);
            rooms.Add(roomFactory.Room1());
            activeRoom = rooms[0];
        }

        public void Update(float delta, Controls inputs, Camera2D cam)
        {
            Player.Update(delta, inputs, activeRoom);
            activeRoom.Update(delta, Player);
            cam.pos = Player.Pos;
            cam.rot += 0.01f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            activeRoom.Draw(spriteBatch, Player.IsGhost);
        }
    }
}
