using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class InInventory:ICommand
    {
        private readonly Game1 Game;
        public ICommand[] inventoryCommands = new ICommand[13];
        public ICommand[] activeCommands = new ICommand[13];
        //private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter };
        // Adding all of the commands into the keyboard controller
        //keyboardCommands[0] = new LinkWalkUp(this);
        //keyboardCommands[1] = new LinkWalkDown(this);
        //keyboardCommands[2] = new LinkWalkLeft(this);
        //keyboardCommands[3] = new LinkWalkRight(this);
        //keyboardCommands[4] = new LinkAttack(this);
        //keyboardCommands[5] = new LinkBomb(this);
        //keyboardCommands[6] = new LinkArrow(this);
        //keyboardCommands[7] = new LinkCandle(this);
        //keyboardCommands[8] = new LinkBoomerang(this);
        //keyboardCommands[9] = new Reset(this);
        //keyboardCommands[10] = new Quit(this);
        //keyboardCommands[11] = new Pause(this);
        //keyboardCommands[12] = new InInventory(this);
        //keyboardController = new KeyboardController(this, keyboardKeys, keyboardCommands);

        public InInventory(Game1 game)
        {
            Game = game;

            // assigning commands for when actively playing game
            activeCommands[0] = new LinkWalkUp(Game);
            activeCommands[1] = new LinkWalkDown(Game);
            activeCommands[2] = new LinkWalkLeft(Game);
            activeCommands[3] = new LinkWalkRight(Game);
            activeCommands[4] = new LinkAttack(Game);
            activeCommands[5] = new LinkBomb(Game);
            activeCommands[6] = new LinkArrow(Game);
            activeCommands[7] = new LinkCandle(Game);
            activeCommands[8] = new LinkBoomerang(Game);
            activeCommands[9] = new Reset(Game);
            activeCommands[10] = new Quit(Game);
            activeCommands[11] = new Pause(Game);
            activeCommands[12] = new InInventory(Game);

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
            inventoryCommands[12] = new InInventory(Game);
        }

        public void Execute()
        {
            if (Game.InInventory)
            {
                setToDefaultKeyMapping();
            }

            Game.InInventory = !Game.InInventory;
        }

        private void setToDefaultKeyMapping()
        {
            Game.keyboardController = new KeyboardController(Game, Game.keyboardKeys, activeCommands);
        }
    }
}
