using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class MovingSpriteUD : ISprite
    {
        private Texture2D Texture;
        private int currentFrame;
        private int TotalFrames;
        private int frameDelay;
        private int maxDelay = 7;
        private int yPos = 200;

        public MovingSpriteUD(Texture2D texture, int totalFrames)
        {
            Texture = texture;
            currentFrame = 0;
            TotalFrames = totalFrames;
            frameDelay = 0;
        }

        public void Update()
        {
            if (frameDelay == maxDelay)
            {
                currentFrame++;
                if (currentFrame == TotalFrames)
                {
                    currentFrame = 0;
                }
                frameDelay = 0;
            }
            frameDelay++;
        }

        public void DrawSprite(SpriteBatch spriteBatch, Vector2 location)
        {

            // sourceRectangle is a sprite and destinationRectangle
            // is the window.
            Rectangle sourceRectangle = new Rectangle(78, 216, 36, 58);
            Rectangle destinationRectangle = new Rectangle(350, yPos, 72, 116);
           
            if (currentFrame < TotalFrames && frameDelay == maxDelay)
            {
                yPos -= 30;
                destinationRectangle = new Rectangle(350, yPos, 72, 116);
            }
            if (yPos <= -100)
            {
                yPos = 480;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
