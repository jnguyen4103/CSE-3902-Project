using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    class ArrowSprite : ISprite
    {
        int currentFrame = 0;
        double frameCounter = 0;
        int currentAtlasColumn = 1;
        int totalFrames = 1;
        int frameWidth;
        int frameHeight;
        Texture2D spriteTexture;
        SpriteBatch spriteBatch;
        Vector2 position;
        int xDirection;
        int yDirection;
        public ArrowSprite(Texture2D texture, SpriteBatch batch, Vector2 spawnPosition, int xDir, int yDir)
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



        public void DrawSprite()
        {


            //Arrow is moving vertically

            if (xDirection == 0)
            {
                frameWidth = 5;
                frameHeight = 16;
            }

            //Arrow Moving Horizontally
            else if(yDirection == 0)
            {
                frameWidth = 16;
                frameHeight = 5;
            }
            int row = currentFrame / currentAtlasColumn;
            int column = currentFrame % currentAtlasColumn;


            this.Move();
            Rectangle srcRectangle = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            spriteBatch.Draw(spriteTexture, destRectangle, srcRectangle, Color.White);
        }
    }
}
