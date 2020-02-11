using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0_YoussefMoosa
{
    class ChangeSpriteToMoveCommand : ICommand
    {
        private readonly Game1 monoProcess;
        private readonly Texture2D spriteSheet;
        private readonly Rectangle sprite;
        private Rectangle dest;
        private Vector2 screen;
        private Vector2 velocity;
        private readonly SpriteBatch batch;

        public ChangeSpriteToMoveCommand(Game1 monoInstance, Texture2D sheet, Rectangle sectionOnSheet,
           Rectangle locationOnScreen, Vector2 vel, Vector2 screenDim, SpriteBatch spriteBatch)
        {
            this.monoProcess = monoInstance;
            this.spriteSheet = sheet;
            this.sprite = sectionOnSheet;
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.velocity = vel;
            this.screen = screenDim;
            this.batch = spriteBatch;
        }

        public void Execute()
        {
            this.monoProcess.LinkSprite = new NoAnimSprite
                (spriteSheet, sprite, dest, velocity, screen, batch);
        }
    }

}
