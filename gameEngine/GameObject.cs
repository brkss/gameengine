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

        public bool collidable = true;
        protected int boundingBoxHeight, boundingBoxWidth;
        protected Vector2 boundingBowOffset;
        Texture2D boundingBoxImage;
        const bool drawBoundingBoxes = true;
        protected Vector2 direction = new Vector2(1, 0);


        public Rectangle BoudingBox
        {
            get { return new Rectangle((int)(position.X + boundingBowOffset.X), (int)(position.Y + boundingBowOffset.Y),boundingBoxWidth,boundingBoxHeight);  }

        }


        public GameObject() { }

        // Methods

        public virtual void Initialize()
        {

        }

        public virtual void Load(ContentManager content)
        {
            boundingBoxImage = TextureLoader.Load("pixel", content);
            this.calculateCenter();
            if(image != null)
            {
                boundingBoxHeight = image.Height;
                boundingBoxWidth = image.Width;
            }
        }

        public virtual void Update(List<GameObject> obects,Map map)
        {

        }

        public virtual bool checkCollition(Rectangle inputRect)
        {
            return BoudingBox.Intersects(inputRect);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (boundingBoxImage != null && drawBoundingBoxes == true && active == true)
                spriteBatch.Draw(boundingBoxImage, new Vector2(BoudingBox.X, BoudingBox.Y), BoudingBox, new Color(120, 120, 120, 120), 0f, Vector2.Zero, 1f, SpriteEffects.None, .1f);

            if (this.image != null && this.active) 
                spriteBatch.Draw(image, position,null ,drawColor , rotation , Vector2.Zero , scale,SpriteEffects.None,layerDepth);
        }

        public virtual void bulletResponse()
        {

        }

        private void calculateCenter()
        {
            if (image == null) return;
            center.X = image.Width / 2;
            center.Y = image.Height / 2;
        }

    }
}
