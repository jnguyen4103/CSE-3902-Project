using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    // Abstract class allows for easy reuse of methods and variables
    // that all NPCSprites share
    // Look at Stalfos and Goriyas for comments on their NPC Sprite implementation
    public abstract class StaticSprite: ISprite
    {

        // Variables for keeping track of Sprite's position on the screen
        protected Game1 Game;

        // Position & Movement Info
        public Vector2 Position;
        protected float InitalAnimationY;


        // Sprite Info
        public string Name;
        protected Vector2 Origin = new Vector2(0, 0);
        protected Vector2 Size;
        protected bool IgnoresBoundaries = true;

        // Sprite Animation & Drawing Info
        protected SpriteBatch Batch;
        protected Texture2D Texture;
        protected Rectangle DrawWindow;
        protected Rectangle AnimationWindow;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        protected float Rotation = 0;
        public float Layer = 0;
        public Color Colour = Color.White;

        // Animation & Moving Info

        public Vector2 GetSize {  get { return Size; } }
        public Vector2 GetPosition {  get { return Position; } }

        public void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.SFactory.Sprites[newSpriteName];
            Size = NewInfo.Item2;
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            InitalAnimationY = NewInfo.Item1.Y;
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
        }

        public virtual void KillSprite()
        {
            Colour = Color.Transparent;
        }

        public virtual void DrawSprite()
        {
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }

    public class FloorSprite: StaticSprite
    {
        public FloorSprite(Game1 game, string name, Texture2D texture, SpriteBatch batch)
        {
            Game = game;
            Name = name;
            Texture = texture;
            Batch = batch;
            Position = new Vector2(32, 96);
            ChangeSpriteAnimation(name);
        }
    }
}
