using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gameEngine
{
    public class GameObject
    {
        // Variables

        protected Texture2D image;
        public Vector2 position;
        public Color drawColor = Color.White;
        public float scale = 1f, rotation = 0f;
        public float layerDepth = .5f;
        public bool active = true;
        protected Vector2 center;


        public GameObject() { }

        // Methods

        public virtual void Initialize()
        {

        }

        public virtual void Load(ContentManager content)
        {
            this.calculateCenter();
        }

        public virtual void Update(List<GameObject> obects)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(this.image != null && this.active) 
                spriteBatch.Draw(image, position,null ,drawColor , rotation , Vector2.Zero , scale,SpriteEffects.None,layerDepth);
        }

        private void calculateCenter()
        {
            if (image == null) return;
            center.X = image.Width / 2;
            center.Y = image.Height / 2;
        }

    }
}
