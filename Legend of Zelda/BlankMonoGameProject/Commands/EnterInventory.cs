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
        public ICommand[] activeCommands = new ICommand[13];
        //private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter };
  

        public EnterInventory(Game1 game)
        {
            Game = game;


        }

        public void Execute()
        {
            Game.Paused = true;
            Game.InInventory = true;
            //setToInventoryKeyMapping();

        }

        private void setToInventoryKeyMapping()
        {
            // assigning commands for when in inventory screen
            //inventoryCommands[0] = new MoveCursorUp(Game);
            //inventoryCommands[1] = new MoveCursorDown(Game);
            //inventoryCommands[2] = new MoveCursorLeft(Game);
            //inventoryCommands[3] = new MoveCursorRight(Game);
            //inventoryCommands[4] = new SelectCurrentItem(Game);
            inventoryCommands[5] = null;
            inventoryCommands[6] = null;
            inventoryCommands[7] = null;
            inventoryCommands[8] = null;
            inventoryCommands[9] = null;
            inventoryCommands[10] = new Quit(Game);
            inventoryCommands[11] = null;
            inventoryCommands[12] = new ExitInventory(Game);
            Game.keyboardController = new KeyboardController(Game, Game.keyboardKeys, inventoryCommands);
        }
    }
}
