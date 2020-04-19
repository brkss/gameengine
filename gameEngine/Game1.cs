using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace gameEngine
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D image;
        Vector2 position = new Vector2(640,360);

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.image = TextureLoader.Load("char1", Content);
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Input.Update(); 
            // keys 
            if (Input.IsKeyDown(Keys.Left)) position.X -= 5;
            else if (Input.IsKeyDown(Keys.Right)) position.X += 5;
            if (Input.IsKeyDown(Keys.Up)) position.Y -= 5;
            else if (Input.IsKeyDown(Keys.Down)) position.Y += 5;


            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(this.image, this.position, Color.White);
            spriteBatch.End();
            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }
    }
}
