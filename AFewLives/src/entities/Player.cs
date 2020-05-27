using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AFewLives.Entities
{
    class Player : Entity
    {
        private readonly static float speed = 2.5f;
        public bool OnGround { get; set; }
        public bool IsGhost { get; set; }

        public Player(Sprite sprite, Vector2 pos) : base(sprite, pos, new Rectangle(0, 0, 16, 16)) 
        {
            OnGround = true;
            IsGhost = false;
        }

        public void Update(float delta, Controls input, Room room)
        {
            base.Update(delta);
            if (input.WasPressed(Control.ToggleLife)) IsGhost = !IsGhost;
            _vel.X = 0;
            _vel.X += input.IsDown(Control.MoveRight) ? speed : 0;
            _vel.X -= input.IsDown(Control.MoveLeft) ? speed : 0;

            spriteState = _vel.X > 0 ? SpriteState.WalkingRight
                        : _vel.X < 0 ? SpriteState.WalkingLeft
                                     : SpriteState.Neutral;

            if (IsGhost)
            {
                if (input.IsDown(Control.MoveUp))
                {
                    _vel.Y = -speed;
                }
                else if (input.IsDown(Control.MoveDown))
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
                if (OnGround && input.IsDown(Control.Jump))
                {
                    _vel.Y = -8f;
                }
                _vel.Y += Physics.GRAVITY * delta;
            }
            Vector2 newPos = _vel * delta + _pos;
            Vector2 correction = Physics.PositionCorrection(_pos, _vel, delta, staticHitbox, room.Solids);
            OnGround = correction.Y  < 0;
            _pos = newPos + correction;
            if (correction.X != 0) { _vel.X = 0; }
            if (correction.Y != 0) { _vel.Y = 0; }

            // Interaction
            if (input.WasPressed(Control.Interact))
            {
                List<InteractableObstacle> interactables = new List<InteractableObstacle>();
                foreach(InteractableObstacle o in room.interactables)
                {
                    if (IsGhost && o.inSpiritRealm || !IsGhost && ! o.inSpiritRealm)
                    {
                        interactables.Add(o);
                    }
                }
                interactables.AddRange(room.doors);
                foreach (InteractableObstacle i in interactables)
                {
                    if (CollidesWith(i))
                    {
                        i.InteractWith();
                        break;
                    }
                }
            }
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
                        correction.X = collision.X > Pos.X ? -collision.Width : collision.Width;
                    }
                    else
                    {
                        correction.Y = collision.Y > Pos.Y ? -collision.Height : collision.Height;
                    }
                }
            }
            return correction;
        }

        public void Die()
        {
            // TODO: sound, animation, and (probably elsewhere) tint change
            Console.WriteLine("rip");
            IsGhost = true;
        }

        public void MoveWithoutCollision(Vector2 d)
        {
            _pos += d;
        }
    }
}
