using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0_YoussefMoosa
{
    class NoMoveNoAnimSprite : ISprite
    {
        private readonly Texture2D spriteSheet;
        private readonly Rectangle sprite;
        private readonly Rectangle dest;
        private readonly SpriteBatch batch;

        public NoMoveNoAnimSprite(Texture2D sheet, Rectangle sectionOnSheet, 
            Rectangle locationOnScreen, SpriteBatch spriteBatch) 
        {
            this.spriteSheet = sheet;
            this.sprite = sectionOnSheet;
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
        }

        public void DrawSprite() 
        {
            batch.Begin();
            batch.Draw(spriteSheet, dest, sprite, Color.White);
            batch.End();
        }
    }
}
