using Microsoft.Xna.Framework;
using System.Linq;

namespace Sprint03
{
    class LinkBoomerang : ICommand
    {
        private readonly Game1 Game;
        public LinkBoomerang(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SecondaryAttack("Boomerang");
            Game.Link.CanMove = false;
        }
    }
}