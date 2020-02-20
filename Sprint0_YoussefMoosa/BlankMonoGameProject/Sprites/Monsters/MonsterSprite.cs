using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    // Abstract class allows for easy reuse of methods and variables
    // that all NPCSprites share
    // Look at Stalfos and Goriyas for comments on their NPC Sprite implementation
    public abstract class MonsterSprite: ISprite
    {

        // Variables for keeping track of Sprite's position on the screen
        protected Game1 Game;

        // Position & Movement Info
        protected Vector2 Path;                 // Position NPC is trying to move to
        protected float BaseSpeed;
        protected Vector2 CurrentSpeed;                // Controls movement speed of NPC
        protected Vector2 Position;
        protected Vector2 PathPosition;


        // Sprite Info
        protected string Name;
        protected Vector2 Origin = new Vector2(0, 0);
        protected Vector2 Size;

        // Sprite Animation & Drawing Info
        protected SpriteBatch Batch;
        protected Texture2D Texture;
        protected Rectangle DrawWindow;
        protected Rectangle AnimationWindow;
        protected SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        protected int Layer = 0;

        // Animation & Moving Info
        protected int TotalFrames;
        protected int CurrentFrame = 0;
        protected int FPS;
        protected int GameFrame = 0;

        public virtual Vector2 GetSize { get { return Size; } }

        public virtual Vector2 GetPosition { get { return Position; } }


        public virtual void Animate()
        {
            if (CurrentSpeed.X != 0 || CurrentSpeed.Y != 0)
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
        }
        public virtual void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo =  Game.Factory.MonsterSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }


        // Consider adding this to the state machine
        // So Call DonePathing() after every update if the
        // Monster is in the Moving State
        // If it returns true then get a new random distance to path
        // and Update Speed of sprite to the direction of that path

        public bool DonePathing()
        {
            return (Path.X == Position.Y && Path.Y == Position.Y);
        }

        public void PathToPosition(Vector2 newPath)
        {
            Path = newPath;
        }

        public void Update(Vector2 newSpeed)
        {
            // Call this method in the Monster StateMachine
            // Update speed based on direction
            // Ex: Moving (0, -0.75)
            // The Move method moves the sprite based on his speed
            // His Idle, Damaged and Attacking States will set the speed to 0
            // Consider adding flags if necessary
            // If no flags are added then change fuction name to just
            // UpdateSpeed or ChangeSpeed

            CurrentSpeed = newSpeed;
        }

        /*
         * Updates the DrawWindow and Animation Window
         * Put into separate method as DrawSprite() since functionality
         * is same across all Monster Sprites
         */
        protected void DrawHelper()
        {
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(CurrentFrame * Size.Y);
        }

        public virtual void DrawSprite()
        {
            Move();
            Animate();
            DrawHelper();
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Color.White, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}
