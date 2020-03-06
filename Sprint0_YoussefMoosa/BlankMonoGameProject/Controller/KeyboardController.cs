using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class KeyboardController : IController
    {
        Game1 Game;
        IDictionary<Keys, ICommand> keyMappings;
        private bool AttackTriggered = false;

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


            if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.D) && Game.Link.GetState() == Link.LinkState.Moving)
            {
                Game.Link.StateMachine.IdleState();
            }
            if (keyState.IsKeyUp(Keys.Z)) { AttackTriggered = false; }


            foreach (Keys k in pressed)
            {

                if ((k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D))
                {
                    // Just having this if check to see if Link is idle creates input delay of around 0.3s
                    if (Game.Link.GetState() != Link.LinkState.Damaged && Game.Link.GetState() != Link.LinkState.Attacking
                        && Game.Link.GetState() != Link.LinkState.Dead && Game.Link.GetState() != Link.LinkState.UsingSecondary)
                    {
                        if (k == Keys.W || k == Keys.S)
                        {
                            keyMappings[k].Execute();
                        }
                        else if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S))
                        {
                            keyMappings[k].Execute();
                        }

                    }
                }

                if(k == Keys.Z && !AttackTriggered)
                {
                    AttackTriggered = true;
                    keyMappings[k].Execute();
                }

                if (keyMappings.ContainsKey(k))
                {
                    // Without this if statement the game wil allow animation cancelling
                    if (k == Keys.Q || k == Keys.R || k == Keys.E || k == Keys.D1 || k == Keys.D2 || k == Keys.X)
                    {
                        keyMappings[k].Execute();

                    }

                }
            }
        }
    }
}


