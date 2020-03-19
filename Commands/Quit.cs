using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class Quit : ICommand
    {
        private readonly Game1 Game;
        public Quit(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Exit();
        }
    }
}
