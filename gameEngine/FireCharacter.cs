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
    public class FireCharacter : Character
    {
        List<Bullet> bullets = new List<Bullet>();

        const int numBullets = 15;

        public FireCharacter()
        {

        }

        public override void Initialize()
        {
            if(bullets.Count == 0)
            {
                for(int i = 0; i < numBullets; i++)
                {
                    bullets.Add(new Bullet());
                }
            }
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            for (int i = 0; i < numBullets; i++)
                bullets[i].Load(content);
            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            for (int i = 0; i < numBullets; i++)
                bullets[i].Update(objects,map);
            base.Update(objects, map);
        }

        public void fire()
        {
            for (int i = 0; i < numBullets; i++)
            {
                if (bullets[i].active == false )
                {
                    bullets[i].fire(this,position, direction);
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numBullets; i++)
                bullets[i].Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

    }

}
