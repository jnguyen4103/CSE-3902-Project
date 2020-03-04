using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class BoomerangSprite : Sprite
    {
        private Sprite Creator;
        private Link.LinkDirection Direction;
        private bool Returning = false;
        private int Timer = 0;
        private int Delay = 2;

        public BoomerangSprite(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Direction = direction;
            this.Game = game;
            this.Batch = batch;
            this.Name = "BoomerangEffect";
            this.Size = game.SFactory.Sprites["BoomerangEffect"].Item2;
            this.Position = creator.GetPosition;
            this.Texture = texture;
            this.BaseSpeed = 2.5f;
            this.TotalFrames = game.SFactory.Sprites["BoomerangEffect"].Item3;
            this.ChangeSpriteAnimation("BoomerangEffect");
            this.FPS = 16;
            
            GetSpawnPosition();
        }

        private void GetSpawnPosition()
        {
            this.Position = Creator.GetPosition;
            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    this.Position.X += 4;
                    this.Position.Y += 14;
                    break;
                case (Link.LinkDirection.Up):
                    this.Position.X += 4;
                    this.Position.Y -= 2;
                    break;
                case (Link.LinkDirection.Left):
                    this.Position.X -= 6;
                    this.Position.Y += 4;
                    break;
                case (Link.LinkDirection.Right):
                    this.Position.X += 14;
                    this.Position.Y += 4;
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
                    if (Position.Y >= Game.WalkingRect.Height + 16)
                    {
                        if (Timer < Delay) { HitOjbect(); }
                    }
                    break;

                case (Link.LinkDirection.Up):
                    Position.Y -= BaseSpeed;
                    if (Position.Y <= Game.WalkingRect.Y)
                    {
                        if(Timer < Delay) { HitOjbect(); }
                    }
                    break;

                case (Link.LinkDirection.Left):
                    Position.X -= BaseSpeed;
                    if (Position.X <= Game.WalkingRect.X)
                    {
                        if (Timer < Delay) { HitOjbect(); }
                    }
                    break;

                case (Link.LinkDirection.Right):
                    Position.X += BaseSpeed;
                    if (Position.X >= Game.WalkingRect.Width+16)
                    {
                        if (Timer < Delay) { HitOjbect(); }
                    }
                    break;

                default:
                    break;
            }
        }

        private void HitOjbect()
        {
            BaseSpeed = 0;
            Timer++;
            if (Timer == 1)
            {
                ChangeSpriteAnimation("ProjectileHit");
            }
            else if (Timer >= Delay)
            {
                BaseSpeed = 1.5f;
                Returning = true;
                Timer = 0;
                Layer = 0.5f;
                ChangeSpriteAnimation("BoomerangEffect");
            }
        }

        private void ReturnToCreator()
        {
            if ((Position.X + (Size.X / 2)) > Creator.Position.X + (Creator.GetSize.X / 2)) { Position.X -= BaseSpeed; }
            if ((Position.X + (Size.X / 2)) < Creator.Position.X + (Creator.GetSize.X / 2)) { Position.X += BaseSpeed; }
            if ((Position.Y + (Size.Y / 2)) > Creator.Position.Y + (Creator.GetSize.Y / 2)) { Position.Y -= BaseSpeed; }
            if ((Position.Y + (Size.Y / 2)) < Creator.Position.Y + (Creator.GetSize.Y / 2)) { Position.Y += BaseSpeed; }
        }

        public override void DrawSprite()
        {
            if(!Returning)
            {
                Move();
            } else
            {
                ReturnToCreator();
            }

            Animate();
            DrawWindow.X = (int)Position.X;
            DrawWindow.Y = (int)Position.Y;
            AnimationWindow.Y = (int)(InitalAnimationY + (CurrentFrame * Size.Y) + (8 * CurrentFrame));
            Batch.Draw(Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer);
        }
    }
}