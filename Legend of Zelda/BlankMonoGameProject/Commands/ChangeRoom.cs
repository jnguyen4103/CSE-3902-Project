using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class ChangeRoom : ICommand
    {
        private readonly Game1 Game;
        private List<ScreenTransition> screenTransitions;
        private Rectangle DoorHitbox;
        const float spawnOffset = 32.0f;
        public ChangeRoom(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            screenTransitions = Game.CurrDungeon.ActiveRoom.RoomTransitions;
            MouseState mouseState = Mouse.GetState();
            int hitboxEndX;
            int hitboxEndY;
            int adjustedMouseX;
            int adjustedMouseY;
            ScreenTransition[] transitionArray = screenTransitions.ToArray();

            for (int i = 0; i < transitionArray.Length; i ++)
            {
                String nextRoom = transitionArray[i].NextRoom;
                DoorHitbox = transitionArray[i].doorHitbox;
                hitboxEndX = DoorHitbox.X + DoorHitbox.Width;
                hitboxEndY = DoorHitbox.Y + DoorHitbox.Height;
                adjustedMouseX = (int)((mouseState.X + Game.Camera.Position.X) / Game.ScreenScale);
                adjustedMouseY = (int)((mouseState.Y + Game.Camera.Position.Y) / Game.ScreenScale);

                Vector2 linkSpawn;

                /*Console.WriteLine("Next Room: " + transition.NextRoom + "\n");
                Console.WriteLine("Door hitbox: X = " + DoorHitbox.X + " " + hitboxEndX + " Y = " + DoorHitbox.Y + " " + hitboxEndY + "\n");
                Console.WriteLine("Mouse click: X = " + mouseState.X + " Y = " + mouseState.Y + "\n");
                Console.WriteLine("Adjusted Mouse click: X = " + adjustedMouseX + " Y = " + adjustedMouseY + "\n");*/

                if (adjustedMouseX > DoorHitbox.X && adjustedMouseX < hitboxEndX && adjustedMouseY > DoorHitbox.Y && adjustedMouseY < hitboxEndY)
                {
                    Console.WriteLine("Clicked on door\n");
                    Game.CurrDungeon.TransitionToRoom(nextRoom);
                    linkSpawn.X = Game.CurrDungeon.ActiveRoom.Position.X + spawnOffset;
                    linkSpawn.Y = Game.CurrDungeon.ActiveRoom.Position.Y + spawnOffset;
                    Game.Link = new Link(Game, Game.SpriteLink, linkSpawn);
                }
            }
        }
    }
}
