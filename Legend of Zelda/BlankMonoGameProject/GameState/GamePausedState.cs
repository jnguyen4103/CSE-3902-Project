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
		}

		public void Update()
		{
		}
	}
}
