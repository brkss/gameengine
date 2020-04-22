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
    public class GameHUD 
    {
        SpriteFont font;

        public void load(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts\\Arial");

        }

        public void draw(SpriteBatch spritebatch)
        {

            spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            //spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spritebatch.DrawString(font, "Score : "+Player.score.ToString(), Vector2.Zero, Color.White);

            spritebatch.End();
        }

    }
}
