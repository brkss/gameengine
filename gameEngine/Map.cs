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
    public class Map
    {
        public List<Wall> walls = new List<Wall>();
        public List<Decore> decores = new List<Decore>();
        Texture2D wallImage;

        public int mapWidth = 15 ;
        public int mapHeight = 9;
        public int tileSize = 128;


        public void loadMap(ContentManager content)
        {
            for(int i = 0; i < decores.Count; i++)
            {
                decores[i].load(content,decores[i].imagePath);
            }
        }

        public void Load(ContentManager content)
        {
            wallImage = TextureLoader.Load("pixel", content);
        }
        public Rectangle checkCollision(Rectangle inputRect)
        {
            for(int i = 0; i < walls.Count; i++)
            {
                if(walls[i] != null && walls[i].wall.Intersects(inputRect) == true)
                {
                    return walls[i].wall;
                } 
            }
            return Rectangle.Empty;
        }

        public void drawWalls(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if(walls[i] != null && walls[i].active)
                {
                    spriteBatch.Draw(wallImage, new Vector2(walls[i].wall.X, walls[i].wall.Y),walls[i].wall,Color.Black,0f,Vector2.Zero,1f,SpriteEffects.None,.7f);
                }
            }
        }

        public void update(List<GameObject> objects)
        {
            for (int i = 0; i < decores.Count; i++)
            {
                decores[i].Update(objects, this);
            }
        }
    }

    public class Wall
    {
        public Rectangle wall;
        public bool active = true;

        public Wall()
        {

        }
        public Wall(Rectangle inpRectangle)
        {
            wall = inpRectangle;
        }

    }

    public class Decore : GameObject
    {
        public string imagePath;
        public Rectangle sourceRect;

        public string Name
        {
            get { return imagePath; }
        }

        public Decore()
        {
            collidable = false;
        }
        public Decore(Vector2 inputPosition , String imagePath,float inputDepth)
        {
            position = inputPosition;
            imagePath = imagePath;
            layerDepth = inputDepth;
            active = true;
            collidable = false;
        }

        public virtual void load(ContentManager content , String asset)
        {
            image = TextureLoader.Load(asset, content);
            image.Name = asset;

            boundingBoxHeight = image.Height;
            boundingBoxWidth = image.Width;

            if (sourceRect == Rectangle.Empty)
                sourceRect = new Rectangle(0, 0, boundingBoxWidth, boundingBoxHeight);
        }

        public void setImage(Texture2D input , string newPath)
        {
            image = input;
            image.Name = newPath;
            boundingBoxWidth = sourceRect.Width = image.Width;
            boundingBoxHeight = sourceRect.Height = image.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (image != null && active == true)
                spriteBatch.Draw(image, position, sourceRect, drawColor, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }
    }
}
