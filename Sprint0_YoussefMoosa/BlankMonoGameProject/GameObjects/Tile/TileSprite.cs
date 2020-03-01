using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    class TileSprite : Sprite
    {

        public TileSprite(Game1 game, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {

            //Texture, DrawWindow, AnimationWindow, Colour, Rotation, Origin, SpriteEffect, Layer
            this.Game = game;
            this.Batch = batch;
            this.Size = new Vector2(16, 16);
            this.Position = spawn;
            this.Texture = texture;
            this.DrawWindow = new Rectangle(0, 0, 16, 16);
            this.Colour = Color.White;
        }

 

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            throw new NotImplementedException();
        }
    }
}
