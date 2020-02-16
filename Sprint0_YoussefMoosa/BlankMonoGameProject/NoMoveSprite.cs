using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    

    class NoMoveSprite : ISprite
    {

        private readonly Texture2D spriteSheet;
        private readonly Rectangle[] animation;
        private int animTime;
        private int animCounter;
        private int currentAnimation;
        private Rectangle dest;
        private readonly SpriteBatch batch;

        public NoMoveSprite(Texture2D sheet, Rectangle[] sectionsOnSheet,
           Rectangle locationOnScreen, int animationTime, SpriteBatch spriteBatch)
        {
            this.spriteSheet = sheet;
            this.animation = (Rectangle[])sectionsOnSheet.Clone();
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.animTime = animationTime;
            this.animCounter = this.animTime;
            this.currentAnimation = 0;
            this.batch = spriteBatch;
        }

        private void Animate()
        {
            if (this.animCounter < 0)
            {
                this.animCounter = this.animTime;
                this.currentAnimation++;
                if (this.currentAnimation >= animation.Length)
                {
                    this.currentAnimation = 0;
                }
            }
            this.animCounter--;
        }

        public void DrawSprite()
        {
            this.Animate();

            batch.Begin();
            batch.Draw(spriteSheet, dest, animation[currentAnimation], Color.White);
            batch.End();
        }

        public void UpdatePositon(Vector2 newPosition)
        {
            throw new System.NotImplementedException();
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
