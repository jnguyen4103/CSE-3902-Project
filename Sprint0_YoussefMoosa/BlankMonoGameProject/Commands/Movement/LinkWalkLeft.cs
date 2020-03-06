using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class LinkWalkLeft : ICommand
    {
        private readonly Game1 monoProcess;
        public LinkWalkLeft(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            if (monoProcess.Link.GetState() != Link.LinkState.Damaged)
            {
                monoProcess.Link.StateMachine.LeftState();
            }
        }
    }
}
