using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives
{
    public interface Sprite
    {
        void Draw(SpriteBatch spriteBatch, Vector2 pos, Color tint);
        void Update(GameTime delta, SpriteState state);
        Vector2 Size();
    }

    public enum SpriteState
    {
        Neutral, WalkingLeft, WalkingRight,
        Activated, Deactivated
    }
}
