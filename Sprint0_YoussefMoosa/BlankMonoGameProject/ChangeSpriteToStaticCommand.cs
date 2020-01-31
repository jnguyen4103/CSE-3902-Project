using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0_YoussefMoosa
{
    class ChangeSpriteToStaticCommand : ICommand
    {
        private Game1 monoProcess;
        private Texture2D spriteSheet;
        private Rectangle source;
        private Rectangle destination;
        private SpriteBatch batch;

        public ChangeSpriteToStaticCommand(Game1 monoInstance,
            Texture2D sheet, Rectangle sprite, Rectangle locationOnScreen, SpriteBatch spriteBatch
            )
        {
            this.monoProcess = monoInstance;
            this.spriteSheet = sheet;
            this.source = sprite;
            this.destination = locationOnScreen;
            this.batch = spriteBatch;
        }

        public void Execute()
        {
            monoProcess.LinkSprite = new NoMoveNoAnimSprite(spriteSheet, source, destination, batch);
        }
    }
}
