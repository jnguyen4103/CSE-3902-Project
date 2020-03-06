using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class FireballSprite : Sprite
    {
        Sprite Creator;
        Vector2 Direction;

        public FireballSprite(Sprite creator, Game1 game, Vector2 direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Name = "Fireball";
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
            Position.X = Creator.Position.X + 3;
            Position.Y = Creator.Position.Y + 10;
        }

        public override void Move()
        {
            Position.X += Direction.X;
            Position.Y += Direction.Y;
        }
    }
}
