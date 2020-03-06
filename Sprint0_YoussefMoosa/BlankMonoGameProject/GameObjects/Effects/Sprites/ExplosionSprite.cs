using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class ExplosionSprite : Sprite
    {
        
        Sprite Creator;
        int Timer = 0;
        int Delay = 25;

        public ExplosionSprite(Sprite creator, Game1 game, Vector2 spawn, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Name = "ExplosionEffect";
            IgnoresBoundaries = true;
            Game = game;
            FPS = 6;
            Position = spawn;
            Batch = batch;
            Texture = texture;
            BaseSpeed = 0;
            Size = Game.SFactory.Sprites[Name].Item2;
            TotalFrames = Game.SFactory.Sprites[Name].Item3;
            ChangeSpriteAnimation(Name);
        }

        public override void Animate()
        {
            Colour = Color.White * (1 - 0.10f * CurrentFrame);
            base.Animate();
        }

        public override void DrawSprite()
        {
            if (Timer < Delay)
            {
                Timer++;
                base.DrawSprite();

            }
            else
            {
                KillSprite();
            }
        }
    }
}
