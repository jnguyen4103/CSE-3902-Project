using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class MouseController : IController
    {
        private Game1 Game;
        private bool SwappingRooms;
        public MouseController(Game1 game)
        {
            Game = game;

        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Released)
            {
                SwappingRooms = false;
            }

            if(!SwappingRooms)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    int x = (int)(mouseState.X / Game.ScreenScale);
                    int y = (int)(mouseState.Y / Game.ScreenScale);


                    // Right Door 
                    if (x >= 224 && x <= 255
                        && y >= 136 && y <= 167)
                    {
                        SwappingRooms = true;
                        Game.CurrentRoom.Doors["Right"].EnterDoor();
                    }

                    // Left Door
                    else if (x >= 0 && x <= 31
                        && y >= 136 && y <= 167)
                    {
                        SwappingRooms = true;
                        Game.CurrentRoom.Doors["Left"].EnterDoor();

                    }

                    // Lower Door
                    else if (x >= 112 && x <= 143
                        && y >= 208 && y <= 239)
                    {
                        SwappingRooms = true;
                        Game.CurrentRoom.Doors["Down"].EnterDoor();
                    }

                    // Upper Door
                    else if (x >= 112 && x <= 143
                        && y >= 64 && y <= 95)
                    {
                        SwappingRooms = true;
                        Game.CurrentRoom.Doors["Up"].EnterDoor();
                    }
                }
            }


       }
    }
}
