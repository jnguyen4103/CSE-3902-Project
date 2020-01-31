using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class MovingSpriteLR : ISprite
    {
        private Texture2D Texture;
        private int currentFrame;
        private int TotalFrames;
        private int frameDelay;
        private int maxDelay = 0;

        public MovingSpriteLR(Texture2D texture, int totalFrames)
        {
            Texture = texture;
            currentFrame = 0;
            TotalFrames = totalFrames;
            frameDelay = 0;
        }
        public void Update()
        {
            if (currentFrame == 0 || currentFrame == 1 || currentFrame == 10 || currentFrame == 11 || currentFrame == 13)
                maxDelay = 15;
            else
                maxDelay = 7;
            if (frameDelay == maxDelay)
            {
                currentFrame++;
                if (currentFrame == TotalFrames)
                    currentFrame = 0;
                frameDelay = 0;
            }
            frameDelay++;
        }

        public void DrawSprite(SpriteBatch spriteBatch, Vector2 location)
        {
            // sourceRectangle is a sprite and destinationRectangle
            // is the window.
            Rectangle sourceRectangle = new Rectangle(32, 581, 139, 121);
            Rectangle destinationRectangle = new Rectangle(270, 70, 278, 242);

            if (currentFrame == 1)
            {
                sourceRectangle = new Rectangle(172, 553, 148, 149);
                destinationRectangle = new Rectangle(260, 20, 296, 298);
            }
            else if (currentFrame == 2)
            {
                sourceRectangle = new Rectangle(326, 649, 38, 48);
                destinationRectangle = new Rectangle(370, 200, 76, 96);
            }
            else if (currentFrame == 3)
            {
                sourceRectangle = new Rectangle(383, 654, 49, 46);
                destinationRectangle = new Rectangle(370, 200, 98, 92);
            }
            else if (currentFrame == 4)
            {
                sourceRectangle = new Rectangle(383, 654, 49, 46);
                destinationRectangle = new Rectangle(400, 200, 98, 92);
            }
            else if (currentFrame == 5)
            {
                sourceRectangle = new Rectangle(383, 654, 49, 46);
                destinationRectangle = new Rectangle(430, 200, 98, 92);
            }
            else if (currentFrame == 6)
            {
                sourceRectangle = new Rectangle(383, 654, 49, 46);
                destinationRectangle = new Rectangle(460, 200, 98, 92);
            }
            else if (currentFrame == 7)
            {
                sourceRectangle = new Rectangle(449, 646, 49, 52);
                destinationRectangle = new Rectangle(520, 180, 98, 104);
            }
            else if (currentFrame == 8)
            {
                sourceRectangle = new Rectangle(503, 638, 127, 72);
                destinationRectangle = new Rectangle(550, 180, 234, 144);
            }
            else if (currentFrame == 9)
            {
                sourceRectangle = new Rectangle(503, 638, 127, 72);
                destinationRectangle = new Rectangle(0, 180, 234, 144);
            }
            else if (currentFrame == 10)
            {
                sourceRectangle = new Rectangle(65, 761, 36, 53);
                destinationRectangle = new Rectangle(150, 200, 72, 106);
            }
            else if (currentFrame == 11)
            {
                sourceRectangle = new Rectangle(116, 761, 36, 53);
                destinationRectangle = new Rectangle(150, 200, 72, 106);
            }
            else if (currentFrame == 12)
            {
                sourceRectangle = new Rectangle(164, 761, 45, 53);
                destinationRectangle = new Rectangle(150, 200, 90, 106);
            }
            else if (currentFrame == 13)
            {
                sourceRectangle = new Rectangle(214, 709, 377, 157);
                destinationRectangle = new Rectangle(70, 100, 754, 314);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }  
    }
}
