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

        private readonly static float fadeLength = 10;
        private float fadeElapsed = 0;
        private Fade fadeState = Fade.None;
        private Door transitioningTo;
        public float FadeAmount {
            get {
                if (fadeState == Fade.None) return 1f;
                return fadeState == Fade.FadingIn ? fadeElapsed / fadeLength
                                                  : 1 - (fadeElapsed / fadeLength);
            }
        }

        public float ColorDrain { get => colorDrainElapsed / colorDrainLength; }
        private float colorDrainElapsed = 0;
        private float colorDrainLength = 20;

        public World(EntityFactory entityFactory, RoomBackground rb)
        {
            roomFactory = new RoomFactory(entityFactory);
            Room room1 = roomFactory.Room1(rb);
            Room room2 = roomFactory.Room2(rb);
            Room room3 = roomFactory.Room3(rb);
            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);
            LinkDoors(room1.doors[0], room2.doors[0]);
            LinkDoors(room2.doors[1], room3.doors[0]);
            ActiveRoom = rooms[2];
            Player = entityFactory.Player(ActiveRoom.doors[0].Pos);
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
            ActiveRoom.SetCameraAim(cam, Player);
            cam.Update(delta);
            if (inputs.WasPressed(Control.ToggleZoom)) cam.targetZoom = cam.targetZoom > 1 ? 1 : 5;

            if (fadeState != Fade.None)
            {
                fadeElapsed += delta;
                if (fadeElapsed > fadeLength)
                {
                    fadeElapsed = 0;
                    if (fadeState == Fade.FadingOut)
                    {
                        fadeState = Fade.FadingIn;
                        ActiveRoom = transitioningTo.containingRoom;
                        Player.Pos = transitioningTo.Pos;
                        cam.pos = Player.Pos;
                    }
                    else
                    {
                        fadeState = Fade.None;
                    }
                }
            }

            if (Player.IsGhost && ColorDrain < colorDrainLength)
            {
                colorDrainElapsed = Math.Min(colorDrainLength, colorDrainElapsed + delta);
            }
            if (!Player.IsGhost && ColorDrain > 0)
            {
                colorDrainElapsed = Math.Max(0, colorDrainElapsed - delta);
            }
        }

        public void DrawBackground(SpriteBatch sb, Effect effect, Camera2D cam)
        {
        }

        public void Draw(SpriteBatch spriteBatch, RenderTarget2D target, Effects effects, Matrix transform)
        {
            effects.spirit.Parameters["visibility"].SetValue(ColorDrain);
            effects.solid.Parameters["colorDrain"].SetValue(ColorDrain);

            ActiveRoom.DrawBackground(spriteBatch, target, effects.bg, transform);

            // Using immediate to make shaders simpler.
            // Probably doesn't change performance as everything uses different textures anyway
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, effects.spirit, transform);
            ActiveRoom.DrawSpiritThings(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, effects.solid, transform);
            ActiveRoom.DrawSolidThings(spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void MoveTo(Door door)
        {
            fadeState = Fade.FadingOut;
            transitioningTo = door;
        }
    }

    enum Fade
    {
        None, FadingIn, FadingOut
    }
}
