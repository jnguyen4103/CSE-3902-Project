using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class Tile
    {
        public Game1 Game;
        public Sprite Sprite;
        protected bool isDestroyed = false;

        public Tile(Game1 game,  Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            
            Game = game;
            Sprite = new TileSprite(game, texture, spawn, batch);
        }

        public void Destroy()
        {
            isDestroyed = !isDestroyed;
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

    }
}
