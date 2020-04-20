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
    public class Bullet : GameObject
    {
        const float speed = 20f; // how fast the bullet will go 
        Character owner;

        int destroyTimer;
        const int maxTimer = 100;

        public Bullet()
        {
            active = false;
        }

        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("bullet", content); 
            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            if (active == false)
                return;

            // Update movement  : 
            position += direction * speed;

            checkCollision(map,objects);
            
           
            //Updare destroy timer
            destroyTimer--;
            if (destroyTimer <= 0 && active == true)
                destroy();

            base.Update(objects, map);
        }


        public void fire(Character inputOwner , Vector2 inputPosition , Vector2 inputDirection) 
        {
            owner = inputOwner;
            position = inputDirection;
            direction = inputDirection;
            active = true;
            destroyTimer = maxTimer;
             
        }

        private void checkCollision(Map map , List<GameObject> objects)
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if(objects[i].active == true && objects[i].checkCollition(BoudingBox) == true && objects[i] != owner)
                {
                    destroy();
                    objects[i].bulletResponse();
                    return;
                }
            }
            if(map.checkCollision(BoudingBox) != Rectangle.Empty)
            {
                //....
                destroy();
            }
        }

        public void destroy()
        {
            active = false; 
        }



    }
}
