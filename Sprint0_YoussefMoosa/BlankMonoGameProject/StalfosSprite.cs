using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0_YoussefMoosa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameProject
{
    class StalfosSprite : ISprite
    {
        private Texture2D spriteSheet;
        private Rectangle[] animation;
        private Rectangle dest;
        private Vector2 screen;
        public Vector2 position;
        private Vector2 velocity;
        int animTime;
        int animCounter;
        int currentAnimation;
        private readonly SpriteBatch batch;

        public StalfosSprite(Texture2D sheet, Rectangle[] sectionsOnSheet,
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
    }
}
