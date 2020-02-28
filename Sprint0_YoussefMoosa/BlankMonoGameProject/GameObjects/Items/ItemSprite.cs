using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    class ItemSprite : Sprite
    {

        public ItemSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = game.SFactory.ItemSprites[name].Item2;
            this.Position = spawn;
            this.Texture = texture;
            this.BaseSpeed = 0.5f;
            this.TotalFrames = game.SFactory.ItemSprites[name].Item3;
            this.FPS = 4;
            this.ChangeSpriteAnimation(name);
        }

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.SFactory.ItemSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }
    }
}
