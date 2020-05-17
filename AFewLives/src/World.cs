using Microsoft.Xna.Framework;
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
        public Room ActiveRoom { get; set; }

        private readonly static float transitionLength = 10;
        private float transitionElapsed = 0;
        private TransitionState transitionState = TransitionState.None;
        private Door transitioningTo;
        public float FadeAmount {
            get {
                if (transitionState == TransitionState.None) return 1f;
                return transitionState == TransitionState.FadingIn ? transitionElapsed / transitionLength
                                                                   : 1 - (transitionElapsed / transitionLength);
            }
        }

        public World(EntityFactory entityFactory)
        {
            Player = entityFactory.Player(new Vector2(400, 200));
            roomFactory = new RoomFactory(entityFactory);
            Room room1 = roomFactory.Room1();
            Room room2 = roomFactory.Room2();
            Room room3 = roomFactory.Room3();
            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);
            LinkDoors(room1.doors[0], room2.doors[0]);
            LinkDoors(room2.doors[1], room3.doors[0]);
            ActiveRoom = rooms[0];
        }

        private void LinkDoors(Door d1, Door d2)
        {
            d1.LinkDoor(d2, this);
            d2.LinkDoor(d1, this);
        }

        public void Update(float delta, Controls inputs, Camera2D cam)
        {
            Player.Update(delta, inputs, ActiveRoom);
            ActiveRoom.Update(delta, Player);
            cam.pos = Player.Pos;

            if (transitionState != TransitionState.None)
            {
                transitionElapsed += delta;
                if (transitionElapsed > transitionLength)
                {
                    transitionElapsed = 0;
                    if (transitionState == TransitionState.FadingOut)
                    {
                        transitionState = TransitionState.FadingIn;
                        ActiveRoom = transitioningTo.containingRoom;
                        Player.Pos = transitioningTo.Pos;
                    }
                    else
                    {
                        transitionState = TransitionState.None;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ActiveRoom.Draw(spriteBatch, Player.IsGhost);
            Player.Draw(spriteBatch);
        }

        public void MoveTo(Door door)
        {
            transitionState = TransitionState.FadingOut;
            transitioningTo = door;
        }
    }

    enum TransitionState
    {
        None, FadingIn, FadingOut
    }
}
