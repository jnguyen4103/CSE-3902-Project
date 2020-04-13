using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class InventoryScreenCmd : ICommand
    {
        Game1 game;

        public InventoryScreenCmd(Game1 game)
        {
            this.game = game;
        }

        public void Execute() 
        {
            game.cameraPrev = game.Camera.Position;

            if (game.updateCtx == game.DungeonUpdate)
            {
                game.updateCtx = game.DungeonToInventoryTransitionUpdate;
                game.drawCtx = game.InventoryDraw;
            }
            else if(game.updateCtx == game.InventoryUpdate)
            {
                game.updateCtx = game.InventoryToDungeonTransitionUpdate;
                game.drawCtx = game.InventoryDraw;
            }
        }
    }
}
