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
            



            foreach (Keys k in pressed)
            {
                if (keyMappings.ContainsKey(k))
                {
                    if(k == Keys.O & !npcSwapTriggered)
                    {
                        keyMappings[k].Execute();
                        npcSwapTriggered = true;
                    }
                    if (!npcSwapTriggered & k == Keys.P)
                    {
                        keyMappings[k].Execute();
                        npcSwapTriggered = true;
                    }
                    else if ( (k == Keys.I | k == Keys.U) & !itemSwapTriggered)
                    {
                        keyMappings[k].Execute();
                        itemSwapTriggered = true;
                    }
                    else if (k != Keys.O & k != Keys.I & k != Keys.P & k != Keys.U)
                    {
                        keyMappings[k].Execute();
                    }
                }
            }
        }
    }
}
