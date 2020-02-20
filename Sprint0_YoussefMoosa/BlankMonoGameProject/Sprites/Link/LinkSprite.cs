using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class LinkSprite : ISprite
    {

        // Variables for keeping track of Sprite's position on the screen
        protected Game1 Game;

        // Position & Movement Info
        private float BaseSpeed = 0.75f;
        protected Vector2 CurrentSpeed = new Vector2(0.0f, 0.0f);
        protected Vector2 Position;


        // Sprite Info
        private string Name;
        protected Vector2 Origin = new Vector2(0, 0);
        protected Vector2 Size;

        // Sprite Animation & Drawing Info
        private SpriteBatch Batch;
        private Texture2D Texture;
        private Rectangle DrawWindow;
        private Rectangle AnimationWindow;
        protected SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        protected int Layer = 0;
        private bool FlipFlag = false;
        private readonly int FlipOffset;

        // Animation & Moving Info
        private int TotalFrames;
        private int CurrentFrame = 0;
        private int FPS;
        private int GameFrame = 0;


        public LinkSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            Game = game;
            Batch = batch;
            Name = name;
            Size = game.Factory.LinkSprites[name].Item2;
            Position = spawn;
            Texture = texture;
            CurrentFrame = 0;
            TotalFrames = game.Factory.LinkSprites[name].Item3;
            FPS = 4;
            ChangeSpriteAnimation(name);
            FlipOffset = (int)this.Size.X;


            // Setting up conditions for testing
            // Remove if still presnet
            this.CurrentSpeed.Y = BaseSpeed;

        }

        public Vector2 GetPosition { get { return Position; } }

        public Vector2 GetSize { get { return Size; } }

        public void Animate()
        {
            if (CurrentSpeed.X != 0 || CurrentSpeed.Y != 0)
            {
                GameFrame++;
                if ((60 / FPS <= GameFrame))
                {
                    GameFrame = 0;
                    CurrentFrame++;
                    if (FlipFlag && (CurrentSpeed.X < 0) || (CurrentSpeed.Y < 0))
                    {
                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                        FlipFlag = !FlipFlag; 
                    }
                    else
                    {
                        this.SpriteEffect = SpriteEffects.None;
                    }
                    if (CurrentFrame == TotalFrames)
                    {
                        CurrentFrame = 0;
                    }
                }
            }

        }

        public void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.Factory.LinkSprites[newSpriteName];
            CurrentFrame = 0;
            TotalFrames = NewInfo.Item3;
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
        }

        private void DrawHelper()
        {
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(CurrentFrame * Size.Y);
        }


        public void DrawSprite()
        {

            Move();
            Animate();
            DrawHelper();
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Color.White, Rotation, Origin, SpriteEffect, Layer);
        }

        public void Move()
        {
            Position.X += CurrentSpeed.X;
            Position.Y += CurrentSpeed.Y;
        }

        public void Update(Vector2 newSpeed)
        {
            // Call this method in the StateMachine
            // Update speed based on direction
            // Ex: Moving up = (0, -0.75)
            // The Move method moves the sprite based on his speed
            // His Idle, Damaged and Attacking States will set the speed to 0
            // Consider adding flags if necessary
            // If no flags are added then change fuction name to just
            // UpdateSpeed or ChangeSpeed

            CurrentSpeed = newSpeed;
        }

    }
}
