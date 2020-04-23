using Microsoft.Xna.Framework;
using System.Linq;

namespace Sprint03
{
    class LinkBomb : ICommand
    {
        private readonly Game1 Game;
        public LinkBomb(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SecondaryAttack("Bomb");
            Game.Link.CanMove = false;

        }
    }
}