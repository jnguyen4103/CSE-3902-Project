using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class BoomerangSprite : Sprite
    {
        private Sprite Creator;
        private Link.LinkDirection Direction;
        private int LifeSpan;
        private int LifeCounter = 0;
        public BoomerangSprite(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Direction = direction;
            this.Game = game;
            this.Batch = batch;
            this.Name = "Boomerang";
            this.Size = game.Factory.EffectSprites["Boomerang"].Item2;
            this.Position = creator.GetPosition;
            this.Texture = texture;
            this.BaseSpeed = 2.5f;
            this.TotalFrames = game.Factory.EffectSprites["Boomerang"].Item3;
            this.ChangeSpriteAnimation("Boomerang");
            this.FPS = 16;
            LifeSpan = 600;
            GetSpawnPosition();
        }
        public override void ChangeSpriteAnimation(string newSpriteName) 
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.Factory.EffectSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }

        private void GetSpawnPosition()
        {
            this.Position = Creator.GetPosition;
            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    this.Position.X += 12;
                    this.Position.Y += 20;
                    this.Rotation = (float)Math.PI;
                    break;
                case (Link.LinkDirection.Up):
                    this.Position.X += 4;
                    this.Position.Y -= 4;
                    break;
                case (Link.LinkDirection.Left):
                    this.Position.X -= 4;
                    this.Position.Y += 12;
                    this.Rotation = (float)(3 * Math.PI / 2);
                    break;
                case (Link.LinkDirection.Right):
                    this.Position.X += 20;
                    this.Position.Y += 4;
                    this.Rotation = (float)(Math.PI / 2);
                    break;
                default:
                    break;
            }
        }

        public override void Move()
        {
            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    Position.Y += BaseSpeed;
                    break;
                case (Link.LinkDirection.Up):
                    Position.Y -= BaseSpeed;
                    break;
                case (Link.LinkDirection.Left):
                    Position.X -= BaseSpeed;
                    break;
                case (Link.LinkDirection.Right):
                    Position.X += BaseSpeed;
                    break;
                default:
                    break;
            }
        }

        public override void DrawSprite()
        {
            if (LifeCounter <= LifeSpan)
            {
                LifeCounter++;
                Move();
                Animate();
                DrawWindow.X = (int)Position.X;
                DrawWindow.Y = (int)Position.Y;
                AnimationWindow.Y = (int)(CurrentFrame * Size.Y);
                Batch.Draw(Texture, DrawWindow, AnimationWindow, Color.White, Rotation, Origin, SpriteEffect, Layer);
            }

        }
    }
}
