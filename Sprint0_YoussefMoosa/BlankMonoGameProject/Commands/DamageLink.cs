using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class DamageLink : ICommand
    {
        private readonly Game1 monoProcess;
        public DamageLink(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            monoProcess.Link.TakeDamage(5, 2);
        }
    }
}
