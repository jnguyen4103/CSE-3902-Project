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
        SpriteBatch batch;
        Texture2D LinkTexture;
        int currentFrame = 0;
        double frameCounter = 0;
        int framesTotal = 2;
        int currentAtlasColumn = 1;

        public LinkSprite(Texture2D texture, Vector2 spawn, SpriteBatch spriteBatch)
        {
            LinkTexture = texture;
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
            frameCounter += 0.15;
            if (frameCounter >= 1)
            {
                currentFrame++;
                if (framesTotal == currentFrame)
                {
                    currentFrame = 0;
                }
                frameCounter = 0;
            }
        }

        public void updateLinkDirection(int direction)
        {
            currentAtlasColumn = direction;
        }

        public void DrawSprite()
        {

            int frameWidth = 15;
            int frameHeight = 16;
            int row = currentFrame;
            int column = currentAtlasColumn;


            Rectangle srcRectangle = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            batch.Draw(LinkTexture, destRectangle, srcRectangle, Color.White);
        }

    }
}
