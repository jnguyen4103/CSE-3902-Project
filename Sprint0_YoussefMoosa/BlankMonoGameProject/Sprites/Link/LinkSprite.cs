using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class LinkSprite : CharacterSprite
    {
        private bool FlipFlag = false;

        public LinkSprite(Game1 game, String name, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            Game = game;
            Batch = batch;
            Name = name;
            Size = game.Factory.LinkSprites[name].Item2;
            Position = spawn;
            Texture = texture;
            CurrentFrame = 0;
            TotalFrames = game.Factory.LinkSprites[name].Item3;
            FPS = 4;
            ChangeSpriteAnimation(name);
            BaseSpeed = 0.75f;

            // Setting up conditions for testing
            // Remove if still presnet
            this.CurrentSpeed.Y = BaseSpeed;
        }

        public override void ChangeSpriteAnimation(string newSpriteName)
        {
            Name = newSpriteName;
            CurrentFrame = 0;
            Tuple<Rectangle, Vector2, int> NewInfo = Game.Factory.LinkSprites[newSpriteName];
            DrawWindow = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            AnimationWindow = new Rectangle(NewInfo.Item1.X, NewInfo.Item1.Y * CurrentFrame, (int)NewInfo.Item2.X, (int)NewInfo.Item2.Y);
            TotalFrames = NewInfo.Item3;
        }


        public override void Animate()
        {
            if (CurrentSpeed.X != 0 || CurrentSpeed.Y != 0)
            {
                GameFrame++;
                if ((60 / FPS <= GameFrame))
                {
                    GameFrame = 0;
                    CurrentFrame++;
                    if (FlipFlag && (CurrentSpeed.X < 0) || (CurrentSpeed.Y < 0))
                    {
                        this.SpriteEffect = SpriteEffects.FlipHorizontally;
                        FlipFlag = !FlipFlag; 
                    }
                    else
                    {
                        this.SpriteEffect = SpriteEffects.None;
                    }
                    if (CurrentFrame == TotalFrames)
                    {
                        CurrentFrame = 0;
                    }
                }
            }

        }

        public void UpdateSpeed(Vector2 newSpeed)
        {
            // Call this method in the StateMachine
            // Update speed based on direction
            // Ex: Moving up = (0, -0.75)
            // The Move method moves the sprite based on his speed
            // His Idle, Damaged and Attacking States will set the speed to 0
            // Consider adding flags if necessary
            // If no flags are added then change fuction name to just
            // UpdateSpeed or ChangeSpeed

            CurrentSpeed = newSpeed;
        }

    }
}
