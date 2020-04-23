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
    public class AnimatedObject : GameObject
    {

        protected int currentAnimationFrame;
        protected float animationTimer;
        protected int currentAnimationY = -1 , currentAnimationX = -1;
        protected AnimationSet animationSet = new AnimationSet() ;
        protected Animation currentAnimation = new Animation();

        protected bool flipRightFrames = true;
        protected bool flipLeftFrames = false;
        protected SpriteEffects spriteEffect = SpriteEffects.None;

        protected enum Animations
        {
            RunLeft , RunRight , IdelRight , IdelLeft
        }

        protected void loadAnimation(string path ,ContentManager content)
        {
            AnimationData animationData = AnimationLoader.Load(path);
            animationSet = animationData.animation ;

            center.X = animationSet.width / 2;
            center.Y = animationSet.height / 2;

            if(animationSet.animationList.Count > 0)
            {
                currentAnimation = animationSet.animationList[0];

                currentAnimationFrame = 0;
                animationTimer = 0f;
                calculateAnimation();
            }
        }

        public override void Update(List<GameObject> obects, Map map)
        {
            base.Update(obects, map);
            if(currentAnimation == null)
            {
                updateAnimation();
            }
        }

        protected virtual void updateAnimation()
        {
            /*
            if (currentAnimation.animationOrder.Count > 1)
                return;
                */
            ///bool test = currentAnimation.animationOrder.Count > 1;
            animationTimer -= 1;

            if(animationTimer <= 0)
            {
                animationTimer = currentAnimation.speed;
                if (animationComplite())
                    currentAnimationFrame = 0;
                else
                    currentAnimationFrame ++;

                calculateAnimation();
            }
        }

        protected virtual void changeAnimation(Animations newAnimation)
        {
            currentAnimation = getAnimation(newAnimation);
            if (currentAnimation == null)
                return;
            // strat from first frame ;
            currentAnimationFrame = 0;
            animationTimer = currentAnimation.speed;

            calculateAnimation();

            // ceck if we need to flip
            if (flipRightFrames == true && currentAnimation.name.Contains("Right") || flipLeftFrames == true && currentAnimation.name.Contains("Left"))
                spriteEffect = SpriteEffects.FlipHorizontally;
            else
                spriteEffect = SpriteEffects.None;


        }

        private Animation getAnimation(Animations animation)
        {
            string name = GetAnimationName(animation); 
            for(int i = 0; i < animationSet.animationList.Count; i++)
            {
                if(animationSet.animationList[i].name == name)
                {
                    return animationSet.animationList[i];
                }
            }
            return null;
        }

        public bool animationComplite()
        {
            return currentAnimationFrame >= currentAnimation.animationOrder.Count - 1  ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (active == false)
                return;
            if (currentAnimationX == -1 || currentAnimationY == -1)
                base.Draw(spriteBatch);
            else
                spriteBatch.Draw(image, position, new Rectangle(currentAnimationX, currentAnimationY, animationSet.width, animationSet.height), drawColor, rotation, Vector2.Zero, scale, spriteEffect, layerDepth);
        }

        public void calculateAnimation()
        {
            int cordinate = currentAnimation.animationOrder[currentAnimationFrame];

            currentAnimationX = (cordinate % animationSet.gridX) * animationSet.width;
            currentAnimationY = (cordinate / animationSet.gridX) * animationSet.height;
        }



        protected string GetAnimationName(Animations animation)
        {
            //Make an accurately spaced string. Example: "RunLeft" will now be "Run Left":
            return AddSpacesToSentence(animation.ToString(), false);
        }

        protected bool AnimationIsNot(Animations input)
        {
            //Used to check if our currentAnimation isn't set to the one passed in:
            return currentAnimation != null && GetAnimationName(input) != currentAnimation.name;
        }

        public string AddSpacesToSentence(string text, bool preserveAcronyms) //IfThisWasPassedIn... "If This Was Passed In" would be returned
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }




    }
}
