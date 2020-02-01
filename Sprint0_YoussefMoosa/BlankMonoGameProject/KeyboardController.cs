using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Sprint0_YoussefMoosa
{
    public class KeyboardController : IController
    {
        IDictionary<Keys, ICommand> keyMappings;

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

            foreach(Keys k in pressed)
            {
                if (keyMappings.ContainsKey(k))
                {
                    keyMappings[k].Execute();
                }
            }
        }
    }
}
