/* Contributors
* Stephen Hogg
* Youssef Moosa
* Grant Gabel
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
        private bool PauseTriggered = false;
        private int Timer = 60;
        private int PauseTimer = 10;
        private int SecondaryAttackDelay = 60;
        private int InventoryMenuSwitchDelay = 60;
        private int PauseDelay = 10;

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
            if (keyState.IsKeyUp(Keys.P) ) { PauseTriggered = false; }
            if (Timer < SecondaryAttackDelay) { Timer++; }
            if (Timer < InventoryMenuSwitchDelay) { Timer++; }
            if (PauseTimer < PauseDelay) { PauseTimer++; }

            // keep as one if statement with else if statements so don't have to worry about a variable being set and un set in one call
            if (Game.GameEnumState.Equals(States.GameState.GamePlayingState) && !Game.Link.State.Equals(States.LinkState.Dead))
            {
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
                        if (k == Keys.Z && !AttackTriggered)
                        {
                            AttackTriggered = true;
                            keyMappings[k].Execute();
                        }

                        if (k == Keys.X && Game.hasGun)
                        {
                            keyMappings[k].Execute();
                        }

                        if (keyMappings.ContainsKey(k))
                        {
                            // Without this if statement the game will allow animation cancelling
                            if (k == Keys.Q || k == Keys.R || k == Keys.E)
                            {
                                keyMappings[k].Execute();

                            }
                            else if ((k == Keys.D1 || k == Keys.D2 || k == Keys.D3 || k == Keys.D4) && Timer == SecondaryAttackDelay)
                            {
                                Timer = 0;
                                keyMappings[k].Execute();
                            }
                            else if (k == Keys.D1 && Game.BombCounter > 0)
                            {
                                keyMappings[k].Execute();
                            }
                            else if (k == Keys.Enter && Timer == InventoryMenuSwitchDelay)
                            {
                                Timer = 0;
                                Game.CurrentGameState = Game.InventoryState;
                                Game.CurrentGameState.TransitionToState();
                            }
                            else if (k == Keys.P && PauseTimer == PauseDelay)
                            {
                                PauseTimer = 0;
                                Game.CurrentGameState = Game.PauseState;
                                Game.CurrentGameState.TransitionToState();
                            }

                        }
                    
                }
            }
            else if (Game.GameEnumState.Equals(States.GameState.GameInventoryState) && !Game.CurrentGameState.isTransitioning && !Game.Link.State.Equals(States.LinkState.Dead))
            {
                foreach (Keys k in pressed)
                {
                    if (keyMappings.ContainsKey(k) && Timer == InventoryMenuSwitchDelay)
                    {
                        
                        if ((k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D))
                        {
                            if (k == Keys.W || k == Keys.S)
                            {
                                //keyMappings[k].Execute();
                            }
                            else if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S))
                            {
                                //keyMappings[k].Execute();
                            }
                        }
                        else if (k == Keys.R || k == Keys.E || k == Keys.X )
                        {
                           // keyMappings[k].Execute();
                        }
                        else if (k == Keys.Q)
                        {
                            keyMappings[k].Execute();
                        }
                        else if (k == Keys.Enter && Timer == InventoryMenuSwitchDelay)
                        {
                            Timer = 0;
                            Game.CurrentGameState = Game.PlayingState;
                            Game.GameEnumState = States.GameState.GamePlayingState;
                            Game.CurrentGameState.TransitionToState();
                        }
                        else
                        {
                            keyMappings[k].Execute();
                        }
                    }
                }
            }
            else if (Game.GameEnumState.Equals(States.GameState.GamePausedState) && PauseTimer == PauseDelay && !Game.Link.State.Equals(States.LinkState.Dead))
            {
                foreach(Keys k in pressed)
                {
                    if (k == Keys.P)
                    {
                        PauseTimer = 0;
                        Game.CurrentGameState = Game.PlayingState;
                        Game.GameEnumState = States.GameState.GamePlayingState;
                        Game.CurrentGameState.TransitionToState();
                    }else if (k == Keys.Q)
                    {
                        keyMappings[k].Execute();
                    }
                }
            }
            // this allows game to be quit from any state
            else
            {
                foreach(Keys k in pressed)
                {
                    if (k == Keys.Q)
                    {
                        keyMappings[k].Execute();
                    }
                }
            }
        }
    }
}


