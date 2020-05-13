using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Player : Entity
    {
        private readonly static float speed = 2.5f;
        public bool OnGround { get; set; }
        public bool IsGhost { get; set; }
        private bool wasInteracting;

        public Player(Sprite sprite, Vector2 pos) : base(sprite, pos, new Rectangle(0, 0, 16, 16)) 
        {
            OnGround = true;
            IsGhost = false;
            wasInteracting = false;
        }

        public void Update(float delta, KeyboardState inputs, Room room)
        {
            base.Update(delta);
            _vel.X = 0;
            _vel.X += inputs.IsKeyDown(Keys.D) ? speed : 0;
            _vel.X -= inputs.IsKeyDown(Keys.A) ? speed : 0;

            spriteState = _vel.X > 0 ? SpriteState.WalkingRight
                        : _vel.X < 0 ? SpriteState.WalkingLeft
                                     : SpriteState.Neutral;

            if (IsGhost)
            {
                if (inputs.IsKeyDown(Keys.W))
                {
                    _vel.Y = -speed;
                }
                else if (inputs.IsKeyDown(Keys.S))
                {
                    _vel.Y = speed;
                }
                else
                {
                    _vel.Y = 0;
                }
            }
            else
            {
                if (OnGround && inputs.IsKeyDown(Keys.Space))
                {
                    _vel.Y = -8f;
                }
                _vel.Y += 0.45f;

                bool interacting = inputs.IsKeyDown(Keys.E);
                if (interacting && !wasInteracting)
                {
                    foreach (InteractableEntity i in room.interactables)
                    {
                        if (CollidesWith(i))
                        {
                            i.InteractWith();
                            break;
                        }
                    }
                }
                wasInteracting = interacting;
            }
            if (inputs.IsKeyDown(Keys.Q)) IsGhost = !IsGhost;

            Vector2 newPos = _vel * delta + _pos;
            Vector2 correction = PositionCorrection(newPos, room.walls);
            OnGround = correction.Y  < 0;
            _pos = newPos + correction;
            if (correction.X != 0) { _vel.X = 0; }
            if (correction.Y != 0) { _vel.Y = 0; }
        }

        private Vector2 PositionCorrection(Vector2 projectedPos, List<Obstacle> obstacles)
        {
            Vector2 correction = new Vector2();
            RectangleF projectedHb = RectangleF.FromPointAndOffset(projectedPos, staticHitbox);
            foreach (Obstacle w in obstacles)
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
