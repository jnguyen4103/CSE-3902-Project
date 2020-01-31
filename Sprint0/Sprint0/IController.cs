using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public interface IController
    {
        /// <summary>
        /// This method will specify what action the game should 
        /// take based on user input from a controller.
        /// </summary>
        void controllerAction();
    }
}
