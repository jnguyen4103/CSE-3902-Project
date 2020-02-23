using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    class LinkUseBeamSword : ICommand
    {
        private readonly Game1 monoProcess;
        public LinkUseBeamSword(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            monoProcess.Link.StateMachine.UseBeamSword();
        }
    }
}
