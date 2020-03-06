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
            IgnoresBoundaries = true;
            this.Size = game.SFactory.Sprites[name].Item2;
            this.Position = spawn;
            this.Texture = texture;
            this.BaseSpeed = 0.5f;
            this.TotalFrames = game.SFactory.Sprites[name].Item3;
            this.FPS = 6;
            this.ChangeSpriteAnimation(name);
        }

    }
}
