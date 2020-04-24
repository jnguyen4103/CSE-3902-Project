using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    public class MouseController : IController
    {
        private Game1 Game;
        private ICommand[] Commands;
        private bool leftClick = false;

        public MouseController(Game1 game, ICommand[] commands)
        {
            Game = game;
            Commands = commands;
        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Released) { leftClick = false; }

            if (mouseState.LeftButton == ButtonState.Pressed && !leftClick)
            {
                leftClick = true;
                Commands[0].Execute();
            }
        }
    }
}
