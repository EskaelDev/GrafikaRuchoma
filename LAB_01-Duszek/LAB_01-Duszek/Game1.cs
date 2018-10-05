using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LAB_01_Duszek
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ghost;
        Texture2D ghostFoot;
        Texture2D street;
        Rectangle backgroundRec;
        List<Rectangle> ghostRec;

        KeyboardState ks;
        int points;
        Random rng;

        int RaindomTime()
        {
            return rng.Next(0, 2000);
        }

        int[] RandomPos()
        {
            int[] pos = new int[2];
            pos[0] = rng.Next(0, GraphicsDevice.Viewport.Width - ghost.Width);
            pos[1] = rng.Next(0, GraphicsDevice.Viewport.Height - ghost.Height);

            return pos;
        }



        string TitleBuilder()
        {
            return "Score: " + Convert.ToString(points);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            Window.AllowUserResizing = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
            points = 0;
            Window.Title = TitleBuilder();
            rng = new Random();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ghost = Content.Load<Texture2D>("Graphics/ghost");
            ghostFoot = Content.Load<Texture2D>("Graphics/ghost-foot");
            street = Content.Load<Texture2D>("Graphics/street");
            backgroundRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                

            // TODO: Add your update logic here
            backgroundRec.Width = GraphicsDevice.Viewport.Width;
            backgroundRec.Height = GraphicsDevice.Viewport.Height;

            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Enter))
                this.Exit();

            

            Window.Title = TitleBuilder();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(street, backgroundRec, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
