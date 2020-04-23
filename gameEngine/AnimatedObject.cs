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
    public class AnimatedObject : GameObject
    {

        protected int currentAnimationFrame;
        protected float animationTimer;
        protected int currentAnimationY , currentAnimationX;
        protected AnimationSet animationSet = new AnimationSet() ;
        protected Animation currentAnimation = new Animation();

        protected bool flipRightFrames = true;
        protected bool flipLeftFrames = false;
        protected SpriteEffects spriteEffect = SpriteEffects.None;

        protected enum Animations
        {
            RunLeft , RunRight , IdelRight , IdelLeft
        }


    }
}
