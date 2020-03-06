using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class LinkWalkRight : ICommand
    {
        private readonly Game1 monoProcess;
        public LinkWalkRight(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            if (monoProcess.Link.GetState() != Link.LinkState.Damaged)
            {
                monoProcess.Link.StateMachine.RightState();
            }
        }
    }
}
