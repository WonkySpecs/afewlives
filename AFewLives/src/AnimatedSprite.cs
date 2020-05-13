using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace AFewLives
{
    class AnimatedSprite : Sprite
    {
        private readonly Texture2D spriteSheet;
        private readonly Dictionary<SpriteState, Animation> animations;
        private SpriteState prevState;
        private float timer;

        public AnimatedSprite(Texture2D spriteSheet, Dictionary<SpriteState, Animation> animations, SpriteState initialState)
        {
            this.spriteSheet = spriteSheet;
            this.animations = animations;
            prevState = initialState;
            timer = 0;
        }

        public AnimatedSprite(Texture2D spriteSheet, Dictionary<SpriteState, Animation> animations)
            : this(spriteSheet, animations, SpriteState.Neutral) { }

        public void Update(float delta, SpriteState curState)
        {
            timer = (timer + delta) % animations[curState].TotalTime;
            if (curState != prevState)
            {
                timer = 0;
                prevState = curState;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Color tint)
        {
            spriteBatch.Draw(spriteSheet, pos, animations[prevState].GetFrame(timer), tint);
        }

        public Vector2 Size()
        {
            return animations[prevState].Size;
        }
    }

    class Animation
    {
        private readonly Rectangle[] frames;
        private readonly int frameDuration;

        public int TotalTime => frameDuration * frames.Length;
        public Vector2 Size => new Vector2(frames[0].Width, frames[0].Height);

        public Animation(int y, Vector2 frameSize, int numFrames, int frameDurationMS) 
        {
            int w = (int)frameSize.X;
            int h = (int)frameSize.Y;
            frames = new Rectangle[numFrames];
            for (int n = 0; n < numFrames; n++)
            {
                frames[n] = new Rectangle(n * w, y, w, h);
            }
            this.frameDuration = frameDurationMS;
        }

        public Rectangle GetFrame(float time) 
        {
            return frames[(int)Math.Round(time) / frameDuration];
        }
    }

    class AnimationFactory
    {
        public Dictionary<SpriteState, Animation> PlayerAnimations()
        {
            return new Dictionary<SpriteState, Animation>
            {
                { SpriteState.Neutral, new Animation(0, new Vector2(16, 16), 1, 10) },
                { SpriteState.WalkingRight, new Animation(16, new Vector2(16, 16), 3, 10) },
                { SpriteState.WalkingLeft, new Animation(32, new Vector2(16, 16), 3, 10) },
            };
        }

        public Dictionary<SpriteState, Animation> LeverAnimations()
        {
            return new Dictionary<SpriteState, Animation>
            {
                { SpriteState.Activated, new Animation(0, new Vector2(16, 8), 1, 100) },
                { SpriteState.Deactivated, new Animation(8, new Vector2(16, 8), 1, 100) },
            };

        }
    }
}
