/* Contributors
* Stephen Hogg
* Youssef Moosa
*/
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class KeyboardController : IController
    {
        Game1 Game;
        IDictionary<Keys, ICommand> keyMappings;
        private bool AttackTriggered = false;
        private int Timer = 60;
        private int SecondaryAttackDelay = 60;
        private bool escapePreviouslyPressed = false;

       public KeyboardController(Game1 game, Keys[] keys, ICommand[] commands)
        {
            Game = game;
            keyMappings = new Dictionary<Keys, ICommand>();

            for (int i = 0; i < keys.Length; i++)
            {
                keyMappings.Add(keys[i], commands[i]);
            }
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();
            Keys[] pressed = keyState.GetPressedKeys();
            if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.D) && Game.Link.State != States.LinkState.Damaged)
            {
                Game.Link.Stop();
            }

            if (keyState.IsKeyUp(Keys.Z)) { AttackTriggered = false; }
            if (Timer < SecondaryAttackDelay) { Timer++; }


            foreach (Keys k in pressed)
            {

                if ((k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D) && Game.Link.CanMove && Game.Link.State != States.LinkState.Damaged && Game.Link.State != States.LinkState.Dead)
                {

                    if (k == Keys.W || k == Keys.S)
                    {
                        keyMappings[k].Execute();
                    }
                    else if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S))
                    {
                        keyMappings[k].Execute();
                    }
                    Game.Link.State = States.LinkState.Moving;
                }


                if(k == Keys.Z && !AttackTriggered)
                {
                    AttackTriggered = true;
                    keyMappings[k].Execute();
                }

                if (keyMappings.ContainsKey(k))
                {
                    // Without this if statement the game wil allow animation cancelling
                    if (k == Keys.Q || k == Keys.R || k == Keys.E || k == Keys.X)
                    {
                        keyMappings[k].Execute();

                    }
                    if ((k == Keys.D1 || k == Keys.D2 || k == Keys.D3 || k == Keys.D4) && Timer == SecondaryAttackDelay)
                    {
                        Timer = 0;
                        keyMappings[k].Execute();
                    }

                }

                if (k == Keys.Escape && !escapePreviouslyPressed)
                {
                    keyMappings[k].Execute();
                }
            }
            escapePreviouslyPressed = Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}


