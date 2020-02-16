using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class IncrementNPC : ICommand
    {
        private readonly Game1 monoProcess;
        public IncrementNPC(Game1 monoInstance)
        {
            monoProcess = monoInstance;        
        }

        public void Execute() 
        {
            if (monoProcess.currentMonsterPosition == 4)
            {
                monoProcess.currentMonsterPosition = 0;
            }
            else
            {
                monoProcess.currentMonsterPosition++;
            }
            monoProcess.MonsterList[monoProcess.currentMonsterPosition].Sprite.UpdatePosition(monoProcess.spawnPosition);
            monoProcess.Monster = monoProcess.MonsterList[monoProcess.currentMonsterPosition];
        }
    }
}
