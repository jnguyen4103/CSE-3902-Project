using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    class FullSprite : ISprite
    {
        private readonly Texture2D spriteSheet;
        private Rectangle[] animation;
        private Rectangle dest;
        private Vector2 screen;
        private Vector2 position;
        private Vector2 velocity;
        int animTime;
        int animCounter;
        int currentAnimation;
        private readonly SpriteBatch batch;

        public FullSprite(Texture2D sheet, Rectangle[] sectionsOnSheet,
           Rectangle locationOnScreen, Vector2 vel, Vector2 screenDim, SpriteBatch spriteBatch,
           int animationTime)
        {
            this.spriteSheet = sheet;
            this.animation = (Rectangle[])sectionsOnSheet.Clone();
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.position.X = this.dest.X;
            this.position.Y = this.dest.Y;
            this.velocity = vel;
            this.screen = screenDim;
            this.batch = spriteBatch;
            this.animTime = animationTime;
            this.animCounter = this.animTime;
            this.currentAnimation = 0;
        }

        private void Move()
        {
            this.position.X += this.velocity.X;
            this.position.Y += this.velocity.Y;

            while (this.position.X > this.screen.X) { this.position.X -= this.screen.X; }
            while (this.position.X < 0) { this.position.X += this.screen.X; }
            while (this.position.Y > this.screen.Y) { this.position.Y -= this.screen.Y; }
            while (this.position.Y < 0) { this.position.Y += this.screen.Y; }

            this.dest.X = (int)(this.position.X);
            this.dest.Y = (int)(this.position.Y);
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
            this.Move();
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

        public void UpdateSpriteFrames(int newAtlasColumn)
        {
            throw new System.NotImplementedException();
        }
    }
}
