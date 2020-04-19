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
        Texture2D wallImage;

        public int mapWidth = 15 ;
        public int mapHeight = 9;
        public int tileSize = 128;



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
}
