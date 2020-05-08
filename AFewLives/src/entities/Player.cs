﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Player : Entity
    {
        public bool OnGround { get; set; }

        public Player(Sprite sprite) : base(sprite, new Vector2(200, 200), new Rectangle(0, 0, 16, 16)) 
        {
            OnGround = true;
        }

        public void Update(GameTime delta, KeyboardState inputs, List<Wall> obstacles)
        {
            base.Update(delta);
            _vel.X = 0;
            _vel.X += inputs.IsKeyDown(Keys.D) ? 0.2f : 0;
            _vel.X -= inputs.IsKeyDown(Keys.A) ? 0.2f : 0;

            spriteState = _vel.X > 0 ? SpriteState.WalkingRight
                        : _vel.X < 0 ? SpriteState.WalkingLeft
                                     : SpriteState.Neutral;

            if (OnGround && inputs.IsKeyDown(Keys.Space))
            {
                _vel.Y = -0.6f;
            }
            else
            {
                _vel.Y += 0.02f;
            }

            Vector2 newPos = _vel * delta.ElapsedGameTime.Milliseconds + _pos;
            Vector2 correction = PositionCorrection(newPos, obstacles);
            OnGround = correction.Y  < 0;
            _pos = newPos + correction;
            if (correction.X != 0) { _vel.X = 0; }
            if (correction.Y != 0) { _vel.Y = 0; }
        }

        private Vector2 PositionCorrection(Vector2 projectedPos, List<Wall> obstacles)
        {
            Vector2 correction = new Vector2();
            RectangleF projectedHb = RectangleF.FromPointAndOffset(projectedPos, staticHitbox);
            foreach (Wall w in obstacles)
            {
                Vector2 relVel = _vel - w.Vel;
                RectangleF collision = w.CollisionWith(projectedHb);
                if (collision.Width > 0 && collision.Height > 0)
                {
                    if (collision.Width < collision.Height)
                    {
                        correction.X = relVel.X > 0 ? -collision.Width : collision.Width;
                    }
                    else
                    {
                        correction.Y = relVel.Y > 0 ? -collision.Height : collision.Height;
                    }
                }
            }
            return correction;
        }
    }
}
