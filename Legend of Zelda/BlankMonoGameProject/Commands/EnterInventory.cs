/* Contributors:
 * Grant Gabel
 */

using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class EnterInventory:ICommand
    {
        public Game1 Game;
        public ICommand[] inventoryCommands = new ICommand[13];
        public ICommand[] activeCommands = new ICommand[13];
        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter};
  

        public EnterInventory(Game1 game)
        {
            Game = game;


        }

        public void Execute()
        {
            Game.Paused = true;
            Game.InInventory = true;
            setToInventoryKeyMapping();

        }

        private void setToInventoryKeyMapping()
        {
            // assigning commands for when in inventory screen
            inventoryCommands[0] = new MenuSelectionUp(Game.sel);
            inventoryCommands[1] = new MenuSelectionDown(Game.sel);
            inventoryCommands[2] = new MenuSelectionLeft(Game.sel);
            inventoryCommands[3] = new MenuSelectionRight(Game.sel);
            inventoryCommands[4] = new MenuSelectionChoice(Game.sel);
            inventoryCommands[5] = new Pass();
            inventoryCommands[6] = new Pass();
            inventoryCommands[7] = new Pass();
            inventoryCommands[8] = new Pass();
            inventoryCommands[9] = new Pass();
            inventoryCommands[10] = new Quit(Game);
            inventoryCommands[11] = new Pass();
            inventoryCommands[12] = new Pass();
            Game.keyboardController = new KeyboardController(Game, Game.keyboardKeys, inventoryCommands);
        }
    }
}
