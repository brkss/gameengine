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
    class Player : GameObject
    {


        public Player()
        {

        }
        public Player(Vector2 inputPosition)
        {
            position = inputPosition;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("sprite", content);
            base.Load(content);
        }

        public override void Update(List<GameObject> obects)
        {
            this.checkInput();
            base.Update(obects);
        }

        private void checkInput()
        {
            // keys 
            if (Input.IsKeyDown(Keys.Left)) position.X -= 5;
            else if (Input.IsKeyDown(Keys.Right)) position.X += 5;
            if (Input.IsKeyDown(Keys.Up)) position.Y -= 5;
            else if (Input.IsKeyDown(Keys.Down)) position.Y += 5;

        }
    }
}
