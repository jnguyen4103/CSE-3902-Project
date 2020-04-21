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
	class GameInventoryState : IGameState
	{
		public Game1 Game;

		public bool isTransitioning { get => isTransition; }
		private bool isTransition = false;
		public Vector2 sizeOfInvScreen = new Vector2(256, 240);
		public GameInventoryState(Game1 game)
		{
			Game = game;
		}

		public void TransitionToState()
		{
		}

		public void Draw()
		{
			Game.spriteBatch.Draw(Game.DungeonMain, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
			Game.spriteBatch.Draw(Game.DungeonDoorFrames, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);
			//Game.hud.Draw();
			Game.inv.Draw();
		}


		public void Update()
		{
			Game.Camera.Update();
		}
	}
}
