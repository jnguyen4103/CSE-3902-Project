using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class StillTextSprite : ISprite
    {
        SpriteFont spriteFont;
        private string Text;

        public StillTextSprite(SpriteFont font, string text)
        {
            spriteFont = font;
            Text = text;
        }
        public void Update()
        {}
        public void DrawSprite(SpriteBatch spriteBatch, Vector2 location)
        {

            spriteBatch.Begin();

            spriteBatch.DrawString(spriteFont, Text, location, Color.Black);

            spriteBatch.End();
        }
    }
}
