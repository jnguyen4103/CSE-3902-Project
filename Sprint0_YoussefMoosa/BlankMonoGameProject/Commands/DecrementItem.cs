using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class DecrementItem : ICommand
    {
        private readonly Game1 monoProcess;
        public int itemPosition;
        public DecrementItem(Game1 monoInstance, int position)
        {
            monoProcess = monoInstance;
            itemPosition = position;
            
        }

        public void Execute() 
        {
            if (itemPosition == 0)
            {
                itemPosition = 12;
            }
            else
            {
                itemPosition--;
            }
            monoProcess.Item = monoProcess.ItemList[itemPosition];
        }
    }
}
