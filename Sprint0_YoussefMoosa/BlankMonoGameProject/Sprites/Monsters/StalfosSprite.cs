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
        private bool FlipFlag = false;

        public StalfosSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            this.Game = game;
            this.Batch = batch;
            this.Name = name;
            this.Size = game.Factory.MonsterSprites[name].Item2;
            this.Position = spawn;
            this.Texture = texture;
            this.BaseSpeed = 0.5f;
            this.TotalFrames = game.Factory.MonsterSprites[name].Item3;
            this.FPS = 4;
            this.ChangeSpriteAnimation(name);
        }

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.Factory.MonsterSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
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
