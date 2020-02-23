using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    public class KeyboardController : IController
    {
        IDictionary<Keys, ICommand> keyMappings;
        private bool npcSwapTriggered = false;
        private bool itemSwapTriggered = false;
        private bool secondaryTriggered = false;
        private bool linkAttackTriggered = false;
        private bool linkDamagedTriggered = false;
        private int attackTimer = 60;
        private int damageTimer = 180;
        Game1 game;

        public KeyboardController(Keys[] keys, ICommand[] commands, Game1 Game) 
        {
            keyMappings = new Dictionary<Keys, ICommand>();

            for (int i = 0; i < keys.Length; i++)
            {
                keyMappings.Add(keys[i], commands[i]);
            }
            game = Game;
        }

        public void Update() 
        {
            KeyboardState keyState = Keyboard.GetState();
            Keys[] pressed = keyState.GetPressedKeys();

            if (keyState.IsKeyUp(Keys.O) & keyState.IsKeyUp(Keys.P)) { npcSwapTriggered = false; }
            if (keyState.IsKeyUp(Keys.I) & keyState.IsKeyUp(Keys.U)) { itemSwapTriggered = false; }
            if (keyState.IsKeyUp(Keys.D1)) { secondaryTriggered = false; }
            if (keyState.IsKeyUp(Keys.Z)) { linkAttackTriggered = false; }
            if (keyState.IsKeyUp(Keys.E)) { linkDamagedTriggered = false; }
            
            // If Link attacks he won't be able to attack until 60 frames have passed
            // If Link is damaged he won't be able to move and attack until 180 frames have
            // passed


            if (attackTimer < 60)
            {
                attackTimer++;
            }
            if (damageTimer < 180)
            {
                damageTimer++;
            }

            // Legend of Zelda prioritizes veritcal movement over horizontal so horizontal movement
            // is ignored if the W and S keys are pressed
            // There are flags coded in so a command will only activate once on a key press

            foreach (Keys k in pressed)
            {
                if (keyMappings.ContainsKey(k))
                {
                    if (k == Keys.Q || k == Keys.R)
                    {
                        keyMappings[k].Execute();
                    }
                    if((k == Keys.W || k == Keys.S) && damageTimer >= 180)
                    {
                        keyMappings[k].Execute();
                    }
                    if ((k == Keys.A || k == Keys.D) && pressed.Length == 1 && damageTimer >= 180)
                    {
                        keyMappings[k].Execute();
                    }

                    if(!secondaryTriggered & k == Keys.D1 & damageTimer >= 180)
                    {
                        keyMappings[k].Execute();
                        secondaryTriggered = true;
                    }
                    if (!linkDamagedTriggered & k == Keys.E)
                    {
                        damageTimer = 0;
                        keyMappings[k].Execute();
                        linkDamagedTriggered = true;
                    }

                    if (!linkAttackTriggered & k == Keys.Z & attackTimer == 60 & damageTimer >= 180)
                    {
                        attackTimer = 0;
                        keyMappings[k].Execute();
                        linkAttackTriggered = true;
                    }


                    if(!linkAttackTriggered & k == Keys.F )
                    {
                       attackTimer = 0;
                        keyMappings[k].Execute();
                        linkAttackTriggered = true;

                    }

                    if(k == Keys.G)
                    {
                        keyMappings[k].Execute();
                    }
                    if (k == Keys.O & !npcSwapTriggered)
                    {
                        keyMappings[k].Execute();
                        npcSwapTriggered = true;
                    }
                    if (!npcSwapTriggered & k == Keys.P)
                    {
                        keyMappings[k].Execute();
                        npcSwapTriggered = true;
                    }
                    else if ((k == Keys.I | k == Keys.U) & !itemSwapTriggered)
                    {
                        keyMappings[k].Execute();
                        itemSwapTriggered = true;
                    }
                }
            }
        }
    }
}
