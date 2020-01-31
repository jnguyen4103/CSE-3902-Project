using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class AnimatedSprite : ISprite
    {
        private Texture2D Texture;
        private int currentFrame;
        private int TotalFrames;
        private int frameDelay;
        private int maxDelay = 10;
        private int xLocation = 350;
        private int yLocation = 100;

        public AnimatedSprite(Texture2D texture, int totalFrames)
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
                    currentFrame = 0;
                frameDelay = 0;
            }
            frameDelay++;
        }
        public void DrawSprite(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(37, 150, 29, 55);
            Rectangle destinationRectangle = new Rectangle(xLocation, yLocation, 87, 165);

            if (currentFrame == 1)
            {
                sourceRectangle = new Rectangle(74, 155, 36, 51);
                destinationRectangle = new Rectangle(xLocation, yLocation, 108, 153);
            }
            else if (currentFrame == 2)
            {
                sourceRectangle = new Rectangle(118, 155, 38, 51);
                destinationRectangle = new Rectangle(xLocation, yLocation, 114, 153);
            }
            else if (currentFrame == 3)
            {
                sourceRectangle = new Rectangle(164, 150, 38, 56);
                destinationRectangle = new Rectangle(xLocation, yLocation, 114, 168);
            }
            else if (currentFrame == 4)
            {
                sourceRectangle = new Rectangle(208, 157, 32, 49);
                destinationRectangle = new Rectangle(xLocation, yLocation, 96, 147);
            }
            else if (currentFrame == 5)
            {
                sourceRectangle = new Rectangle(250, 150, 37, 56);
                destinationRectangle = new Rectangle(xLocation, yLocation, 111, 168);
            }
            else if (currentFrame == 6)
            {
                sourceRectangle = new Rectangle(290, 158, 37, 48);
                destinationRectangle = new Rectangle(xLocation, yLocation, 111, 144);
            }
            else if (currentFrame == 7)
            {
                sourceRectangle = new Rectangle(341, 165, 55, 41);
                destinationRectangle = new Rectangle(xLocation, yLocation, 165, 123);
            }
            else if (currentFrame == 8)
            {
                sourceRectangle = new Rectangle(396, 155, 55, 41);
                destinationRectangle = new Rectangle(xLocation, yLocation, 165, 123);
            }

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
