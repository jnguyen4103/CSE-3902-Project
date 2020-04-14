using Microsoft.Xna.Framework;

namespace Sprint03
{
    class LinkCandle : ICommand
    {
        private readonly Game1 Game;
        public LinkCandle(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SecondaryAttack("BlueCandle");
            Game.soundEffects[4].Play();
            Game.Link.CanMove = false;
        }
    }
}