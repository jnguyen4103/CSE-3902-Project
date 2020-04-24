/* Contributors:
 * Grant
 */
using Sprint03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlankMonoGameProject.GameState
{
	class GamePausedState : IGameState
	{
		Game1 Game;
		bool IGameState.isTransitioning { get => isTransition; }
		private bool isTransition = false;

		public GamePausedState(Game1 game)
		{
			Game = game;
		}

		public void TransitionToState()
		{
			Game.GameEnumState = States.GameState.GamePausedState;
		}

		public void Draw()
		{
			Game.spriteBatch.Draw(Game.DungeonMain, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
			Game.CurrDungeon.Draw();
			Game.Link.Draw();
			Game.spriteBatch.Draw(Game.DungeonDoorFrames, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);
			Game.spriteBatch.Draw(Game.InventoryScreen, Game.Camera.Position, null, Color.White, 0, Game.Camera.Position, 1, SpriteEffects.None, 0.90f);
			Game.hud.Draw();
		}

		public void Update()
		{
		}
	}
}
