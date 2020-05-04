using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AFewLives
{
    class AnimatedSprite : Sprite
    {
        private readonly Texture2D spriteSheet;
        private readonly Dictionary<SpriteState, Animation> animations;
        private SpriteState prevState;
        private int timerMS;

        public AnimatedSprite(Texture2D spriteSheet, Dictionary<SpriteState, Animation> animations)
        {
            this.spriteSheet = spriteSheet;
            this.animations = animations;
            prevState = SpriteState.Neutral;
            timerMS = 0;
        }

        public void Update(GameTime delta, SpriteState curState)
        {
            timerMS = (timerMS + delta.ElapsedGameTime.Milliseconds) % animations[curState].TotalTimeMS;
            if (curState != prevState)
            {
                timerMS = 0;
                prevState = curState;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos, Color tint)
        {
            spriteBatch.Draw(spriteSheet, pos, animations[prevState].GetFrame(timerMS), tint);
        }

        public Vector2 Size()
        {
            return animations[prevState].Size;
        }
    }

    class Animation
    {
        private readonly Rectangle[] frames;
        private readonly int frameDurationMS;

        public int TotalTimeMS => frameDurationMS * frames.Length;
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
            this.frameDurationMS = frameDurationMS;
        }

        public Rectangle GetFrame(int time) 
        {
            return frames[time / frameDurationMS];
        }
    }

    class AnimationFactory
    {
        public Dictionary<SpriteState, Animation> PlayerAnimations()
        {
            return new Dictionary<SpriteState, Animation>
            {
                { SpriteState.Neutral, new Animation(97, new Vector2(16, 16), 3, 80) }
            };
        }
    }
}
