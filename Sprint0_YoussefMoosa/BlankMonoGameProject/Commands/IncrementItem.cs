using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class IncrementItem : ICommand
    {
        private readonly Game1 monoProcess;
        public IncrementItem(Game1 monoInstance)
        {
            monoProcess = monoInstance;            
        }

        public void Execute() 
        {
            if (monoProcess.currentItemPosition == 12)
            {
                monoProcess.currentItemPosition = 0;
            }
            else
            {
                monoProcess.currentItemPosition++;
            }
            monoProcess.Item = monoProcess.ItemList[monoProcess.currentItemPosition];
        }
    }
}
