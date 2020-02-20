using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class DecrementItem : ICommand
    {
        private readonly Game1 monoProcess;
        public DecrementItem(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute() 
        {
            if (monoProcess.currentItemPosition == 0)
            {
                monoProcess.currentItemPosition = 12;
            }
            else
            {
                monoProcess.currentItemPosition--;
            }
            monoProcess.Item = monoProcess.ItemList[monoProcess.currentItemPosition];
        }
    }
}
