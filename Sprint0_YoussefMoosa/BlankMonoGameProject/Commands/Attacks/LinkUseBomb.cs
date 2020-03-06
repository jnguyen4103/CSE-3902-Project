using System.Linq;

namespace Sprint03
{
    class LinkUseBomb : ICommand
    {
        private readonly Game1 Game;
        public LinkUseBomb(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            if (!Game.EffectsList.Any(item => item is BombEffect))
            {
                Game.Link.StateMachine.UseBomb();
            }
        }
    }
}
