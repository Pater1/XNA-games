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

namespace Pong_Reference
{
    /// <summary>
    /// Welcome to Stickman Pong!!! have fun.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D stickMan1Texture, stickMan2Texture, ballTexture;

        Rectangle stickMan1Rectangle, stickMan2Rectangle, ballRectangle;

        int stickmanSpeed = 10, ballSpeedX = 5, ballSpeedY = 5, ballSpeedLimit = 20;//sprite speeds

        int stickMan1X = 700, stickMan1Y, stickMan2X = 0, stickMan2Y, ballX, ballY;//sprite positions

        int stickMan1Score, stickMan2Score;

        int stickMan1Width, stickMan1Height, stickMan2Width, stickMan2Height, ballWidth, ballHeight;// sprite sizes

        bool ballMoveUp = false, ballMoveRight = true;

        SoundEffect clashSound, crashSound, crash1Sound, chordSound, FchordSound;

        SoundEffectInstance clashInstance, crashInstance, crash1Instance, chordInstance, FchordInstance;

        Song Maneater;

        SpriteFont Font;

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

            // load stuff from content manager
            stickMan1Texture = this.Content.Load<Texture2D>("stickman1");
            stickMan2Texture = this.Content.Load<Texture2D>("stickman2");
            ballTexture = this.Content.Load<Texture2D>("ball");

            //get widths of pics (and correct size differences)
            stickMan1Width = stickMan1Texture.Width/3;
            stickMan1Height = stickMan1Texture.Height/3;
            stickMan2Width = stickMan2Texture.Width/6;
            stickMan2Height = stickMan2Texture.Height/6;

            ballWidth = ballTexture.Width/3;
            ballHeight = ballTexture.Height/3;

            //combine textures with rectangles
            stickMan1Rectangle = new Rectangle(stickMan1X, stickMan1Y, stickMan1Width, stickMan1Height);
            stickMan2Rectangle = new Rectangle(stickMan2X, stickMan2Y, stickMan2Width, stickMan2Height);
            ballRectangle = new Rectangle(ballX + stickMan2Width, ballY, ballWidth, ballHeight);

            //Load sounds
            clashSound = this.Content.Load<SoundEffect>("clash");
            clashInstance = clashSound.CreateInstance();
            clashInstance.Volume = .5f;
            clashInstance.IsLooped = false;

            crash1Sound = this.Content.Load<SoundEffect>("crash1");
            crash1Instance = crash1Sound.CreateInstance();
            crash1Instance.Volume = .25f;
            crash1Instance.IsLooped = false;

            crashSound = this.Content.Load<SoundEffect>("crash");
            crashInstance = crash1Sound.CreateInstance();
            crashInstance.Volume = .25f;
            crashInstance.IsLooped = false;

            chordSound = this.Content.Load<SoundEffect>("chord");
            chordInstance = chordSound.CreateInstance();
            chordInstance.Volume = 1f;
            chordInstance.IsLooped = false;

            FchordSound = this.Content.Load<SoundEffect>("Fchord");
            FchordInstance = FchordSound.CreateInstance();
            FchordInstance.Volume = 1f;
            FchordInstance.IsLooped = false;



            //load font
            Font = Content.Load<SpriteFont>("SpriteFont1");

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
            //game controls and mechanics
            MouseState mouse = Mouse.GetState();
            KeyboardState keys = Keyboard.GetState();

            //button controls
            if (keys.IsKeyDown(Keys.Up)) stickMan1Rectangle.Y -= stickmanSpeed;
            if (keys.IsKeyDown(Keys.Down)) stickMan1Rectangle.Y += stickmanSpeed;

            if (keys.IsKeyDown(Keys.W)) stickMan2Rectangle.Y -= stickmanSpeed;
            if (keys.IsKeyDown(Keys.S)) stickMan2Rectangle.Y += stickmanSpeed;

            //allows to exit
            if (keys.IsKeyDown(Keys.Escape)) Exit();

