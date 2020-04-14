/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class LinkSprite : ISprite
    {
        protected Game1 Game;
        protected Vector2 Origin = new Vector2(0, 0);
        public Vector2 Size;
        public string Name;


        // Sprite Drawing Info
        protected SpriteBatch Batch;
        protected Texture2D Texture;
        protected Vector2 Position;
        protected Rectangle DrawWindow;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        public float Layer = 0.6f;
        private Texture2D YOUWON;
        private Texture2D TriForcePepe;
        public Color Colour { get; set; } = Color.White;

        // Animation & Moving Info
        protected Rectangle AnimationWindow;
        protected float InitalAnimationY;
        public int TotalFrames;
        protected int CurrentFrame = 0;
        public int FPS = 8;
        protected int GameFrame = 0;


        public LinkSprite(Game1 game, string name, Texture2D texture, SpriteBatch batch)
        {
            Tuple<Rectangle, Vector2, int> spriteInfo = game.SFactory.Sprites[name];
            Game = game;
            Batch = batch;
            Name = name;
            Size = spriteInfo.Item2;
            Texture = texture;
            CurrentFrame = 0;
            TotalFrames = spriteInfo.Item3;
            FPS = 8;
            ChangeSpriteAnimation(name);
            TriForcePepe = game.Content.Load<Texture2D>("WinningGame");
            YOUWON = game.Content.Load<Texture2D>("YOU WON");
        }

        public void UpdatePosition(Vector2 position)
        {
            Position = position;
        }

        public void Animate()
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

        public virtual void DrawSprite()
        {
            if (Game.Link.State != States.LinkState.Idle) { Animate(); }
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(InitalAnimationY + (CurrentFrame * Size.Y) + (8 * CurrentFrame));
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }

        public void ChangeSpriteAnimation(string name)
        {
            if (Name != name) { CurrentFrame = 0; }
            Name = name;
            Tuple<Rectangle, Vector2, int> spriteInfo = Game.SFactory.Sprites[name];
            Size = spriteInfo.Item2;
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            InitalAnimationY = spriteInfo.Item1.Y;
            AnimationWindow = new Rectangle(spriteInfo.Item1.X, spriteInfo.Item1.Y * CurrentFrame, (int)spriteInfo.Item2.X, (int)spriteInfo.Item2.Y);
            TotalFrames = spriteInfo.Item3;


        }

        public void Remove()
        {
            Colour = Color.Transparent;
        }
    }
}
