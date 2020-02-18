using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public abstract class ItemFactory
    {
        // Keeps track of basic info about items
        protected Texture2D itemTexture;
        protected Vector2 itemLocation;
        protected SpriteBatch spriteBatch;
        public abstract void ActivateEffect();
        public void DeleteItem()
        {
            // Delete item
        }

        public abstract void DrawItem();
    }
}
