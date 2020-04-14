/* Contributors:
 * Grant Gabel
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class EnterInventory:ICommand
    {
        public Game1 Game;
        public ICommand[] inventoryCommands = new ICommand[13];
        //private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter };
  

        public EnterInventory(Game1 game)
        {
            Game = game;


        }

        public void Execute()
        {
            Game.Paused = !Game.Paused;
            Game.InInventory = !Game.InInventory;
            if (Game.InInventory)
            {
                setToInventoryKeyMapping();
            }
            

        }

        private void setToInventoryKeyMapping()
        {
            // assigning commands for when in inventory screen
            inventoryCommands[0] = new DoNothing();
            inventoryCommands[1] = new DoNothing();
            inventoryCommands[2] = new DoNothing();
            inventoryCommands[3] = new DoNothing();
            inventoryCommands[4] = new DoNothing();
            inventoryCommands[5] = new DoNothing();
            inventoryCommands[6] = new DoNothing();
            inventoryCommands[7] = new DoNothing();
            inventoryCommands[8] = new DoNothing();
            inventoryCommands[9] = new DoNothing();
            inventoryCommands[10] = new Quit(Game);
            inventoryCommands[11] = new DoNothing();
            inventoryCommands[12] = new ExitInventory(Game);

            Game.keyboardController = new KeyboardController(Game, Game.keyboardKeys, inventoryCommands);
        }


    }
}