            //automatic controls
            if (stickMan1Rectangle.Y < 0) stickMan1Rectangle.Y = 0;
            if (stickMan1Rectangle.Y > (GraphicsDevice.Viewport.Height - stickMan1Height)) stickMan1Rectangle.Y = (GraphicsDevice.Viewport.Height - stickMan1Height);
            if (stickMan2Rectangle.Y < 0) stickMan2Rectangle.Y = 0;
            if (stickMan2Rectangle.Y > (GraphicsDevice.Viewport.Height - stickMan2Height)) stickMan2Rectangle.Y = (GraphicsDevice.Viewport.Height - stickMan2Height);

            if (ballSpeedX > ballSpeedLimit) ballSpeedX = ballSpeedLimit;
            if (ballSpeedY > ballSpeedLimit) ballSpeedY = ballSpeedLimit;
            

            //ball functions
            if (ballMoveRight == true) ballRectangle.X += ballSpeedX;
            else ballRectangle.X -= ballSpeedX;
            if (ballMoveUp == true) ballRectangle.Y -= ballSpeedY;
            else ballRectangle.Y += ballSpeedY;

            if (ballRectangle.X < 0)
            {
                ballMoveRight = true;
                stickMan1Score += 1;
                ballRectangle.Y = stickMan2Rectangle.Y;
                ballRectangle.X = stickMan2Rectangle.Width;
                chordInstance.Play();
            }
            if (ballRectangle.X > (GraphicsDevice.Viewport.Width-ballRectangle.Height))
            {
                ballMoveRight = false;
                stickMan2Score += 1;
                ballRectangle.Y = stickMan1Rectangle.Y;
                ballRectangle.X = (GraphicsDevice.Viewport.Width - stickMan1Rectangle.Width);
                FchordInstance.Play();
            }
            if (ballRectangle.Y < 0)
            {
                ballMoveUp = false;
                clashInstance.Play();
            }
            if (ballRectangle.Y > (GraphicsDevice.Viewport.Height-ballRectangle.Height))
            {
                ballMoveUp = true;
                clashInstance.Play();
            }

            //collision detection
            if (ballRectangle.Intersects(stickMan2Rectangle))
            {
                ballMoveRight = true;
                crashInstance.Play();
                ballSpeedX +=1;
                if (keys.IsKeyDown(Keys.S))
                {
                    ballMoveUp = false;
                    ballSpeedY += stickmanSpeed;
                }
                if (keys.IsKeyDown(Keys.W))
                {
                    ballMoveUp = true;
                    ballSpeedY += stickmanSpeed;
                }
                else
                {
                    if (ballSpeedY > 1) ballSpeedY -= 1;
                }
            }
            if (ballRectangle.Intersects(stickMan1Rectangle))
            {
                ballMoveRight = false;
                crash1Instance.Play();
                ballSpeedX +=1;
                if (keys.IsKeyDown(Keys.Up))
                {
                    ballMoveUp = true;
                    ballSpeedY += stickmanSpeed;
                }
                if (keys.IsKeyDown(Keys.Down))
                {
                    ballMoveUp = false;
                    ballSpeedY +=  stickmanSpeed;
                }
                else
                {
                    if (ballSpeedY > 1) ballSpeedY -= 1;
                }
            }




            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //draw score
            Vector2 stickMan1ScoreVector = new Vector2(stickMan1Rectangle.X, 10);
            Vector2 stickMan2ScoreVector = new Vector2(stickMan2Rectangle.X, 10);


            spriteBatch.Begin();

            spriteBatch.DrawString(Font, stickMan1Score.ToString(), stickMan1ScoreVector, Color. White);
            spriteBatch.DrawString(Font, stickMan2Score.ToString(), stickMan2ScoreVector, Color.White); 

            spriteBatch.Draw(ballTexture, ballRectangle, Color.White);
            spriteBatch.Draw(stickMan1Texture, stickMan1Rectangle, Color.White);
            spriteBatch.Draw(stickMan2Texture, stickMan2Rectangle, Color.White); 

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
