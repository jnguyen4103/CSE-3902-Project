using Microsoft.Xna.Framework;
using Sprint03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameProject.Commands.Camera
{
	class CameraUp : ICommand
	{
		Game1 Game;
		public CameraUp(Game1 game)
		{
			Game = game;
		}
		public void Execute()
		{
			Vector2 newPosition = new Vector2(528, 848);
			Game.Camera.Transition(newPosition);
		}
	}
}
