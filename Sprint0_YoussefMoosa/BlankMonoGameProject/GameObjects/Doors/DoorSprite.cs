using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class DoorSprite: StaticSprite
    {
        public DoorSprite(Game1 game, string name, string side, Texture2D texture, SpriteBatch batch)
        {
            Game = game;
            Name = name;
            Batch = batch;
            Texture = texture;
            PlaceDoor(side);
        }

        private void PlaceDoor(string side)
        {
            switch(side)
            {
                case "Up":
                    ChangeSpriteAnimation(Name);
                    Position.X = 112;
                    Position.Y = 64;
                    break;

                case "Down":
                    SpriteEffect = SpriteEffects.FlipVertically;
                    ChangeSpriteAnimation(Name);
                    Position.X = 112;
                    Position.Y = 208;
                    break;

                case "Left":
                    ChangeSpriteAnimation(Name + "Horizontal");
                    Position.X = 0;
                    Position.Y = 136;

                    break;

                case "Right":
                    ChangeSpriteAnimation(Name + "Horizontal");
                    Position.X = 224;
                    Position.Y = 136;
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    break;

                default:
                    break;
            }
        }
    }
}
