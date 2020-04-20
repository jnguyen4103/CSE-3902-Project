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
            switch (ItemName)
            {
                case "Rupee":
                    Game.soundEffects[11].Play();
                    Sprite.Remove();
                    break;
                case "Heart":
                    Game.soundEffects[9].Play();
                    Sprite.Remove();
                    break;
                case "Triforce":
                    Sprite.UpdatePosition(new Vector2(Game.Link.Position.X, Game.Link.Position.Y - 16));
                    break;
                case "OldMan":
                    CollisionHandler.LinkHitBlock(Game.Link,new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y));
                    break;
                case "Merchant":
                    CollisionHandler.LinkHitBlock(Game.Link, new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y));
                    break;
                case "OldManFire":
                    CollisionHandler.LinkHitBlock(Game.Link, new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y));
                    break;
                case "Skull":
                    Sprite.UpdatePosition(new Vector2(Game.Link.Position.X, Game.Link.Position.Y - 16));
                    break;
                default:
                    Game.soundEffects[10].Play();
                    Sprite.Remove();
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
