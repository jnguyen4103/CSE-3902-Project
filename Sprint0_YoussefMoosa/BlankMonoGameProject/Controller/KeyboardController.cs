using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class KeyboardController : IController
    {
        IDictionary<Keys, ICommand> keyMappings;
        private bool npcSwapTriggered = false;
        private bool itemSwapTriggered = false;
        private bool secondaryTriggered = false;
        private bool linkAttackTriggered = false;
        private bool linkDamagedTriggered = false;
        private int attackTimer = 30;
        private int damageTimer = 180;


        public KeyboardController(Keys[] keys, ICommand[] commands) 
        {
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

            if (keyState.IsKeyUp(Keys.O) & keyState.IsKeyUp(Keys.P)) { npcSwapTriggered = false; }
            if (keyState.IsKeyUp(Keys.I) & keyState.IsKeyUp(Keys.U)) { itemSwapTriggered = false; }
            if (keyState.IsKeyUp(Keys.D1) & keyState.IsKeyUp(Keys.D2)) { secondaryTriggered = false; }
            if (keyState.IsKeyUp(Keys.Z)) { linkAttackTriggered = false; }
            if (keyState.IsKeyUp(Keys.E)) { linkDamagedTriggered = false; }
            if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.D))
            {
                keyMappings[Keys.H].Execute();
            }
            
            // If Link attacks he won't be able to attack until 60 frames have passed
            // If Link is damaged he won't be able to move and attack until 180 frames have
            // passed


            if (attackTimer < 30)
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
                if ((k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D) && (damageTimer >= 180) && (attackTimer >= 30))
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

                if (keyMappings.ContainsKey(k))
                {
                    if (k == Keys.Q || k == Keys.R)
                    {
                        keyMappings[k].Execute();
                    }

                    if((k == Keys.D1 || k == Keys.D2) & !secondaryTriggered & damageTimer >= 180)
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

                    if (!linkAttackTriggered & k == Keys.Z & attackTimer == 30 & damageTimer >= 180)
                    {
                        attackTimer = 0;
                        keyMappings[k].Execute();
                        linkAttackTriggered = true;
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
