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

        public Map map = new Map();

        GameHUD gameHud = new GameHUD();

        public List<GameObject> objects = new List<GameObject>();

      

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(1280, 720);

            Resolution.SetResolution(800, 600,false);

            // camera
            Camera.Initialize();

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
            LoadLevel();
            gameHud.load(Content);
            map.Load(Content);
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            updateObjects();
            map.update(objects);
            updateCamera();
            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            Resolution.BeginDraw();
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, null, RasterizerState.CullCounterClockwise, null, Camera.GetTransformMatrix());
            drawObjects();
            map.drawWalls(spriteBatch);
            spriteBatch.End();

            

            gameHud.draw(spriteBatch);

            //Draw the things FNA handles for us underneath the hood:
            base.Draw(gameTime);
        }

        public void LoadLevel()
        {
            //add walls

            map.walls.Add(new Wall(new Rectangle(256, 256, 256, 256)));
            map.walls.Add(new Wall(new Rectangle(0, 650, 1280, 128)));
            objects.Add(new Player(new Vector2(256, 200)));
            objects.Add(new Enemy(new Vector2(300, 120)));

            //add decores
            map.decores.Add(new Decore(Vector2.Zero, "background", 1f));

            map.loadMap(Content);

            loadObjects();
        }

        public void loadObjects()
        {
            for(int i = 0; i <  objects.Count; i++)
            {
                objects[i].Initialize();
                objects[i].Load(Content);
            }
        }

        public void updateObjects()
        {
            for (int i = 0; i <  objects.Count; i++)
            {
                objects[i].Update(objects,map);
                
            }
        }

        public void drawObjects()
        {
            for (int i = 0; i <  objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);

            }

            for (int i = 0; i < map.decores.Count; i++)
            {
                map.decores[i].Draw(spriteBatch);

            }
        }

        private void updateCamera()
        {
            if (objects.Count == 0) return;
            Camera.Update(objects[0].position);
        }
    }
}
