using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class Item
    {
        public Sprite Sprite;
        private Game1 Game;
        private string ItemName;

        public Item(Game1 game, String spriteName, String itemName, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            Sprite = new ItemSprite(game, spriteName, texture, spawn, batch);
            Game = game;
            ItemName = itemName;
        }
        public void ActivateItem()
        {
            Game.IFactory.UseItem[ItemName]();
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }
    }
}
