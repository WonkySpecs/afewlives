
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    class Spikes : Obstacle
    {
        public Spikes(Texture2D tex, Vector2 size, Vector2 pos) : base(tex, size, pos) { }
        public void Update(Player player)
        {
            if (player.Vel.Y > 0 && CollidesWith(player))
            {
                player.Die();
            }
        }
    }
}
