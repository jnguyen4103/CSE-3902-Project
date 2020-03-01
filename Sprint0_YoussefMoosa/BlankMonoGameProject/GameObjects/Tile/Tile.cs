﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class Block
    {
        public Game1 Game;
        public Sprite Sprite;
        protected bool isDestroyed = false;

        public Block(Game1 game, String spriteName, Texture2D texture, Vector2 spawn, SpriteBatch batch)
        {
            
            Game = game;
            Sprite = new TileSprite(game, spriteName, texture, spawn, batch);
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
