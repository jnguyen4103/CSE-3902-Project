using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class LinkUseBoomerang : ICommand
    {
        private readonly Game1 monoProcess;
        public LinkUseBoomerang(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            monoProcess.Link.StateMachine.UseItem(0);
        }
    }
}
