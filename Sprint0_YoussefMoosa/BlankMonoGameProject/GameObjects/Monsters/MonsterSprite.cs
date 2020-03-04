using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Sprint03 { 

    public class MonsterSprite : Sprite
    {
        public MonsterSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = game.SFactory.Sprites[name].Item2;
            this.Position = spawn;
            this.Texture = texture;
            this.TotalFrames = game.SFactory.Sprites[name].Item3;
            this.ChangeSpriteAnimation(name);
        }
    }
}
