using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class Pause : ICommand
    {
        private readonly Game1 Game;
        public Pause(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Paused = !Game.Paused;
        }
    }
}
