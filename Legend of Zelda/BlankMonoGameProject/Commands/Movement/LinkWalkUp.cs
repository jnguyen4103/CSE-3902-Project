using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class LinkWalkUp : ICommand
    {
        private readonly Game1 Game;
        public LinkWalkUp(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.ChangeDirection(States.Direction.Up);
            Vector2 Velocity = new Vector2(0, -Game.Link.BaseSpeed);
            Game.Link.Position += Velocity;
            Game.Link.Sprite.UpdatePosition(Game.Link.Position);
        }
    }
}
