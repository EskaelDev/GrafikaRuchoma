using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LAB_01_Liczby
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        // Graphic vars
        Texture2D backgroud;
        Texture2D numberTiles;
        Texture2D questionTile;
        Texture2D selectedTile;

        Rectangle tileRect;
        Rectangle selectedRect;
        Rectangle backgroundRect;

        const int tileSize = 50;
        const int selectedSize = 100;
        const int startPositionX = 200;
        const int startPositionY = 10;
        int tileGapX;
        int tileGapY;

        List<GameTile> tiles;
        List<MenuTile> menuTiles;

        MouseState mState;
        MouseState lastMouseState;
        Point mPos;



        void RotateTile(GameTile tile_)
        {
            if (tile_.texture != questionTile)
            {
                if (tile_.rotation == 3)
                {
                    tile_.rotation = 0;
                }
                else
                    tile_.rotation++;
            }
        }

        void ColourTiles()
        {

            // The active state from the last frame is now old
            lastMouseState = mState;


            mState = Mouse.GetState();
            mPos = new Point(mState.X, mState.Y);

            foreach (var tile in tiles)
            {
                if (tile.position.Contains(mPos) && (lastMouseState.LeftButton == ButtonState.Released && mState.LeftButton == ButtonState.Pressed))
                {
                    if (tile.texture == selectedTile)
                    {
                        RotateTile(tile);
                    }
                    tile.texture = selectedTile;
                }
            }
        }

        void SelectTile()
        {
            mState = Mouse.GetState();
            mPos = new Point(mState.X, mState.Y);
            foreach (var tile in menuTiles)
            {
                if (tile.position.Contains(mPos) && mState.LeftButton == ButtonState.Pressed)
                {
                    selectedTile = tile.texture;
                }
            }
        }



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferHeight = 600;
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

            backgroud = Content.Load<Texture2D>("graphics/background");
            questionTile = Content.Load<Texture2D>("graphics/question");
            numberTiles = Content.Load<Texture2D>("graphics/numbers");
            selectedTile = questionTile;



            tileRect = new Rectangle(tileSize, tileSize, tileSize, tileSize);
            selectedRect = new Rectangle(20, 20, selectedSize, selectedSize);
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            tiles = new List<GameTile>();
            menuTiles = new List<MenuTile>();

            tileGapY = 140;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Rectangle cropRect = new Rectangle(j * 100, i * 100, 100, 100);
                    Texture2D cropTexture = new Texture2D(GraphicsDevice, 100, 100);
                    Color[] data = new Color[cropRect.Width * cropRect.Height];
                    numberTiles.GetData(0, cropRect, data, 0, data.Length);
                    cropTexture.SetData(data);

                    MenuTile mtile = new MenuTile();
                    mtile.position = new Rectangle(20, tileGapY, tileSize, tileSize);
                    mtile.texture = cropTexture;
                    menuTiles.Add(mtile);
                    tileGapY += tileSize;

                }
            }


            tileGapX = startPositionX;
            tileGapY = startPositionY;
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    GameTile tile = new GameTile();
                    tile.position = new Rectangle(tileGapX, tileGapY, tileSize, tileSize);
                    tile.rotation = 0;
                    tile.texture = questionTile;
                    tiles.Add(tile);
                    tileGapX += tileSize;
                }
                tileGapX = startPositionX;
                tileGapY += tileSize;

            }

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            ColourTiles();
            SelectTile();

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


            spriteBatch.Draw(backgroud, backgroundRect, Color.White);
            spriteBatch.Draw(selectedTile, selectedRect, Color.White);

            foreach (var tile_ in tiles)
            {

                /*spriteBatch.Draw(tile_.texture, tile_.position, null, Color.White,
                    (float)Math.PI / 2 * tile_.rotation, new Vector2(36, 36), SpriteEffects.None, 0f);*/
                spriteBatch.Draw(
                        tile_.texture, 
                        new Vector2(tile_.position.X + (tileSize / 2), tile_.position.Y + (tileSize / 2)),
                        null,
                        Color.White, 
                        (float)(Math.PI / 2) * tile_.rotation, 
                        new Vector2(tile_.position.Width,tile_.position.Height), 
                        0.5f, 
                        SpriteEffects.None, 
                        0f);
            }

            foreach (var tile_ in menuTiles)
            {
                spriteBatch.Draw(tile_.texture, tile_.position, Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
