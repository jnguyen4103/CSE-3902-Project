﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    class NoAnimSprite : ISprite
    {
        private readonly Texture2D   spriteSheet;
        private readonly Rectangle   sprite;
        private          Rectangle   dest;
        private          Vector2     screen;
        private          Vector2     position;
        private          Vector2     velocity;
        private readonly SpriteBatch batch;

        public NoAnimSprite(Texture2D sheet, Rectangle sectionOnSheet,
           Rectangle locationOnScreen, Vector2 vel, Vector2 screenDim, SpriteBatch spriteBatch)
        {
            this.spriteSheet = sheet;
            this.sprite = sectionOnSheet;
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.position.X = this.dest.X;
            this.position.Y = this.dest.Y;
            this.velocity = vel;
            this.screen = screenDim;
            this.batch = spriteBatch;
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

        public void DrawSprite() 
        {
            this.Move();

            batch.Begin();
            batch.Draw(spriteSheet, dest, sprite, Color.White);
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
