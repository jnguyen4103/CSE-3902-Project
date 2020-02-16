using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    public class KeyboardController : IController
    {
        IDictionary<Keys, ICommand> keyMappings;
        private bool npcSwapTriggered = false;

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
            Keys[] pressed = Keyboard.GetState().GetPressedKeys();

            if (pressed.Length == 0) npcSwapTriggered = false;


            foreach (Keys k in pressed)
            {
                if (keyMappings.ContainsKey(k))
                {
                    if(k == Keys.O && !npcSwapTriggered)
                    {
                        keyMappings[Keys.O].Execute();
                        npcSwapTriggered = true;
                    } else if (k != Keys.O)
                    {
                        keyMappings[k].Execute();
                    }
                }
            }
        }
    }
}
