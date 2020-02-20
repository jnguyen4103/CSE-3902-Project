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
        // Variables that assist in maintaining info on Link's position
        // what sprite/animations he is currently using and his SpriteSheet
        public Vector2 position;
        public Rectangle[] AnimationFrames;
        public Rectangle currentAnimationFrame;
        Vector2 lastFrameSize;
        SpriteBatch batch;
        Texture2D LinkTexture;

        // Flags depending on which state Link is in
        private bool isMoving = false;
        private bool isAttacking = false;
        private bool isDamaged = false;
        SpriteEffects SpriteEffect;

        // Default sprite frame height
        int frameWidth = 16;
        int frameHeight = 16;
  
        // Variables that keep track of which animation frame is in
        int currentRow = 0;             // Keeps track of which frame is being used
        double frameCounter = 0;        // Frame transition speed (increments by 0.2 per frame)
        int rowsTotal = 2;              // All Link Sprites have 2 animation frames
        int currentFrameColumn = 1;     // Each animation has a separate column (ranging from 0 to 10)
        double animationCounter = 1;    // Animation speed


        // Default drawing height and width
        public int drawWidth = 16;
        public int drawHeight = 16;


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
            // Basic W A S D movement for Link
            position.X += direction.X;
            position.Y += direction.Y;
            Animate();
        }

        public void Animate()
        {
            // When AnimationCounter / 0.2 frames pass, transition to next animation frame
            frameCounter += 0.2;
            if (frameCounter >= animationCounter)
            {
                currentRow++;
                if (rowsTotal == currentRow)
                {
                    // When last frame is reached, restart
                    currentRow = 0;
                    isAttacking = false;
                }
                frameCounter = 0;
            }
        }

        public void UpdateLinkAnimationFrames(Rectangle newFrame, bool moving, SpriteEffects spriteEffect)
        {
            // Each Sprite Animation is 16 pixels apart in the X axis
            currentFrameColumn = (newFrame.X/16);

            // Updates which Animation Frames are used, allows for changing directions, attack directions, damaged and such
            currentAnimationFrame = newFrame;
            isMoving = moving;

            // Some frames have a sprite effect (such as flipping) to limit space of the sprite sheet
            SpriteEffect = spriteEffect;

            // True if Link is attacking up or down
            if((newFrame.X / 16) == 8 || (newFrame.X / 16) == 9)
            {
                animationCounter = 3;
                isAttacking = true;
                currentRow = 0;
                lastFrameSize = new Vector2(16, 28);
            }
            // True if link is attacking to the left or right
            else if ((newFrame.X / 16) == 10)
            {
                animationCounter = 3;
                isAttacking = true;
                currentRow = 0;
                lastFrameSize = new Vector2(28, 16);
            }
            // True if Link is being damaged
            else if((newFrame.X / 16) >3 && (newFrame.X / 16) < 8)
            {
                isDamaged = true;
            }
            
            // True for just basic W A S D movement
            else
            {
                animationCounter = 1;
                isAttacking = false;
                isDamaged = false;
                lastFrameSize = new Vector2(16, 16);
            }
        }

        public void DrawSprite()
        {
            Vector2 origin = new Vector2(0, 0);
            drawWidth = 16;
            drawHeight = 16;


            // The last attacking animation frame changes in size
            // so this calculates the size change so the drawing rectangles 
            // can be adjusted accordingly
            if (currentRow > 0 && isAttacking)
            {
                drawWidth = (int)lastFrameSize.X;
                drawHeight = (int)lastFrameSize.Y;
            }

            // If a Sprite Effect exists change the origin of the Link Sprite
            // A sprite that is flipped is offset, this logic makes animation smooth 
            // offsets the sprite so it's in the same location prior to being flipped
            // so the animation runs correctly, otherwise Link will move backward when
            // using his sword
            if (!SpriteEffect.Equals(new SpriteEffects()))
            {
                origin.X = drawWidth - frameWidth;
                origin.Y = drawHeight - frameHeight;
            }

            // Call animate when Link is not moving, otherwise movement animation is handled in the UpdateLocation method
            if (isAttacking || isDamaged) { Animate(); }

            Rectangle srcRectangle = new Rectangle(frameWidth * currentFrameColumn, frameHeight * currentRow, drawWidth, drawHeight);
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, drawWidth, drawHeight);
            batch.Draw(LinkTexture, destRectangle, srcRectangle, Color.White, 0.0f, origin, SpriteEffect, 0.0f);
        }

    }
}
