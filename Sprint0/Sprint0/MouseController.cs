using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    class MouseController : IController
    {
        MouseState mouseState;
        Game1 currentGame;
        Vector2 screenSize = new Vector2(800, 480);

        public MouseController(Game1 game1)
        {
            currentGame = game1;
        }

        public void controllerAction()
        {
            mouseState = Mouse.GetState();
            if (mouseState.RightButton.ToString() == "Pressed")
            {
                currentGame.Exit();
            }
            else if ((mouseState.X >= 0 && mouseState.X <= screenSize.X / 2) && (mouseState.Y >= 0 && mouseState.Y <= screenSize.Y / 2)
                && mouseState.LeftButton.ToString() == "Pressed")
            {
                // One frame of animation and a fixed position. Initial state of the program.
                currentGame.SetSprite(new StillSprite(currentGame.GetTexture()));
            }
            else if ((mouseState.X > screenSize.X / 2 && mouseState.X <= screenSize.X) && (mouseState.Y >= 0 && mouseState.Y <= screenSize.Y / 2)
                && mouseState.LeftButton.ToString() == "Pressed")
            {
                // Animated sprite with fixed position
                currentGame.SetSprite(new AnimatedSprite(currentGame.GetTexture(), 9));
            }
            else if ((mouseState.X >= 0 && mouseState.X <= screenSize.X / 2) && (mouseState.Y >= screenSize.Y / 2 && mouseState.Y <= screenSize.Y)
                && mouseState.LeftButton.ToString() == "Pressed")
            {
                currentGame.SetSprite(new MovingSpriteUD(currentGame.GetTexture(), 1));
            }
            else if ((mouseState.X > screenSize.X / 2 && mouseState.X <= screenSize.X) && (mouseState.Y >= screenSize.Y / 2 && mouseState.Y <= screenSize.Y)
                && mouseState.LeftButton.ToString() == "Pressed")
            {
                // Animated sprite moving left and right
                currentGame.SetSprite(new MovingSpriteLR(currentGame.GetTexture(), 14));
            }
        }
    }
}
