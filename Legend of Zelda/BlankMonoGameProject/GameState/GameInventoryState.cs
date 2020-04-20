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

		private Vector2 targetCamPosition;

		public GameInventoryState(Game1 game)
		{
			Game = game;
		}

		public void TransitionToState()
		{
			
			isTransition = true;

			//Game.GraphicsDevice.Clear(Color.Black);
			//Game.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: Game.Camera.Transform);

			//Draw();

			//Game.spriteBatch.End();

			//targetCamPosition = new Vector2( Game.Camera.Position.X / Game.ScreenScale  , (Game.Camera.Position.Y -  sizeOfInvScreen.Y   ) / Game.ScreenScale );
			//Game.Camera.Transition(targetCamPosition);

			
			//	Game.Camera.Update();
			

			isTransition = false;
		}

		public void Draw()
		{
			Game.spriteBatch.Draw(Game.DungeonMain, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
			//Game.CurrDungeon.Draw();
			//Game.Link.Draw();
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
