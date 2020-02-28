using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class StalfosSprite : MonsterSprite
    {
        private bool FlipFlag = false;

        public StalfosSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = game.Factory.MonsterSprites[name].Item2;
            this.Position = spawn;
            this.PathPosition = spawn;
            this.Texture = texture;
            this.BaseSpeed = 0.5f;
            this.TotalFrames = game.Factory.MonsterSprites[name].Item3;
            this.FPS = 8;
            this.ChangeSpriteAnimation(name);
        }

        public override void Animate()
        {
            GameFrame++;
            if (CurrentSpeed.X != 0 || CurrentSpeed.Y != 0)
            {
                if ((60 / FPS <= GameFrame))
                {
                    GameFrame = 0;
                    if (FlipFlag)
                    {
                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        this.SpriteEffect = SpriteEffects.None;
                    }
                    FlipFlag = !FlipFlag;
                }
            }
        }

    }
}
