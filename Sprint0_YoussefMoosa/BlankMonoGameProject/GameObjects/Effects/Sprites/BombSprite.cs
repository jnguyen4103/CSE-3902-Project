
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class BombSprite : Sprite
    {
        private Sprite Creator;
        private static readonly Random random = new Random();
        private int Timer = 0;
        private int Delay = 90;
        public BombSprite(Sprite creator, Game1 game, Vector2 spawn, Texture2D texture, SpriteBatch batch)
        {
            Name = "BombEffect";
            Creator = creator;
            FPS = 2;
            Game = game;
            Position = spawn;
            Batch = batch;
            Texture = texture;
            Size = Game.SFactory.Sprites[Name].Item2;
            TotalFrames = Game.SFactory.Sprites[Name].Item3;
            ChangeSpriteAnimation(Name);
        }

        public override void DrawSprite()
        {
            Console.WriteLine(Position);
            if (Timer < Delay)
            {
                Timer++;
                base.DrawSprite();

            }
            else
            {
                CreateExplosion();
                KillSprite();
            }
        }

        private void CreateExplosion()
        {
            // 3x3 explosion

            // Columns
            for (int i = 0; i < 3; i++)
            {
                Vector2 pos = new Vector2(Position.X, Position.Y);
                pos.X += (i * 16) - 16;
                pos.Y -= 16;

                // Row
                for (int j = 0; j < 3; j++)
                {
                    pos.Y += (j) * 16;
                    IEffect explosion = new ExplosionEffect(Creator, Game, pos, Texture, Batch);
                    explosion.CreateEffect();
                    pos = new Vector2(pos.X, Position.Y - 16);

                }

            }


        }
    
    }
}