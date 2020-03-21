using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class LinkWalkDown : ICommand
    {
        private readonly Game1 Game;
        public LinkWalkDown(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Vector2 Velocity = new Vector2(0, Game.Link.BaseSpeed);
            Game.Link.Position += Velocity;
            Game.Link.Sprite.UpdatePosition(Game.Link.Position);
            Game.Link.ChangeDirection(States.Direction.Down);
        }
    }
}
