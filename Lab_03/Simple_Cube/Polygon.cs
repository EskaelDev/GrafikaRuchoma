using Microsoft.Xna.Framework;

namespace Simple_Cube
{
    public class Polygon
    {
        public Vector3 top1 { get; }
        public Vector3 top2 { get; }
        public Vector3 top3 { get; }

        public Color color1 { get; set; }
        public Color color2 { get; set; }
        public Color color3 { get; set; }

        public Polygon(Vector3 top1, Vector3 top2, Vector3 top3)
        {
            this.top1 = top1;
            this.top2 = top2;
            this.top3 = top3;
            this.color1 = Color.White;
            this.color2 = this.color3 = this.color1;
        }

        public Polygon(Vector3 top1, Vector3 top2, Vector3 top3, Color color1)
        {
            this.top1 = top1;
            this.top2 = top2;
            this.top3 = top3;
            this.color1 = color1;
            this.color2 = this.color3 = this.color1;
        }

        public Polygon(Vector3 top1, Vector3 top2, Vector3 top3, Color color1, Color color2, Color color3)
        {
            this.top1 = top1;
            this.top2 = top2;
            this.top3 = top3;
            this.color1 = color1;
            this.color2 = color2;
            this.color3 = color3;
        }


    }
}
