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
    class ExitInventory:ICommand
    {
        public Game1 Game;
        public ICommand[] inventoryCommands = new ICommand[13];
        public ICommand[] activeCommands = new ICommand[13];
        //private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter };

        public ExitInventory(Game1 game)
        {
            this.Game = game;

            // assigning commands for when actively playing game
            //activeCommands[0] = new LinkWalkUp(this.Game);
            //activeCommands[1] = new LinkWalkDown(this.Game);
            //activeCommands[2] = new LinkWalkLeft(this.Game);
            //activeCommands[3] = new LinkWalkRight(this.Game);
            //activeCommands[4] = new LinkAttack(this.Game);
            //activeCommands[5] = new LinkBomb(this.Game);
            //activeCommands[6] = new LinkArrow(this.Game);
            //activeCommands[7] = new LinkCandle(this.Game);
            //activeCommands[8] = new LinkBoomerang(this.Game);
            //activeCommands[9] = new Reset(this.Game);
            //activeCommands[10] = new Quit(this.Game);
            //activeCommands[11] = new Pause(this.Game);
            //activeCommands[12] = new EnterInventory(this.Game);

        }

        public void Execute()
        {
            Game.Paused = false;
            Game.InInventory = false;
            //setToDefaultKeyMapping();
        }

        private void setToDefaultKeyMapping()
        {
            Game.keyboardController = new KeyboardController(Game, Game.keyboardKeys, activeCommands);
        }

    }
}
