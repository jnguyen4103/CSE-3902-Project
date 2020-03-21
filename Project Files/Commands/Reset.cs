/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class Reset : ICommand
    {
        private readonly Game1 Game;
        public Reset(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.State = States.LinkState.Idle;
            Game.SpriteLink = new LinkSprite(Game, "WalkUp", Game.LinkSpriteSheet, Game.spriteBatch);
            Game.Link = new Link(Game, Game.SpriteLink, Game.LinkSpawn);
            Game.RupeeCounter = 0;
            Game.KeyCounter = 0;
            Game.BombCounter = 0;
            Game.hud = new HUD(Game);
            DungeonLoader.ResetLevel(Game, Game.Dungeon01);
        }
    }
}
