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
                    this.Position.X -= 2;
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
                    if (Position.Y >= Game.WalkingRect.Height+16)
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
                    if (Position.X >= Game.WalkingRect.Width+16)
                        this.KillSprite();
                    break;
                default:
                    break;
            }


        }
    }
}