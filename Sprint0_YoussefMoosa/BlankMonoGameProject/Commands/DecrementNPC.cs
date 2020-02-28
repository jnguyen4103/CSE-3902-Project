using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class DecrementNPC : ICommand
    {
        private readonly Game1 monoProcess;
        public DecrementNPC(Game1 monoInstance)
        { 
            monoProcess = monoInstance;
        }

        public void Execute() 
        {
            if (monoProcess.currentMonsterPosition == 0)
            {
                monoProcess.currentMonsterPosition = 5;
            }
            else
            {
                monoProcess.currentMonsterPosition--;
            }
            //monoProcess.Monster = monoProcess.MonsterList[monoProcess.currentMonsterPosition];
            //monoProcess.Monster.Sprite.UpdatePosition(monoProcess.spawnPosition);
        }
    }
}
