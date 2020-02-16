using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class IncrementItem : ICommand
    {
        private readonly Game1 monoProcess;
        public int itemPosition;
        public IncrementItem(Game1 monoInstance, int position)
        {
            monoProcess = monoInstance;
            itemPosition = position;
            
        }

        public void Execute() 
        {
            if (itemPosition == 12)
            {
                itemPosition = 0;
            }
            else
            {
                itemPosition++;
            }
            monoProcess.Item = monoProcess.ItemList[itemPosition];
        }
    }
}
