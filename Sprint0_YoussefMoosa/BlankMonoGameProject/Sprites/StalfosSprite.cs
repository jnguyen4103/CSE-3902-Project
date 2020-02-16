using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class StalfosSprite : ISprite
    {
        // Variables for keeping track of which frame is drawn
        double frameCounter = 0;                // Controls speed in which frames change
        private int currentFrame = 0;           // The currrent frame being drawn
        private int currentAttackFrame = 0;     // Only important for monsters that idle when attacking
        private int currentAtlasColumn = 2;     // Controls which column of frames will be drawn
        private readonly int framesTotal = 2;   // Max number of frames for walking and attacking

        // Variables for keeping track of Sprite's position on the screen
        private Vector2 targetPosition;         // Position NPC is trying to move to
        public Vector2 position;                // Current position of NPC
        private readonly Vector2 screen;        // Screen boundaries
        private Vector2 speed;

        // Variables required for drwaing the Sprite
        public Texture2D spriteTexture;
        private readonly SpriteBatch batch;

        public StalfosSprite(Texture2D texture,
           Vector2 spawn, Vector2 screenDim, SpriteBatch spriteBatch)
        {
            this.spriteTexture = texture;
            this.batch = spriteBatch;
            this.position.X = spawn.X;
            this.position.Y = spawn.Y;
            this.screen = screenDim;
            this.batch = spriteBatch;
            this.speed.X = 0.25f;
            this.speed.Y = 0.25f;
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            targetPosition.X = position.X +  newPosition.X;
            targetPosition.Y = position.Y + newPosition.Y;

        }

        public void UpdateSpriteFrames(int newAtlasColumn)
        {
            currentAtlasColumn = newAtlasColumn;
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
                    position.X -= speed.X;
                }
                else if (position.X < targetPosition.X)
                {
                    position.X += speed.X;
                }
            }
            else if (position.Y != targetPosition.Y)
            {
                if (position.Y > targetPosition.Y)
                {
                    position.Y -= speed.Y;
                }
                else if (position.Y < targetPosition.Y)
                {
                    position.Y += speed.Y;
                }
            }
        }

        private void Animate()
        {
            frameCounter += 0.1;
            if (frameCounter >= 1)
            {
                currentFrame++;
                if(framesTotal == currentFrame)
                {
                    currentFrame = 0;
                }
                frameCounter = 0;
            }

        }

        public void DrawSprite()
        {
            int frameWidth = 15;
            int frameHeight = 16;
            int row = currentFrame / currentAtlasColumn;
            int column = currentFrame % currentAtlasColumn;

            this.Move();
            this.Animate();

            Rectangle srcRectangle = new Rectangle(frameWidth*column, frameHeight*row, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int) position.X, (int) position.Y, frameWidth, frameHeight);
            batch.Draw(spriteTexture, destRectangle, srcRectangle, Color.White);
        }

    }
}
