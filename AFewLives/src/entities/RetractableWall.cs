﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace AFewLives.Entities
{
    class RetractableWall : Obstacle, Activatable
    {
        private readonly Vector2 fullSize;
        private readonly Vector2 retractedSize;
        private readonly float retractTime;
        private float extension;
        private State state;
        private Vector2 Size { get => (fullSize - retractedSize) * extension / retractTime + retractedSize; }

        public RetractableWall(Texture2D tex, Vector2 retractedSize, Vector2 fullSize, Vector2 pos,
                               bool inSpiritRealm, bool startRetracted, float retractTime) 
            : base(tex, startRetracted ? retractedSize : fullSize, pos, inSpiritRealm)
        {
            extension = startRetracted ? 0 : retractTime;
            this.retractTime = retractTime;
            state = State.Static;
            this.fullSize = fullSize;
            this.retractedSize = retractedSize;
        }

        public override void Update(float delta)
        {
            if (state == State.Extending)
            {
                extension += delta;
                if (extension >= retractTime)
                {
                    extension = retractTime;
                    state = State.Static;
                }
            }
            else if (state == State.Retracting)
            {
                extension -= delta;
                if (extension <= 0)
                {
                    extension = 0;
                    state = State.Static;
                }
            }
            Vector2 hbSize = Size;
            staticHitbox.Width = (int)Math.Round(hbSize.X);
            staticHitbox.Height = (int)Math.Round(hbSize.Y);
        }

        public override void Draw(SpriteBatch batch, Color tint)
        {
            Rectangle frame = sprite.CurrentFrame;
            Vector2 spriteFrac = Size / fullSize;
            frame.Width = (int)Math.Round(frame.Width * spriteFrac.X);
            frame.Height = (int)Math.Round(frame.Height * spriteFrac.Y);
            batch.Draw(sprite.SpriteSheet, Pos, frame, tint);
        }

        public void Activate()
        {
            switch (state)
            {
                case State.Extending:
                    state = State.Retracting;
                    break;
                case State.Retracting:
                    state = State.Extending;
                    break;
                default:
                    state = extension == retractTime ? State.Retracting : State.Extending;
                    break;
            }
        }
    }

    enum State
    {
        Retracting, Static, Extending
    }
}
