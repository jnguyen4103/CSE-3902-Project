using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class BeamExplosionSprite : Sprite
    {
        private int Lifespan = 30;
        private int timer = 0;

        public BeamExplosionSprite(Game1 game, Vector2 spawn, Vector2 speed,  Texture2D texture, SpriteBatch batch)
        {
            Game = game;
            Batch = batch;
            CurrentSpeed = speed;
            Position = spawn;
            Texture = texture;
            FPS = 24;
            Name = "SwordBeamExplosion";
            Size = Game.SFactory.Sprites[Name].Item2;
            TotalFrames = Game.SFactory.Sprites[Name].Item3;
            ChangeSpriteAnimation(Name);
        }

        public override void Move()
        {
            Position.X += CurrentSpeed.X;
            Position.Y += CurrentSpeed.Y;
        }

        public override void DrawSprite()
        {
            base.DrawSprite();
            timer++;
            if(timer >= Lifespan)
            {
                KillSprite();
            }
        }

    }
}
