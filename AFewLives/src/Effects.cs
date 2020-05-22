using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFewLives
{
    class Effects
    {
        public readonly Effect solid;
        public readonly Effect spirit;
        public readonly Effect bg;

        public Effects(Effect solid, Effect spirit, Effect bg)
        {
            this.solid = solid;
            this.spirit = spirit;
            this.bg = bg;
        }
    }
}
