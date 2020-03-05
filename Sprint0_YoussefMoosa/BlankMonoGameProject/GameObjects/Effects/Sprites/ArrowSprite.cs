using Microsoft.Xna.Framework.Graphics;
namespace Sprint03
{
    public class ArrowSprite : Sprite
    {
        private Sprite Creator;
        private Link.LinkDirection Direction;
        private int Timer = 0;
        private int Delay = 2;
        public ArrowSprite(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Direction = direction;
            Game = game;
            Batch = batch;
            Position = creator.GetPosition;
            Texture = texture;
            BaseSpeed = 2.5f;
            GetSpawnPosition();
            Size = Game.SFactory.Sprites[Name].Item2;
            TotalFrames = Game.SFactory.Sprites[Name].Item3;
            ChangeSpriteAnimation(Name);
        }

        private void GetSpawnPosition()
        {
            Position = Creator.GetPosition;
            switch (Direction)
            {
                case (Link.LinkDirection.Down):
                    Position.X += 4;
                    Position.Y += 12;
                    Name = "ArrowEffect";
                    SpriteEffect = SpriteEffects.FlipVertically;
                    break;
                case (Link.LinkDirection.Up):
                    Position.X += 4;
                    Position.Y -= 4;
                    Name = "ArrowEffect";
                    break;
                case (Link.LinkDirection.Left):
                    Position.X -= 14;
                    Position.Y += 4;
                    Name = "ArrowEffectHorizontal";
                    break;
                case (Link.LinkDirection.Right):
                    Position.X += 14;
                    Position.Y += 4;
                    Name = "ArrowEffectHorizontal";
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
            ChangeSpriteAnimation("ProjectileHit");
            BaseSpeed = 0;
        }

        public override void DrawSprite()
        {
            if(!Name.Equals("ProjectileHit"))
            {
                base.DrawSprite();

            }
            else
            {
                Timer++;
                base.DrawSprite();
            }

            if(Timer >= Delay)
            {
                base.KillSprite();
            }
        }
    }
}
