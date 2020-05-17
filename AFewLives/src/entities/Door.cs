using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AFewLives.Entities
{
    class Door : InteractableObstacle
    {
        public Door LeadsTo { get; set; }
        private World world;
        private Room containingRoom;
        public Door(Texture2D tex, Vector2 pos, Room containingRoom) : base(tex, pos)
        {
            this.containingRoom = containingRoom;
        }

        public void LinkDoor(Door leadsTo, World world)
        {
            LeadsTo = leadsTo;
            this.world = world;
        }

        public override void InteractWith()
        {
            world.ActiveRoom = LeadsTo.containingRoom;
            world.Player.Pos = LeadsTo.Pos;
        }
    }
}
