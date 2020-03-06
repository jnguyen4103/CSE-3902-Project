using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public abstract class Tile
    {
        public Game1 Game;
        public Sprite Sprite;
        protected bool isDestroyed = false;


        public void Destroy()
        {
            isDestroyed = !isDestroyed;
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }
        public virtual void Move()
        {
            Sprite.Position.Y -= .5f;
        }
        public virtual void DestroyTile()
        {
           Sprite.Colour = Color.Transparent;
        }
    }
}
