using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class KeyboardCheater
    {
        public KeyboardState currState;
        public KeyboardState prevState;
        Game1 game;

        public KeyboardCheater(Game1 game)
        {
            this.game = game;
        }

        public void Update()
        {
            prevState = currState;
            currState = Keyboard.GetState();

            /*
             *  If left control is pressed and NumPad 0
             *  is transitioning to pressed, Restore to full health.
             */
            if (currState.IsKeyDown(Keys.LeftControl) &&
                currState.IsKeyDown(Keys.NumPad0)
                && prevState.IsKeyUp(Keys.NumPad0))
            {
                game.Link.HP = 6;
                game.hud.UpdateCurrentHealth(game.Link.HP);
            }

            /*
             *  If left control pressed and Numpad 1 is
             *  transitioning to pressed, change base speed.
             */
            if (currState.IsKeyDown(Keys.LeftControl)
                && currState.IsKeyDown(Keys.NumPad1)
                && prevState.IsKeyUp(Keys.NumPad1))
            {
                if (game.Link.BaseSpeed <= 1.9f)
                { game.Link.BaseSpeed *= 2.0f; }
                else
                { game.Link.BaseSpeed /= 2.0f;  } 
                
            }

            /*
             *  If left control pressed and Numpad 2 is
             *  transitioning to pressed, change key counter to 50.
             */
            if (currState.IsKeyDown(Keys.LeftControl)
                && currState.IsKeyDown(Keys.NumPad2)
                && prevState.IsKeyUp(Keys.NumPad2))
            {
                game.KeyCounter = 50;
                game.hud.UpdateKeyCounter(50);
            }

            /*
             *  If left control pressed and Numpad 3 is
             *  transitioning to pressed, change bomb counter to 50.
             */
            if (currState.IsKeyDown(Keys.LeftControl)
                && currState.IsKeyDown(Keys.NumPad3)
                && prevState.IsKeyUp(Keys.NumPad3))
            {
                game.BombCounter = 50;
                game.hud.UpdateBombCounter(50);
            }

            /*
             *  You get the idea...
             *  NumPad 4 cheat kills all monsters
             */
            if (currState.IsKeyDown(Keys.LeftControl)
                && currState.IsKeyDown(Keys.NumPad4)
                && prevState.IsKeyUp(Keys.NumPad4))
            {
                foreach (Monster m in game.Dungeon01.Monsters)
                {
                    m.TakeDamage(States.Direction.None, int.MaxValue);
                }
            }
        }
    }
}
