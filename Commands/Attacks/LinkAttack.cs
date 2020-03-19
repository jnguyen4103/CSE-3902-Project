using Microsoft.Xna.Framework;

namespace Sprint03
{
    class LinkAttack : ICommand
    {
        private readonly Game1 Game;
        public LinkAttack(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.Attack();
        }
    }
}