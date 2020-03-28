/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class Item: IItem
    {
        protected Game1 Game;
        public StaticSprite Sprite { get; set; }
        private string ItemName;

        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public bool isDrawn = true;
        public Item(Game1 game, string spriteName, string itemName, Vector2 spawn)
        {
            Game = game;
            Sprite = new StaticSprite(game, spriteName, spawn, game.ItemSpriteSheet, game.spriteBatch);
            Position = spawn;
            ItemName = itemName;


        }


        public void ActivateItem()
        {
         
            if (!Sprite.Colour.Equals(Color.Transparent)) { Game.IFactory.UseItem[ItemName](); }
            Sprite.Remove();
            switch (ItemName)
            {
                case "Rupee":
                    Game.soundEffects[11].Play();
                    break;
                case "Heart":
                    Game.soundEffects[9].Play();
                    break;
                default:
                    Game.soundEffects[10].Play();
                    break;
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
            
            if (ItemName == "Key" && isDrawn)
            {
                Game.soundEffects[12].Play();
                isDrawn = false;
            }
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);

        }

    }
}
