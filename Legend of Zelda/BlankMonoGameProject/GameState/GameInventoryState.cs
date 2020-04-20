﻿/* Contributors:
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
		Game1 Game;

		public GameInventoryState(Game1 game)
		{
			Game = game;
		}

		public void TransitionToState()
		{
			// TODO: add camera movement/transition
			Game.GameEnumState = States.GameState.GameInventoryState;
		}

		public void Draw()
		{
			Game.spriteBatch.Draw(Game.DungeonMain, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
			Game.CurrDungeon.Draw();
			Game.Link.Draw();
			Game.spriteBatch.Draw(Game.DungeonDoorFrames, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);
			Game.hud.Draw();
		}


		public void Update()
		{
			Game.Camera.Update();
		}
	}
}