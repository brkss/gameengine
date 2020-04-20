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
    class Player : Character
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
            image = TextureLoader.Load("char1", content);
            base.Load(content);
        }

        public override void Update(List<GameObject> objects,Map map)
        {
            this.checkInput();
            base.Update(objects,map);
        }

        private void checkInput()
        {
            // keys 
            if (Input.IsKeyDown(Keys.Left)) moveLeft();
            else if (Input.IsKeyDown(Keys.Right)) moveRight();
            if (Input.IsKeyDown(Keys.Up)) moveUp() ;
            else if (Input.IsKeyDown(Keys.Down)) moveDown() ;

        }
    }
}
