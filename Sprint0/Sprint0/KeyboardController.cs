using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    class KeyboardController : IController
    {
        KeyboardState keyboardState;
        Game1 currentGame;

        public KeyboardController(Game1 game1)
        {
            currentGame = game1;
        }
        public void controllerAction()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D0) || keyboardState.IsKeyDown(Keys.NumPad0))
            {
                currentGame.Exit();
            }
            else if (keyboardState.IsKeyDown(Keys.D1) || keyboardState.IsKeyDown(Keys.NumPad1))
            {
                // One frame of animation and a fixed position. Initial state of the program.
                currentGame.SetSprite(new StillSprite(currentGame.GetTexture()));
            }
            else if (keyboardState.IsKeyDown(Keys.D2) || keyboardState.IsKeyDown(Keys.NumPad2))
            {
                // Animated sprite with fixed position
                currentGame.SetSprite(new AnimatedSprite(currentGame.GetTexture(), 9));
            }
            else if (keyboardState.IsKeyDown(Keys.D3) || keyboardState.IsKeyDown(Keys.NumPad3))
            {
                // One frame of animation but moves up and down
                currentGame.SetSprite(new MovingSpriteUD(currentGame.GetTexture(), 1));
            }
            else if (keyboardState.IsKeyDown(Keys.D4) || keyboardState.IsKeyDown(Keys.NumPad4))
            {
                // Animated sprite moving left and right
                currentGame.SetSprite(new MovingSpriteLR(currentGame.GetTexture(), 14));
            }
        }
    }
}
