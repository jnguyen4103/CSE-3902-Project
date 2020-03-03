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
            UpdatePosition();
            this.Size = game.SFactory.Sprites[Name].Item2;
            this.Texture = texture;
            this.TotalFrames = game.SFactory.Sprites[Name].Item3;
            this.ChangeSpriteAnimation(Name);
            this.FPS = 12;
            IgnoresBoundaries = true;
            Lifespan = (60 / FPS) * 3;
        }

        private void UpdatePosition()
        {
            this.Position = Creator.GetPosition;

            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    this.Position.X += 6;
                    this.Position.Y += 12;
                    Name = "SwordSwing";
                    this.SpriteEffect = SpriteEffects.FlipVertically;
                    break;

                case (Link.LinkDirection.Up):
                    this.Position.X += 3;
                    this.Position.Y -= 13;
                    Name = "SwordSwing";
                    break;

                case (Link.LinkDirection.Left):
                    this.Position.X -= 12;
                    this.Position.Y += 6;
                    Name = "SwordSwingHorizontal";
                    break;

                case (Link.LinkDirection.Right):
                    this.Position.X += 12;
                    this.Position.Y += 6;
                    Name = "SwordSwingHorizontal";
                    this.SpriteEffect = SpriteEffects.FlipHorizontally;
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
