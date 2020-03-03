using System.Linq;

namespace Sprint03
{
    class LinkUseBoomerang : ICommand
    {
        private readonly Game1 Game;
        public LinkUseBoomerang(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            if (!Game.EffectsList.Any(item => item is BoomerangEffect))
            {
                Game.Link.StateMachine.UseBoomerang();
            }
        }
    }
}
