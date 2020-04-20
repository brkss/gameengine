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
    public class Character : GameObject
    {
        public Vector2 velocity;

        //Custum the feel of our movement
        protected float deceel = 1.2f; // the lower you decile is , the slower you slow down
        protected float accel = .78f; // the your acceleration is , the slower take off 
        protected float maxSpeed = 5f;

        const float gravity = 1f;
        const float jumpVelocity = 16f; //how much we jump 
        const float maxFallVelocity = 32;

        protected bool jumping;
        public static bool applyGravity = true ;


        public override void Initialize()
        {
            velocity = Vector2.Zero;
            jumping = false;
            base.Initialize();
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            updateMovement(objects, map);
            base.Update(objects, map);
        }

        private void updateMovement(List<GameObject> objects,Map map )
        {
            if (velocity.X != 0 && checkCollistions(map, objects,true) == true)
                velocity.X = 0;
            position.X += velocity.X;
            
            if (velocity.Y != 0 && checkCollistions(map, objects, false) == true)
                velocity.Y = 0;
            
            position.Y += velocity.Y;

            if (applyGravity == true)
                ApplyGravity(map);

            velocity.X = TendToZero(velocity.X, deceel);
            if (applyGravity == false)
                velocity.Y = TendToZero(velocity.Y, deceel);

        }

        private void ApplyGravity(Map map)
        {
            if (jumping || OnGround(map) == Rectangle.Empty)
                velocity.Y += gravity;
            if (velocity.Y > maxFallVelocity)
                velocity.Y = maxFallVelocity; 
        }

        protected void moveRight()
        {
            velocity.X += accel + deceel;
            if (velocity.X > maxSpeed)
                velocity.X = maxSpeed;

            direction.X = 1;
        }

        protected void moveLeft()
        {
            velocity.X -= accel + deceel;
            if (velocity.X < -maxSpeed)
                velocity.X = -maxSpeed;

            direction.X = -1;
        }

        protected void moveDown()
        {
            velocity.Y += accel + deceel;
            if (velocity.Y > maxSpeed)
                velocity.Y = maxSpeed;

            direction.Y = 1;
        }

        protected void moveUp()
        {
            velocity.Y -= accel + deceel;
            if (velocity.Y < -maxSpeed)
                velocity.Y = -maxSpeed;

            direction.Y = -1;
        }

        protected bool jumpUp(Map map)
        {
            if (jumping == true)
                return false ;
            if (velocity.Y == 0 && OnGround(map) != Rectangle.Empty)
            {
                velocity.Y -= jumpVelocity;
                jumping = true;
                return true;
            }
            return false;
        }


        protected virtual bool checkCollistions(Map map, List<GameObject> objects,bool xAxis)
        {
            Rectangle futerBoundingBox = BoudingBox;

            int maxX = (int)maxSpeed;
            int maxY = (int)maxSpeed;
            if (applyGravity)
                maxY = (int)jumpVelocity;
            if (xAxis == true && velocity.X != 0)
            {
                if (velocity.X > 0)
                    futerBoundingBox.X += maxX;
                else
                    futerBoundingBox.X -= maxX;
            }
            else if (applyGravity == false && xAxis == false && velocity.Y != 0)
            {
                if (velocity.Y > 0)
                    futerBoundingBox.Y += maxY;
                else
                    futerBoundingBox.Y -= maxY;
            }
            else if (applyGravity == true && xAxis == false && velocity.Y != gravity)
            {
                if (velocity.Y > 0)
                    futerBoundingBox.Y += maxY;
                else
                    futerBoundingBox.Y -= maxY;
            }

            // Check Collition with map
            Rectangle wallcollistion = map.checkCollision(futerBoundingBox);
            if(wallcollistion != Rectangle.Empty)
            {
                if (applyGravity == true && velocity.Y >= gravity && (futerBoundingBox.Bottom > wallcollistion.Top - maxSpeed) && (futerBoundingBox.Bottom <= wallcollistion.Top + velocity.Y))
                {
                    // Land Response 
                    landResponse(wallcollistion);
                    return true;
                }
                else
                    return true;
            }

            // Check collition with objects 
            for(int i = 0; i < objects.Count; i++)
            {
                if(objects[i] != this &&  objects[i].active == true && objects[i].collidable == true && objects[i].checkCollition(futerBoundingBox) == true )
                {
                    return true;
                }
            }

            //  no collition 
            return false;
        }

        public void landResponse(Rectangle wallCollision)
        {
            position.Y = wallCollision.Top - (boundingBoxHeight + boundingBowOffset.Y);
            velocity.Y = 0;
            jumping = false;

        }
        protected Rectangle OnGround(Map map)
        {
            Rectangle futureBoundingBox = new Rectangle((int)(position.X + boundingBowOffset.X), (int)(position.Y + boundingBowOffset.Y + (velocity.Y + gravity)), boundingBoxWidth, boundingBoxHeight);

            return map.checkCollision(futureBoundingBox);
        }
        protected float TendToZero(float val, float amount)
        {
            if (val > 0f && (val -= amount) < 0f) return 0f;
            if (val < 0f && (val += amount) > 0f) return 0f;
            return val;
        }

        
    }
}
