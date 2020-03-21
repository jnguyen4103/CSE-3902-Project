/* Contributors
* Nico Negrete
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint03
{
    public class Fairy : IItem
    {
        protected Game1 Game;
        public StaticSprite Sprite { get; set; }
        private Vector2 Path { get; set; }
        private Vector2 Velocity { get; set; }
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }

        private string ItemName;
        private float BaseSpeed = 0.5f;
        private Vector2 velocity;
        private int Timer = 0;
        private int MoveReset = 0;

        public Fairy(Game1 game, string spriteName, string itemName, Vector2 spawn)
        {
            Game = game;
            Sprite = new StaticSprite(Game, spriteName, spawn, Game.ItemSpriteSheet, Game.spriteBatch);
            Position = spawn;
            ItemName = itemName;
        }
        public void ActivateItem()
        {
            if (!Sprite.Colour.Equals(Color.Transparent)) { Game.IFactory.UseItem[ItemName](); }
            Sprite.Remove();
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {
            Timer++;
            if(Timer >= MoveReset) { Move(); }
            Position += velocity;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Sprite.UpdatePosition(Position);
        }


        private void Move()
        {
            velocity = Vector2.Zero;
            velocity.X = BaseSpeed*(Game1.random.Next(1, 4) - 2);
            velocity.Y = BaseSpeed * (Game1.random.Next(1, 4) - 2);
            MoveReset = Game1.random.Next(300, 600);
        }

    }
}
