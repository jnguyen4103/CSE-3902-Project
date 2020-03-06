namespace Sprint03
{
    class KillAllCommand : ICommand
    {
        private readonly Game1 Game;

        public KillAllCommand(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.MonstersList.Clear();
        }
    }
}