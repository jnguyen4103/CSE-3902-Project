using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class KeyboardController : IController
    {
        Game1 Game;
        IDictionary<Keys, ICommand> keyMappings;
        private bool secondaryTriggered = false;
        private bool linkAttackTriggered = false;
        private bool linkDamagedTriggered = false;
        private int attackDelay = 25;
        private int attackTimer = 25;
        private int damageDelay = 90;
        private int damageTimer = 90;
        private int secondaryDelay = 8;
        private int secondaryTimer = 8;


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

            if (keyState.IsKeyUp(Keys.D1) & keyState.IsKeyUp(Keys.D2)) { secondaryTriggered = false; }
            if (keyState.IsKeyUp(Keys.Z)) { linkAttackTriggered = false; }
            if (keyState.IsKeyUp(Keys.E)) { linkDamagedTriggered = false; }

            if (keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.S) && keyState.IsKeyUp(Keys.D) && Game.Link.GetState() == Link.LinkState.Moving)
            {
                Game.Link.StateMachine.IdleState();
            }

            if(keyState.IsKeyDown(Keys.W)&& keyState.IsKeyDown(Keys.A)&&
                keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
            {
                keyMappings[Keys.W].Execute();

            }

            if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A) &&
              keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
            {
                keyMappings[Keys.S].Execute();
            }

                // If Link attacks he won't be able to attack until 60 frames have passed
                // If Link is damaged he won't be able to move and attack until 180 frames have
                // passed


                if (attackTimer < attackDelay)
            {
                attackTimer++;
            }
            if (damageTimer < damageDelay)
            {
                damageTimer++;
            }

            if (secondaryTimer < secondaryDelay)
            {
                secondaryTimer++;
            }

            // Legend of Zelda prioritizes veritcal movement over horizontal so horizontal movement
            // is ignored if the W and S keys are pressed
            // There are flags coded in so a command will only activate once on a key press

            foreach (Keys k in pressed)
            {
                if ((k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D) && (damageTimer >= damageDelay) && (attackTimer >= attackDelay) && (secondaryTimer >= secondaryDelay))
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

                    if((k == Keys.D1 || k == Keys.D2) & !secondaryTriggered & damageTimer >= damageDelay & secondaryTimer >= secondaryDelay)
                    {
                        secondaryTimer = 0;
                        keyMappings[k].Execute();
                        secondaryTriggered = true;
                    }

                    if (!linkDamagedTriggered & k == Keys.E)
                    {
                        damageTimer = 0;
                        keyMappings[k].Execute();
                        linkDamagedTriggered = true;
                    }

                    if (!linkAttackTriggered & k == Keys.Z & attackTimer >= attackDelay & damageTimer >= damageDelay)
                    {
                        attackTimer = 0;
                        keyMappings[k].Execute();
                        linkAttackTriggered = true;
                    }
                }
            }
        }
    }
}
