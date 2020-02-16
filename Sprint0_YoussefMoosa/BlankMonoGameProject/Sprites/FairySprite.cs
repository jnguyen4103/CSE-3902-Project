using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class FairySprite : ISprite
    {
        public Texture2D spriteTexture;
        double frameCounter = 0;
        private int currentFrame = 0;
        private readonly int framesTotal = 2;
        private int currentAtlasColumn = 1;
        private readonly Vector2 screen;
        private Vector2 targetPosition;
        public Vector2 position;
        private readonly SpriteBatch batch;

        public FairySprite(Texture2D texture,
           Vector2 spawn, Vector2 screenDim, SpriteBatch spriteBatch)
        {
            this.spriteTexture = texture;
            this.batch = spriteBatch;
            this.position.X = spawn.X;
            this.position.Y = spawn.Y;
            this.screen = screenDim;
            this.batch = spriteBatch;
        }

        public void UpdateSpriteFrames(int newAtlasColumn)
        {
            currentAtlasColumn = newAtlasColumn;
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            targetPosition.X = position.X + newPosition.X;
            targetPosition.Y = position.Y + newPosition.Y;

        }

        public void UpdatePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        private void Move()
        {
            if (position.X != targetPosition.X)
            {
                if (position.X > targetPosition.X)
                {
                    position.X--;
                }
                else
                {
                    position.X++;
                }
            }
            else if (position.Y != targetPosition.Y)
            {
                if (position.Y > targetPosition.Y)
                {
                    position.Y--;
                }
                else
                {
                    position.Y++;
                }
            }

            position.X++;

        }

        private void Animate()
        {
            frameCounter += 0.1;
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

        public void DrawSprite()
        {
            int frameWidth = 8;
            int frameHeight = 16;
            int row = currentFrame / currentAtlasColumn;
            int column = currentFrame % currentAtlasColumn;

            this.Move();
            this.Animate();

            Rectangle srcRectangle = new Rectangle(frameWidth * column, frameHeight * row, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            batch.Draw(spriteTexture, destRectangle, srcRectangle, Color.White);
        }

    }
}
