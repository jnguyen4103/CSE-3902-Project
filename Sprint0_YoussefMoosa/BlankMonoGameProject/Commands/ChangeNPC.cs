using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class ChangeNPC : ICommand
    {
        private readonly Game1 monoProcess;
        public int monsterPosition;
        public ChangeNPC(Game1 monoInstance, int position)
        {
            monoProcess = monoInstance;
            monsterPosition = position;
            
        }

        public void Execute() 
        {
            if (monsterPosition > 3)
            {
                monsterPosition = 0;
            }
            else
            {
                monsterPosition++;
            }
            monoProcess.Monster = monoProcess.MonsterList[monsterPosition];
            monoProcess.Monster.Sprite.UpdatePosition(monoProcess.spawnPosition);
        }
    }
}
