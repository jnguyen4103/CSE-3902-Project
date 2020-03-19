using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    // Abstract class allows for easy reuse of methods and variables
    // that all NPCSprites share
    // Look at Stalfos and Goriyas for comments on their NPC Sprite implementation
    public class StaticSprite: ISprite
    {
         Game1 Game;
         Vector2 Origin = new Vector2(0, 0);
        public Vector2 Size;
        public string Name;


        // Sprite Drawing Info
         SpriteBatch Batch;
         Texture2D Texture;
         Vector2 Position;
         Rectangle DrawWindow;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        float Rotation = 0;
        public float Layer = 0;
        public Color Colour { get; set; } = Color.White;

        // Animation & Moving Info
         Rectangle AnimationWindow;
         float InitalAnimationY;
        public int TotalFrames;
         int CurrentFrame = 0;
        public int FPS = 8;
         int GameFrame = 0;

        public StaticSprite(Game1 game, string name, Vector2 spawn, Texture2D texture, SpriteBatch batch)
        {
            Game = game;
            Name = name;
            Position = spawn;
            Texture = texture;
            Batch = batch;
            ChangeSpriteAnimation(name);
        }

        public void UpdatePosition(Vector2 position)
        {
            Position = position;
        }

         void Animate()
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

        public void DrawSprite()
        {
            Animate();
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(InitalAnimationY + (CurrentFrame * Size.Y) + (8 * CurrentFrame));
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}
