using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class SwordBeamSprite : Sprite
    {
        private Sprite Creator;
        private Link.LinkDirection Direction;

        public SwordBeamSprite(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
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
            this.BaseSpeed = 3.5f;
            this.FPS = 32;
        }

        private void UpdatePosition()
        {
            this.Position = Creator.GetPosition;

            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    this.Position.X += 6;
                    this.Position.Y += 12;
                    Name = "SwordBeam";
                    this.SpriteEffect = SpriteEffects.FlipVertically;
                    break;

                case (Link.LinkDirection.Up):
                    this.Position.X += 3;
                    this.Position.Y -= 13;
                    Name = "SwordBeam";
                    break;

                case (Link.LinkDirection.Left):
                    this.Position.X -= 12;
                    this.Position.Y += 6;
                    Name = "SwordBeamHorizontal";
                    break;

                case (Link.LinkDirection.Right):
                    this.Position.X += 12;
                    this.Position.Y += 6;
                    Name = "SwordBeamHorizontal";
                    this.SpriteEffect = SpriteEffects.FlipHorizontally;
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
                        this.KillSprite();
                    break;
                case (Link.LinkDirection.Up):
                    Position.Y -= BaseSpeed;
                    if (Position.Y <= Game.WalkingRect.Y)
                        this.KillSprite();
                    break;
                case (Link.LinkDirection.Left):
                    Position.X -= BaseSpeed;
                    if (Position.X <= Game.WalkingRect.X)
                        this.KillSprite();
                    break;
                case (Link.LinkDirection.Right):
                    Position.X += BaseSpeed;
                    if (Position.X >= Game.WalkingRect.Width + 16)
                        this.KillSprite();
                break;
                default:
                    break;
            }


        }

    }
}
