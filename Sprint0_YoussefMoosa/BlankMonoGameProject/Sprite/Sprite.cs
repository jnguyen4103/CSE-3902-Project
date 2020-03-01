using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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


        // Sprite Info
        public string Name;
        protected Vector2 Origin = new Vector2(0, 0);
        protected Vector2 Size;

        // Sprite Animation & Drawing Info
        protected SpriteBatch Batch;
        protected Texture2D Texture;
        protected Rectangle DrawWindow;
        protected Rectangle AnimationWindow;
        protected SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        protected float Layer = 0;
        public Color Colour = Color.White;

        // Animation & Moving Info
        protected int TotalFrames;
        protected int CurrentFrame = 0;
        public int FPS;
        protected int GameFrame = 0;

        public Vector2 GetSize {  get { return Size; } }

        

        public Vector2 GetPosition {  get { return Position; } }

        public void SetPosition(){
            if (this.Position.X == Game.GraphicsDevice.Viewport.Width)
            {
                this.Position.X = Game.GraphicsDevice.Viewport.Width;
            }
            if (this.Position.Y == Game.GraphicsDevice.Viewport.Height)
            {
                this.Position.X = Game.GraphicsDevice.Viewport.Height;
            }
         
        }


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

            if (Position.X>= Game.GraphicsDevice.Viewport.Width-16)
            {
                Position.X = Game.GraphicsDevice.Viewport.Width - 16;
            }
            else if(Position.X<=0)
            {
                Position.X = 0;
            }
            else if (Position.Y >= Game.GraphicsDevice.Viewport.Height - 16)
            {
                Position.Y = Game.GraphicsDevice.Viewport.Height - 16;
            }
            else if (Position.Y <= 0)
            {
                Position.Y = 0;
            }
        }
        public abstract void ChangeSpriteAnimation(string newSpriteName);

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
            AnimationWindow.Y = (int)(CurrentFrame * Size.Y) + (8 * CurrentFrame);
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}
