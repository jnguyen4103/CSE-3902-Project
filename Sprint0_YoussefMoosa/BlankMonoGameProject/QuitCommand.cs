﻿using Microsoft.Xna.Framework;

namespace Sprint0_YoussefMoosa
{
    class QuitCommand : ICommand
    {
        private readonly Game monoProcess;

        public QuitCommand(Game monoInstance)
        {
            this.monoProcess = monoInstance;
        }

        public void Execute()
        {
            this.monoProcess.Exit();
        }
    }
}
