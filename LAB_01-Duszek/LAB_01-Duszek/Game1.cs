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
        List<Ghost> ghosts;

        KeyboardState ks;
        int points, newGhostTimer;
        Random rng;
        MouseState mouseState;
        private Point mousePosition;

        private const int GhostSize = 50;



        int RandomGhostX()
        {
            return rng.Next(0, GraphicsDevice.Viewport.Width - GhostSize);
        }
        int RandomGhostY()
        {
            return rng.Next(0, GraphicsDevice.Viewport.Height - GhostSize);
        }

        void AddNewGhost()
        {
            Ghost ghost = new Ghost();
            ghost.AppearanceTime = rng.Next(0, 2000);
            ghost.IsHitted = false;
            Rectangle ghostRectangle = new Rectangle(RandomGhostX(), RandomGhostY(), GhostSize, GhostSize);
            ghost.Position = ghostRectangle;

            ghosts.Add(ghost);
        }

        void UpdateGhostsAppearanceTime(int deltaTime)
        {
            for (int i = ghosts.Count - 1; i >= 0; i--)
            {
                ghosts[i].AppearanceTime -= deltaTime;

                if (ghosts[i].AppearanceTime <= 0 && ghosts[i].IsHitted == false)
                    ghosts.Remove(ghosts[i]);
            }
        }


        void CheckGhostClick()
        {
            mouseState = Mouse.GetState();
            mousePosition = new Point(mouseState.X, mouseState.Y);

            for (int i = ghosts.Count - 1; i >= 0; i--)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && ghosts[i].Position.Contains(mousePosition))
                {
                    ghosts[i].IsHitted = true;
                    points++;
                }
            }
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

            ghosts = new List<Ghost>();
            newGhostTimer = 0;

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

            if (newGhostTimer <= 0)
            {
                AddNewGhost();
                newGhostTimer = 500;
            }

            newGhostTimer -= gameTime.ElapsedGameTime.Milliseconds;

            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
                Exit();

            Window.Title = TitleBuilder();

            CheckGhostClick();
            UpdateGhostsAppearanceTime(gameTime.ElapsedGameTime.Milliseconds);


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

            foreach (var ghost_ in ghosts)
            {
                if (ghost_.IsHitted)
                {
                    spriteBatch.Draw(ghostFoot, ghost_.Position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(ghost, ghost_.Position, Color.White);
                }

            }



            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
