using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    class SwordBeamSprite : ISprite
    {
        int currentFrame = 0;
        double frameCounter = 0;
        int currentAtlasColumn = 1;
        int totalFrames = 3;
        int frameHeight;
        int frameWidth;

        Texture2D spriteTexture;
        SpriteBatch spriteBatch;
        Vector2 position;
        int xDirection;
        int yDirection;
        public SwordBeamSprite(Texture2D texture, SpriteBatch batch, Vector2 spawnPosition, int xDir, int yDir)
        {
            spriteTexture = texture;
            spriteBatch = batch;
            position = spawnPosition;
            xDirection = xDir;
            yDirection = yDir;

        }

        private void Move()
        {
            position.X += xDirection;
            position.Y += yDirection;
        }

        public void Animate()
        {
            frameCounter += .3;
            if (frameCounter >= 1)
            {
                currentFrame++;
                if (totalFrames == currentFrame)
                {
                    currentFrame = 0;
                }
                frameCounter = 0;
            }
        }


        public void DrawSprite()
        {

            //Sword Beam is moving vertically

            if (xDirection == 0)
            {
                frameWidth = 7;
                frameHeight = 15;
            }

            //Sword Beam Moving Horizontally
            else if (yDirection == 0)
            {
                frameWidth = 15;
                frameHeight = 7;

            }
            int row = currentFrame / currentAtlasColumn;
            int column = currentFrame % currentAtlasColumn;

            this.Move();
            this.Animate();
            Rectangle srcRectangle = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            spriteBatch.Draw(spriteTexture, destRectangle, srcRectangle, Color.White);
        }
    }
}
