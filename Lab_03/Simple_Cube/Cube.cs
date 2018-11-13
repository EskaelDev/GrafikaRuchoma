using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simple_Cube
{
    class Cube
    {
        VertexPositionColor[] userPrimitives;       //tablica z geometria modelu

        public Wall FrontWall { get; set; }
        public Wall LeftWall { get; set; }
        public Wall RightWall { get; set; }
        public Wall BackWall { get; set; }
        public Wall TopWall { get; set; }
        public Wall DownWall { get; set; }
        private int size;

        public VertexPositionColor[] tops;
        private Vector3 FrontWallTopLeftPosition;

        public Cube(Vector3 FrontWallTopLeftPosition, Color CubeColor, int size)
        {
            tops = new VertexPositionColor[36];
            this.size = size;
            userPrimitives = new VertexPositionColor[36];
            SetWalls(FrontWallTopLeftPosition, CubeColor, this.size);
            LoadWallsToPrimitives();

        }

        public Cube(Vector3 FrontWallTopLeftPosition, Color FrontWallTopColor, Color FrontWallBottomColor, Color LeftWallTopColor, Color LeftWallBottomColor, Color RightWallTopColor,
                    Color RightWallBottomColor, Color BackWallTopColor, Color BackWallBottomColor, Color TopWallTopColor, Color TopWallBottomColor, Color DownWallTopColor, Color DownWallBottomColor, int size)
        {
            this.FrontWallTopLeftPosition = FrontWallTopLeftPosition;
            userPrimitives = new VertexPositionColor[36];
            this.size = size;
            tops = new VertexPositionColor[36];

            SetWalls(FrontWallTopLeftPosition, FrontWallTopColor, this.size);
            SetColors(FrontWallTopColor, FrontWallBottomColor,
                LeftWallTopColor, LeftWallBottomColor,
                RightWallTopColor, RightWallBottomColor,
                BackWallTopColor, BackWallBottomColor,
                TopWallTopColor, TopWallBottomColor,
                DownWallTopColor, DownWallBottomColor);


            LoadWallsToPrimitives();

        }


        void SetWalls(Vector3 FrontWallTopLeftPosition, Color CubeColor, int size)
        {
            this.FrontWallTopLeftPosition = FrontWallTopLeftPosition;
            Polygon ftop = new Polygon(FrontWallTopLeftPosition, FrontWallTopLeftPosition + new Vector3(size, 0, 0), FrontWallTopLeftPosition + new Vector3(size, -size, 0), CubeColor);
            Polygon fbottom = new Polygon(FrontWallTopLeftPosition, FrontWallTopLeftPosition + new Vector3(size, -size, 0), FrontWallTopLeftPosition + new Vector3(0, -size, 0), CubeColor);
            FrontWall = new Wall(ftop, fbottom);

            Polygon ltop = new Polygon(FrontWallTopLeftPosition + new Vector3(0, 0, -size), FrontWallTopLeftPosition, FrontWallTopLeftPosition + new Vector3(0, -size, 0), CubeColor);
            Polygon lbottom = new Polygon(FrontWallTopLeftPosition + new Vector3(0, 0, -size), FrontWallTopLeftPosition + new Vector3(0, -size, 0), FrontWallTopLeftPosition + new Vector3(0, -size, -size), CubeColor);
            LeftWall = new Wall(ltop, lbottom);

            Polygon rtop = new Polygon(FrontWallTopLeftPosition + new Vector3(size, 0, -size), FrontWallTopLeftPosition + new Vector3(size, 0, 0), FrontWallTopLeftPosition + new Vector3(size, -size, -size), CubeColor);
            Polygon rbottom = new Polygon(FrontWallTopLeftPosition + new Vector3(size, 0, 0), FrontWallTopLeftPosition + new Vector3(size, -size, -size), FrontWallTopLeftPosition + new Vector3(size, -size, 0), CubeColor);
            RightWall = new Wall(rtop, rbottom);

            Polygon btop = new Polygon(FrontWallTopLeftPosition + new Vector3(0, 0, -size), FrontWallTopLeftPosition + new Vector3(size, 0, -size), FrontWallTopLeftPosition + new Vector3(0, -size, -size), CubeColor);
            Polygon bbottom = new Polygon(FrontWallTopLeftPosition + new Vector3(size, 0, -size), FrontWallTopLeftPosition + new Vector3(size, -size, -size), FrontWallTopLeftPosition + new Vector3(0, -size, -size), CubeColor);
            BackWall = new Wall(btop, bbottom);

            Polygon ttop = new Polygon(FrontWallTopLeftPosition + new Vector3(0, 0, -size), FrontWallTopLeftPosition + new Vector3(size, 0, -size), FrontWallTopLeftPosition + new Vector3(size, 0, 0), CubeColor);
            Polygon tbottom = new Polygon(FrontWallTopLeftPosition + new Vector3(0, 0, -size), FrontWallTopLeftPosition + new Vector3(size, 0, 0), FrontWallTopLeftPosition, CubeColor);
            TopWall = new Wall(ttop, tbottom);

            Polygon dtop = new Polygon(FrontWallTopLeftPosition + new Vector3(0, -size, -size), FrontWallTopLeftPosition + new Vector3(size, -size, -size), FrontWallTopLeftPosition + new Vector3(size, -size, 0), CubeColor);
            Polygon dbottom = new Polygon(FrontWallTopLeftPosition + new Vector3(0, -size, -size), FrontWallTopLeftPosition + new Vector3(size, -size, 0), FrontWallTopLeftPosition + new Vector3(0, -size, 0), CubeColor);
            DownWall = new Wall(dtop, dbottom);

        }

        void SetColors(Color FrontWallTopColor, Color FrontWallBottomColor, Color LeftWallTopColor, Color LeftWallBottomColor, Color RightWallTopColor,
                        Color RightWallBottomColor, Color BackWallTopColor, Color BackWallBottomColor, Color TopWallTopColor, Color TopWallBottomColor, Color DownWallTopColor, Color DownWallBottomColor)
        {
            FrontWall.polygonTop.color1 = FrontWall.polygonTop.color2 = FrontWall.polygonTop.color3 = FrontWallTopColor;
            FrontWall.polygonBottom.color1 = FrontWall.polygonBottom.color2 = FrontWall.polygonBottom.color3 = FrontWallBottomColor;

            LeftWall.polygonTop.color1 = LeftWall.polygonTop.color2 = LeftWall.polygonTop.color3 = LeftWallTopColor;
            LeftWall.polygonBottom.color1 = LeftWall.polygonBottom.color2 = LeftWall.polygonBottom.color3 = LeftWallBottomColor;

            RightWall.polygonTop.color1 = RightWall.polygonTop.color2 = RightWall.polygonTop.color3 = RightWallTopColor;
            RightWall.polygonBottom.color1 = RightWall.polygonBottom.color2 = RightWall.polygonBottom.color3 = RightWallBottomColor;

            BackWall.polygonTop.color1 = BackWall.polygonTop.color2 = BackWall.polygonTop.color3 = BackWallTopColor;
            BackWall.polygonBottom.color1 = BackWall.polygonBottom.color2 = BackWall.polygonBottom.color3 = BackWallBottomColor;

            TopWall.polygonTop.color1 = TopWall.polygonTop.color2 = TopWall.polygonTop.color3 = TopWallTopColor;
            TopWall.polygonBottom.color1 = TopWall.polygonBottom.color2 = TopWall.polygonBottom.color3 = TopWallBottomColor;

            DownWall.polygonTop.color1 = DownWall.polygonTop.color2 = DownWall.polygonTop.color3 = DownWallTopColor;
            DownWall.polygonBottom.color1 = DownWall.polygonBottom.color2 = DownWall.polygonBottom.color3 = DownWallBottomColor;
        }


        public void DrawCube(GraphicsDevice GraphicsDevice)
        {
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                userPrimitives,                 // referencja do talicy wierzcholkow,
                0,                              // offset pierwszego wierzcholka
                12);
        }

        void LoadWallsToPrimitives()
        {
            #region Down
            // PolyTop
            userPrimitives[35] = tops[35] = new VertexPositionColor(DownWall.polygonTop.top1, DownWall.polygonTop.color1);
            userPrimitives[34] = tops[34] = new VertexPositionColor(DownWall.polygonTop.top2, DownWall.polygonTop.color2);
            userPrimitives[33] = tops[33] = new VertexPositionColor(DownWall.polygonTop.top3, DownWall.polygonTop.color3);
            // PolyBottom
            userPrimitives[32] = tops[32] = new VertexPositionColor(DownWall.polygonBottom.top1, DownWall.polygonBottom.color3);
            userPrimitives[31] = tops[31] = new VertexPositionColor(DownWall.polygonBottom.top2, DownWall.polygonBottom.color2);
            userPrimitives[30] = tops[30] = new VertexPositionColor(DownWall.polygonBottom.top3, DownWall.polygonBottom.color1);
            #endregion Down

            #region Top
            // PolyTop
            userPrimitives[29] = tops[29] = new VertexPositionColor(TopWall.polygonTop.top3, TopWall.polygonTop.color3);
            userPrimitives[28] = tops[28] = new VertexPositionColor(TopWall.polygonTop.top2, TopWall.polygonTop.color2);
            userPrimitives[27] = tops[27] = new VertexPositionColor(TopWall.polygonTop.top1, TopWall.polygonTop.color1);
            //PolyBottom
            userPrimitives[26] = tops[26] = new VertexPositionColor(TopWall.polygonBottom.top3, TopWall.polygonBottom.color3);
            userPrimitives[25] = tops[25] = new VertexPositionColor(TopWall.polygonBottom.top2, TopWall.polygonBottom.color2);
            userPrimitives[24] = tops[24] = new VertexPositionColor(TopWall.polygonBottom.top1, TopWall.polygonBottom.color1);
            #endregion

            #region Back
            // PolyTop
            userPrimitives[23] = tops[23] = new VertexPositionColor(BackWall.polygonTop.top1, BackWall.polygonTop.color1);
            userPrimitives[22] = tops[22] = new VertexPositionColor(BackWall.polygonTop.top2, BackWall.polygonTop.color2);
            userPrimitives[21] = tops[21] = new VertexPositionColor(BackWall.polygonTop.top3, BackWall.polygonTop.color3);
            // PolyBottom       // PolyBottom
            userPrimitives[20] = tops[20] = new VertexPositionColor(BackWall.polygonBottom.top1, BackWall.polygonBottom.color1);
            userPrimitives[19] = tops[19] = new VertexPositionColor(BackWall.polygonBottom.top2, BackWall.polygonBottom.color2);
            userPrimitives[18] = tops[18] = new VertexPositionColor(BackWall.polygonBottom.top3, BackWall.polygonBottom.color3);
            #endregion


            #region Right
            // PolyTop
            userPrimitives[17] = tops[17] = new VertexPositionColor(RightWall.polygonTop.top1, RightWall.polygonTop.color1);
            userPrimitives[16] = tops[16] = new VertexPositionColor(RightWall.polygonTop.top2, RightWall.polygonTop.color2);
            userPrimitives[15] = tops[15] = new VertexPositionColor(RightWall.polygonTop.top3, RightWall.polygonTop.color3);
            // PolyBottom       // PolyBottom
            userPrimitives[14] = tops[14] = new VertexPositionColor(RightWall.polygonBottom.top3, RightWall.polygonBottom.color3);
            userPrimitives[13] = tops[13] = new VertexPositionColor(RightWall.polygonBottom.top2, RightWall.polygonBottom.color2);
            userPrimitives[12] = tops[12] = new VertexPositionColor(RightWall.polygonBottom.top1, RightWall.polygonBottom.color1);
            #endregion


            #region Left
            // PolyTop
            userPrimitives[11] = tops[11] = new VertexPositionColor(LeftWall.polygonTop.top3, LeftWall.polygonTop.color3);
            userPrimitives[10] = tops[10] = new VertexPositionColor(LeftWall.polygonTop.top2, LeftWall.polygonTop.color2);
            userPrimitives[9] = tops[9] = new VertexPositionColor(LeftWall.polygonTop.top1, LeftWall.polygonTop.color1);

            userPrimitives[8] = tops[8] = new VertexPositionColor(LeftWall.polygonBottom.top3, LeftWall.polygonBottom.color3);
            userPrimitives[7] = tops[7] = new VertexPositionColor(LeftWall.polygonBottom.top2, LeftWall.polygonBottom.color2);
            userPrimitives[6] = tops[6] = new VertexPositionColor(LeftWall.polygonBottom.top1, LeftWall.polygonBottom.color1);
            #endregion

            #region Front
            // PolyTop
            userPrimitives[5] = tops[5] = new VertexPositionColor(FrontWall.polygonTop.top3, FrontWall.polygonTop.color3);
            userPrimitives[4] = tops[4] = new VertexPositionColor(FrontWall.polygonTop.top2, FrontWall.polygonTop.color2);
            userPrimitives[3] = tops[3] = new VertexPositionColor(FrontWall.polygonTop.top1, FrontWall.polygonTop.color1);
            // PolyBottom       // PolyBottom
            userPrimitives[2] = tops[2] = new VertexPositionColor(FrontWall.polygonBottom.top3, FrontWall.polygonBottom.color3);
            userPrimitives[1] = tops[1] = new VertexPositionColor(FrontWall.polygonBottom.top2, FrontWall.polygonBottom.color2);
            userPrimitives[0] = tops[0] = new VertexPositionColor(FrontWall.polygonBottom.top1, FrontWall.polygonBottom.color1);
            #endregion


        }


        public void Rotate(float angle)
        {
            //var rotationCenter = new Vector3(0.5f, 0.5f, 0.5f);

            //Matrix transformation = Matrix.CreateTranslation(-rotationCenter)

            //                        * Matrix.CreateRotationY(rotation)
            //                        * Matrix.CreateTranslation(FrontWallTopLeftPosition);


            Matrix rotation = Matrix.Identity;
            Matrix transform = Matrix.CreateFromAxisAngle(rotation.Up, MathHelper.ToRadians(angle));
            //transform.Translation = new Vector3(0, 0, 0);

            for (int i = 0; i < tops.Length; i++)
            {
                userPrimitives[i].Position = Vector3.Transform(tops[i].Position, transform);
            }
            

        }


    }
}
