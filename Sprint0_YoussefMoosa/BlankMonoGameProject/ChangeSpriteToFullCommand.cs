using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0_YoussefMoosa
{
    class ChangeSpriteToFullCommand : ICommand
    {
        private Game1 monoProcess;
        private readonly Texture2D spriteSheet;
        private Rectangle[] animation;
        private Rectangle dest;
        private Vector2 screen;
        private Vector2 velocity;
        int animTime;
        private readonly SpriteBatch batch;

        public ChangeSpriteToFullCommand(Game1 monoInstance, Texture2D sheet, Rectangle[] sectionsOnSheet,
           Rectangle locationOnScreen, Vector2 vel, Vector2 screenDim, SpriteBatch spriteBatch,
           int animationTime)
        {
            this.monoProcess = monoInstance;
            this.spriteSheet = sheet;
            this.animation = (Rectangle[])sectionsOnSheet.Clone();
            this.batch = spriteBatch;
            this.dest = locationOnScreen;
            this.velocity = vel;
            this.screen = screenDim;
            this.batch = spriteBatch;
            this.animTime = animationTime;
        }

        public void Execute() 
        {
            monoProcess.LinkSprite = new FullSprite(spriteSheet, animation, dest, velocity, screen, batch, animTime);
        }
    }
}
