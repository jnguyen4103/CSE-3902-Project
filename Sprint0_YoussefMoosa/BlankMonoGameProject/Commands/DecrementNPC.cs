using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    class DecrementNPC : ICommand
    {
        private readonly Game1 monoProcess;
        public int monsterPosition;
        public DecrementNPC(Game1 monoInstance, int position)
        { 
            monoProcess = monoInstance;
            monsterPosition = position;
            
        }

        public void Execute() 
        {
            if (monsterPosition == 0)
            {
                monsterPosition = 4;
            }
            else
            {
                monsterPosition--;
            }
            monoProcess.Monster = monoProcess.MonsterList[monsterPosition];
            monoProcess.Monster.Sprite.UpdatePosition(monoProcess.spawnPosition);
        }
    }
}
