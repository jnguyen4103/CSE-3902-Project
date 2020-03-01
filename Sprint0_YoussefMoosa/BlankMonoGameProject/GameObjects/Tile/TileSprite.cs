using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    class TileSprite : Sprite
    {

        public TileSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = new Vector2(16, 16);
            this.Position = spawn;
            this.Texture = texture;
            this.ChangeSpriteAnimation(name);
        }

 

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            throw new NotImplementedException();
        }
    }
}
