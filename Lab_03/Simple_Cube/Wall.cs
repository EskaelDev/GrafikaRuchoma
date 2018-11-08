using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Simple_Cube
{
    public class Wall
    {
        public Polygon polygonTop { get; set; }
        public Polygon polygonBottom { get; set; }

        public Wall(Polygon polygonTop, Polygon polygonBottom)
        {
            this.polygonTop = polygonTop;
            this.polygonBottom = polygonBottom;
        }

        public Wall(Polygon polygonTop, Polygon polygonBottom, Color color)
        {
            this.polygonTop = polygonTop;
            this.polygonBottom = polygonBottom;
        }
    }
}
