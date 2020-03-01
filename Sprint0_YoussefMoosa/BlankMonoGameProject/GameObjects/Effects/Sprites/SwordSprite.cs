using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class SwordSprite : Sprite
    {
        private Sprite Creator;
        private Link.LinkDirection Direction;
        private int Lifespan;
        private int timer = 0;
        public SwordSprite(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Direction = direction;
            this.Game = game;
            this.Batch = batch;
            this.Name = "SwordSwing";
            this.Size = game.SFactory.EffectSprites["SwordSwing"].Item2;
            this.Position = creator.GetPosition;
            this.Texture = texture;
            this.TotalFrames = game.SFactory.EffectSprites["SwordSwing"].Item3;
            this.ChangeSpriteAnimation("SwordSwing");
            this.FPS = 12;
            Lifespan = (60 / FPS) * 3;
            UpdatePosition();
        }
        public override void ChangeSpriteAnimation(string newSpriteName) 
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.SFactory.EffectSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }

        private void UpdatePosition()
        {
            this.Position = Creator.GetPosition;

            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    this.Position.X += 6;
                    this.Position.Y += 12;
                    this.SpriteEffect = SpriteEffects.FlipVertically;
                    break;
                case (Link.LinkDirection.Up):
                    this.Position.X += 3;
                    this.Position.Y -= 13;
                    break;
                case (Link.LinkDirection.Left):
                    this.Position.X -= 12;
                    this.Position.Y += 12;
                    this.Rotation = (float)(3*Math.PI / 2);
                    break;
                case (Link.LinkDirection.Right):
                    this.Position.X += 28;
                    this.Position.Y += 6;
                    this.Rotation = (float) (Math.PI / 2);
                    break;
                default:
                    break;
            }
        }

        public override void DrawSprite()
        {
            timer++;
            if (timer >= Lifespan)
            {
                KillSprite();
                timer = 0;
            }

            UpdatePosition();
            Move();
            Animate();
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(CurrentFrame * Size.Y) + (8 * CurrentFrame);
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}
