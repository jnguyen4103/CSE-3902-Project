using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class StalfosSprite : Sprite
    {

        public StalfosSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = game.SFactory.Sprites[name].Item2;
            this.Position = spawn;
            this.Texture = texture;
            this.BaseSpeed = 0.0f;
            this.TotalFrames = game.SFactory.Sprites[name].Item3;
            this.FPS = 4;
            this.ChangeSpriteAnimation(name);
        }

    }
}
