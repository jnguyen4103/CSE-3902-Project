using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class StillSprite : ISprite
    {
        private Texture2D Texture;

        public StillSprite(Texture2D texture)
        {
            Texture = texture;
        }
        public void Update()
        {}
        public void DrawSprite(SpriteBatch spriteBatch, Vector2 location)
        { 
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;

            sourceRectangle = new Rectangle(37, 150, 29, 55);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 87, 165);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
