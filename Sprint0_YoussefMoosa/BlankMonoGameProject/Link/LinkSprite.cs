using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class LinkSprite
    {
        public Vector2 position;
        public Rectangle[] AnimationFrames;
        public Rectangle currentAnimationFrame;
        Vector2 lastFrameSize;
        SpriteBatch batch;
        Texture2D LinkTexture;
        bool isMoving = false;
        float angle = 0f;
        int currentRow = 0;

        // Default sprite frame height and width
        int frameWidth = 16;
        int frameHeight = 16;

        // Default drawing height and width
        int drawWidth = 16;
        int drawHeight = 16;
        double frameCounter = 0;
        int rowsTotal = 2;
        int currentFrameColumn = 1;
        double animationCounter = 1;

        public LinkSprite(Texture2D texture, Vector2 spawn, SpriteBatch spriteBatch, Rectangle[] animationFrames)
        {
            LinkTexture = texture;
            AnimationFrames = animationFrames;
            currentAnimationFrame = animationFrames[1];
            position = spawn;
            batch = spriteBatch;
        }

        public void UpdateLocation(Vector2 direction)
        {

            position.X += direction.X;
            position.Y += direction.Y;
            Animate();
        }

        public void Animate()
        {
            frameCounter += 0.2;
            if (frameCounter >= animationCounter)
            {
                currentRow++;
                if (rowsTotal == currentRow)
                {
                    currentRow = 0;
                }
                frameCounter = 0;
            }
        }

        public void UpdateLinkAnimationFrames(Rectangle newFrame, bool moving, float rotation)
        {
            currentFrameColumn = (newFrame.X/16);
            currentAnimationFrame = newFrame;
            isMoving = moving;
            angle = rotation;

            if((newFrame.X / 16) == 8 || (newFrame.X / 16) == 9)
            {
                Console.WriteLine("Vertical Attack");
                animationCounter = 3;
                lastFrameSize = new Vector2(16, 28);
            }
            else if ((newFrame.X / 16) == 10)
            {
                Console.WriteLine("Horiziontal Attack");
                animationCounter = 3;
                lastFrameSize = new Vector2(28, 16);
            } else
            {
                animationCounter = 1;
                lastFrameSize = new Vector2(16, 16);
            }
        }

        public void DrawSprite()
        {

            if (currentRow > 0)
            {
                drawWidth = (int)lastFrameSize.X;
                drawHeight = (int)lastFrameSize.Y;
            }

            Rectangle srcRectangle = new Rectangle(frameWidth * currentFrameColumn, frameHeight * currentRow, drawWidth, drawHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, drawWidth, drawHeight);
            batch.Draw(LinkTexture, destRectangle, srcRectangle, Color.White);
        }

    }
}
