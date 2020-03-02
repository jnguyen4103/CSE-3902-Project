using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    // Abstract class allows for easy reuse of methods and variables
    // that all NPCSprites share
    // Look at Stalfos and Goriyas for comments on their NPC Sprite implementation
    public abstract class Sprite: ISprite
    {

        // Variables for keeping track of Sprite's position on the screen
        protected Game1 Game;

        // Position & Movement Info
        public float BaseSpeed;
        public Vector2 CurrentSpeed;                // Controls movement speed of NPC
        public Vector2 Position;
        protected float InitalAnimationY;


        // Sprite Info
        public string Name;
        protected Vector2 Origin = new Vector2(0, 0);
        protected Vector2 Size;

        // Sprite Animation & Drawing Info
        protected SpriteBatch Batch;
        protected Texture2D Texture;
        protected Rectangle DrawWindow;
        protected Rectangle AnimationWindow;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        protected float Layer = 0;
        public Color Colour = Color.White;

        // Animation & Moving Info
        protected int TotalFrames;
        protected int CurrentFrame = 0;
        public int FPS = 8;
        protected int GameFrame = 0;

        public Vector2 GetSize {  get { return Size; } }

        

        public Vector2 GetPosition {  get { return Position; } }



        public virtual void Animate()
        {
            if (TotalFrames > 1)
            {
                GameFrame++;
                if ((60 / FPS <= GameFrame))
                {
                    GameFrame = 0;
                    CurrentFrame++;

                    if (CurrentFrame == TotalFrames)
                    {
                        CurrentFrame = 0;
                    }
                }
            }

        }

        public virtual void Move()
        {

            Position.X += CurrentSpeed.X;
            Position.Y += CurrentSpeed.Y;
            Console.WriteLine(Position.X+" , "+ Position.Y);
            Console.WriteLine(Game.WalkingRect.Y);
            Console.WriteLine(Game.WalkingRect.X);


            if (Position.X >= Game.WalkingRect.Width)
            {
                Position.X = Game.WalkingRect.Width;
                if (Position.Y >= Game.WalkingRect.Height)
                {
                    Position.Y = Game.WalkingRect.Height;
                }
                else if (Position.Y <= Game.WalkingRect.Y)
                {
                    Position.Y = Game.WalkingRect.Y;
                }
            }
            else if (Position.X <= Game.WalkingRect.X)
            {
                Position.X = Game.WalkingRect.X;
                if (Position.Y >= Game.WalkingRect.Height)
                {
                    Position.Y = Game.WalkingRect.Height;
                }
                else if (Position.Y <= Game.WalkingRect.Y)
                {
                    Position.Y = Game.WalkingRect.Y;
                }
            }
            else if (Position.Y >= Game.WalkingRect.Height)
            {
                Position.Y = Game.WalkingRect.Height;
                if (Position.X >= Game.WalkingRect.Width)
                {
                    Position.X = Game.WalkingRect.Width;
                }
                else if (Position.X <= Game.WalkingRect.X)
                {
                    Position.X = Game.WalkingRect.X;
                }
            }
            else if (Position.Y <= Game.WalkingRect.Y)
            {
                Position.Y = Game.WalkingRect.Y;
                if (Position.X >= Game.WalkingRect.Width)
                {
                    Position.X = Game.WalkingRect.Width;
                }
                else if (Position.X <= Game.WalkingRect.X)
                {
                    Position.X = Game.WalkingRect.X;
                }
            }
        }

        public void ChangeSpriteAnimation(string newSpriteName)
        {
            if (Name != newSpriteName) { CurrentFrame = 0; }
            Name = newSpriteName;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.SFactory.Sprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            InitalAnimationY = NewInfo.Item1.Y;
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }

        public virtual void KillSprite()
        {
            Colour = Color.Transparent;
        }

        public virtual void DrawSprite()
        {
            Move();
            Animate();
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(InitalAnimationY + (CurrentFrame * Size.Y) + (8 * CurrentFrame));
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}
