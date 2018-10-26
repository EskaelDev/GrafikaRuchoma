using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private Texture2D ballAnimationTexture2D, backgroundTexture2D, leftPaddleTexture2D, righPaddleTexture2D, explosionAnimationTexture2D;
        private Rectangle backgroundRectangle;
        private Paddle leftPaddle, rightPaddle;
        private Ball ball;
        private SpriteFont font;
        private Score LeftScore { get; set; }
        private Score RightScore { get; set; }

        private bool play = false;




        void ResponseToInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                leftPaddle.MoveUP();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                leftPaddle.MoveDown(GraphicsDevice.Viewport.Height);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                rightPaddle.MoveUP();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                rightPaddle.MoveDown(GraphicsDevice.Viewport.Height);
            }
        }

        void CheckCollisions(GameTime gameTime)
        {
            /*            if (ball.CheckCollisonWithPaddle(leftPaddle.body))
                        {
                            leftPaddle.Hit();
                        }
                        leftPaddle.CheckIfHitted(gameTime);

                        if (ball.CheckCollisonWithPaddle(rightPaddle.body))
                        {
                            rightPaddle.Hit();
                        }*/
            if (leftPaddle.CheckCollisionWithBall( ball))
            {
                leftPaddle.Hit();
            }
            else
            if (rightPaddle.CheckCollisionWithBall( ball))
            {
                rightPaddle.Hit();
            }

            rightPaddle.CheckIfHitted(gameTime);

            if (ball.CheckCollisionWithWall(0, GraphicsDevice.Viewport.Width) == 'l')
            {
                RightScore.AddPoint();
                ball.DestroyBall(gameTime);

                play = false;
            }
            else if (ball.CheckCollisionWithWall(0, GraphicsDevice.Viewport.Width) == 'r')
            {
                LeftScore.AddPoint();
                ball.DestroyBall(gameTime);

                play = false;
            }

            ball.BounceFromWall(GraphicsDevice.Viewport.Height);
        }



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            base.Initialize();
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

            ballAnimationTexture2D = Content.Load<Texture2D>("graphics/ball-anim");
            backgroundTexture2D = Content.Load<Texture2D>("graphics/pongBackground");
            leftPaddleTexture2D = Content.Load<Texture2D>("graphics/paddle1a");
            righPaddleTexture2D = Content.Load<Texture2D>("graphics/paddle2a");
            explosionAnimationTexture2D = Content.Load<Texture2D>("graphics/explosion64");
            font = Content.Load<SpriteFont>("fonts/font");

            backgroundRectangle = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            leftPaddle = new Paddle(leftPaddleTexture2D, new Vector2(5, GraphicsDevice.Viewport.Height / 2 - leftPaddleTexture2D.Height / 2));
            rightPaddle = new Paddle(righPaddleTexture2D, new Vector2(GraphicsDevice.Viewport.Width - leftPaddleTexture2D.Width - 5, GraphicsDevice.Viewport.Height / 2 - leftPaddleTexture2D.Height / 2));
            ball = new Ball(ballAnimationTexture2D,
                            explosionAnimationTexture2D,
                            GraphicsDevice.Viewport.Width / 2 - ballAnimationTexture2D.Width / 16 / 2,
                            GraphicsDevice.Viewport.Height / 2 - ballAnimationTexture2D.Height / 5 / 2);

            LeftScore = new Score(font, new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, 30));
            RightScore = new Score(font, new Vector2(GraphicsDevice.Viewport.Width / 2 + 30, 30));
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

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                play = true;

            ball.AnimateBall(gameTime);
            if (ball.isDestroyed)
            {
                ball.AnimateExplosion(gameTime);
            }
            if (play)
            {
                CheckCollisions(gameTime);
                ball.Move();

                ResponseToInput();


                rightPaddle.CheckIfHitted(gameTime);
                leftPaddle.CheckIfHitted(gameTime);
            }

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



            spriteBatch.Draw(backgroundTexture2D, backgroundRectangle, Color.White);


            rightPaddle.Draw(spriteBatch);
            leftPaddle.Draw(spriteBatch);
            LeftScore.Draw(spriteBatch);
            RightScore.Draw(spriteBatch);

            ball.DrawBall(spriteBatch);

            if (ball.isDestroyed)
                ball.DrawExplosion(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
