﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class Compass : ItemFactory
    {
        public Compass(Texture2D texture, Vector2 spawnPosition, SpriteBatch _spriteBatch)
        {
            this.itemTexture = texture;
            this.itemLocation = spawnPosition;
            this.spriteBatch = _spriteBatch;
        }
        public override void ActivateEffect()
        {
            // Add for later sprints
        }

        public override void DrawItem()
        {
            int frameWidth = 11;
            int frameHeight = 12;


            Rectangle srcRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            Rectangle destRectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, frameWidth, frameHeight);
            spriteBatch.Draw(this.itemTexture, destRectangle, srcRectangle, Color.White);

        }
    }
}
