using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            Game.Link.StateMachine.AttackState();
        }
    }
}
