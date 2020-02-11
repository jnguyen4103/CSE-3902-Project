using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0_YoussefMoosa
{
    class ChangeSpriteToAnimateCommand : ICommand
    {
        private readonly Game1 monoProcess;
        private readonly Texture2D spriteSheet;
        private readonly Rectangle[] animation;
        private int animTime;
        private Rectangle dest;
        private readonly SpriteBatch batch;

        public ChangeSpriteToAnimateCommand(Game1 monoInstance, Texture2D sheet, Rectangle[] sectionsOnSheet,
           Rectangle locationOnScreen, int animationTime, SpriteBatch spriteBatch)
        {
            this.monoProcess = monoInstance;
            this.spriteSheet = sheet;
            this.animation = (Rectangle[])sectionsOnSheet.Clone();
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.animTime = animationTime;
            this.batch = spriteBatch;
        }

        public void Execute() 
        {
            this.monoProcess.LinkSprite = new NoMoveSprite(spriteSheet, animation, dest, animTime, batch);
        }
    }
}
