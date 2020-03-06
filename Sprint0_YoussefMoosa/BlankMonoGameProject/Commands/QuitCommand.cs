using Microsoft.Xna.Framework;

namespace Sprint03
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
