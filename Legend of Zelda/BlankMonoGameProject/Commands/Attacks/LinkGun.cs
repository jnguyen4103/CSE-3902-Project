using Microsoft.Xna.Framework;
using System.Linq;

namespace Sprint03
{
    class LinkGun : ICommand
    {
        private readonly Game1 Game;
        public LinkGun(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SecondaryAttack("Bullet");
            Game.soundEffects[19].CreateInstance().Play(); 
            Game.Link.CanMove = false;
        }
    }
}