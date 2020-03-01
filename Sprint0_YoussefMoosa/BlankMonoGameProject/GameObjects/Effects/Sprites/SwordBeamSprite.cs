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
            Game = game;
            Batch = batch;
            UpdatePosition();
            Size = game.SFactory.Sprites[Name].Item2;
            Texture = texture;
            TotalFrames = game.SFactory.Sprites[Name].Item3;
            ChangeSpriteAnimation(Name);
            BaseSpeed = 3.5f;
            FPS = 32;
        }

        private void UpdatePosition()
        {
            Position = Creator.GetPosition;

            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    Position.X += 6;
                    Position.Y += 12;
                    Name = "SwordBeam";
                    SpriteEffect = SpriteEffects.FlipVertically;
                    break;

                case (Link.LinkDirection.Up):
                    Position.X += 3;
                    Position.Y -= 13;
                    Name = "SwordBeam";
                    break;

                case (Link.LinkDirection.Left):
                    Position.X -= 12;
                    Position.Y += 6;
                    Name = "SwordBeamHorizontal";
                    break;

                case (Link.LinkDirection.Right):
                    Position.X += 12;
                    Position.Y += 6;
                    Name = "SwordBeamHorizontal";
                    SpriteEffect = SpriteEffects.FlipHorizontally;
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
                        KillSprite();
                    break;
                case (Link.LinkDirection.Up):
                    Position.Y -= BaseSpeed;
                    if (Position.Y <= Game.WalkingRect.Y)
                        KillSprite();
                    break;
                case (Link.LinkDirection.Left):
                    Position.X -= BaseSpeed;
                    if (Position.X <= Game.WalkingRect.X)
                        KillSprite();
                    break;
                case (Link.LinkDirection.Right):
                    Position.X += BaseSpeed;
                    if (Position.X >= Game.WalkingRect.Width + 16)
                        KillSprite();
                break;
                default:
                    break;
            }
        }


        public override void KillSprite()
        {

            base.KillSprite();
            IEffect UpperLeftExplosion = new BeamExplosionEffect(Creator, Game, Position, new Vector2(-0.5f, -0.5f), Texture, Batch);
            IEffect UpperRightExplosion = new BeamExplosionEffect(Creator, Game, Position, new Vector2(0.5f, -0.5f), Texture, Batch);
            IEffect LowerLeftExplosion = new BeamExplosionEffect(Creator, Game, Position, new Vector2(-0.5f, 0.5f), Texture, Batch);
            IEffect LowerRightExplosion = new BeamExplosionEffect(Creator, Game, Position, new Vector2(0.5f, 0.5f), Texture, Batch);

            UpperLeftExplosion.CreateEffect();
            UpperRightExplosion.CreateEffect();
            LowerLeftExplosion.CreateEffect();
            LowerRightExplosion.CreateEffect();

            UpperRightExplosion.Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            LowerLeftExplosion.Sprite.SpriteEffect = SpriteEffects.FlipVertically;
            LowerRightExplosion.Sprite.SpriteEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;



        }

    }
}
