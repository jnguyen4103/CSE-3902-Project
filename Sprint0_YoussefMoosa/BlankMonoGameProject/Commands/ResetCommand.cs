namespace Sprint03
{
    class ResetCommand : ICommand
    {
        private readonly Game1 Game;

        public ResetCommand(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.SpriteLink.Position = Game.LinkSpawn;
            Game.Link.StateMachine.DownState();
            Game.Link = new Link(Game.SpriteLink, Game);
            Game.Link.HP = Game.Link.MaxHP;
            Game.MonsterList.Clear();
            Game.ItemsList.Clear();
            Game.EffectsList.Clear();
            Game.RFactory.ResetRooms();
            Game.RFactory.LoadRoom("Room0");
        }
    }
}