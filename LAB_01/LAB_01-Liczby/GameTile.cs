using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LAB_01_Liczby
{
    class GameTile
    {
        public Rectangle position { get; set; }
        public Texture2D texture { get; set; }
        public int rotation { get; set; }
    }
}
