using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simple_Cube
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix worldMatrix, viewMatrix, projectrionMatrix;

        BasicEffect basicEffect;                    //programy cieniujace
        float angleX = 0.0f, angleY = 0.0f, scale = 1.0f, rotatnionZ = 2.0f;

        private Cube cube, cube2;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            cube = new Cube(new Vector3(-1, 1, 1), Color.Red, Color.Blue, Color.Yellow, Color.Green, Color.White, Color.Crimson, Color.Azure, Color.Cyan, Color.Orange, Color.DarkOliveGreen, Color.LightPink, Color.DarkOrchid, 2);



            cube2 = new Cube(new Vector3(-2, 0, 0), Color.White, 1);


            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // rotation
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                angleY += 0.02f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                angleY -= 0.02f;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                angleX += 0.02f;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                angleX -= 0.02f;
            // zoom
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                scale += 0.02f;
            if (Keyboard.GetState().IsKeyDown(Keys.L))
                scale -= 0.02f;


            cube.TopWall.polygonTop.color2 = Color.Black;
            cube.TopWall.polygonTop.color3 = Color.Black;
            cube.TopWall.polygonTop.color1 = Color.Black;

            cube.FrontWall.polygonTop.color2 = Color.Black;
            cube.FrontWall.polygonTop.color3 = Color.Black;
            cube.FrontWall.polygonTop.color1 = Color.Black;

            cube.TopWall.polygonTop.color2 = Color.Black;
            cube.TopWall.polygonTop.color3 = Color.Black;
            cube.TopWall.polygonTop.color1 = Color.Black;

            Matrix scaleMatrix = Matrix.CreateScale(scale);
            rotatnionZ += 1f;
            cube.Rotate(rotatnionZ);

            worldMatrix = Matrix.Identity * scaleMatrix;


            // polozenie kamery
            viewMatrix = Matrix.CreateLookAt(
                new Vector3(0.0f, 0.0f, 4.0f),
                Vector3.Zero,
                Vector3.Up);


            // obrot kamery
            viewMatrix = Matrix.CreateRotationX(angleX) * Matrix.CreateRotationY(angleY) * viewMatrix;

            projectrionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(50),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                1000.0f);


            // update parametrow shadera
            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectrionMatrix;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            basicEffect.CurrentTechnique.Passes[0].Apply(); // uruchamia shader

            cube.DrawCube(GraphicsDevice);

            //cube2.DrawCube(GraphicsDevice);


            base.Draw(gameTime);
        }
    }
}
