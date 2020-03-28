using Microsoft.Xna.Framework;
using System.Linq;

namespace Sprint03
{
    class LinkArrow : ICommand
    {
        private readonly Game1 Game;
        public LinkArrow(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SecondaryAttack("Arrow");
            Game.soundEffects[0].CreateInstance().Play();
            Game.Link.CanMove = false;
        }
    }
}