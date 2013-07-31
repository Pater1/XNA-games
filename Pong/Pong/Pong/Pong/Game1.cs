using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D stickMan1Texture, stickMan2Texture, ballTexture;//what each sprite will look like

        Rectangle stickMan1Rectangle, stickMan2Rectangle, ballRectangle;//sprite 'hit boxes'

        int stickManSpeed = 10, ballSpeed = 5;//speed of our sprites

        int stickMan1X, stickMan1Y, stickMan2X, stickMan2Y, ballX, ballY;//place of our sprites

        int stickMan1Width, stickMan1Height, stickMan2Width, stickMan2Height, ballWidth, ballHeight;// height and width of our sprites

        bool ballMoveUp = false, ballMoveRight = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

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

            //define all our texutre
            stickMan1Texture = this.Content.Load<Texture2D>("stickMan1");
            stickMan2Texture = this.Content.Load<Texture2D>("stickMan2");
            ballTexture = this.Content.Load<Texture2D>("ball");

            //get width and height of our sprites
            stickMan1Width = stickMan1Texture.Width/2;
            stickMan2Width = stickMan2Texture.Width/4;
            ballWidth = ballTexture.Width/2;

            stickMan1Height = stickMan1Texture.Height/2;
            stickMan2Height = stickMan2Texture.Height/4;
            ballHeight = ballTexture.Height/2;

            //combine rectangles with textures
            stickMan1Rectangle = new Rectangle(stickMan1X, stickMan1Y, stickMan1Width, stickMan1Height);
            stickMan2Rectangle = new Rectangle(stickMan2X, stickMan2Y, stickMan2Width, stickMan2Height);
            ballRectangle = new Rectangle(ballX, ballY, ballWidth, ballHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            stickMan2Y = (GraphicsDevice.Viewport.Width - stickMan2Width);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //start drawing sprites
            spriteBatch.Begin();

            spriteBatch.Draw(stickMan2Texture, stickMan2Rectangle, Color.White);
            spriteBatch.Draw(stickMan1Texture, stickMan1Rectangle, Color.White);
            spriteBatch.Draw(ballTexture, ballRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}