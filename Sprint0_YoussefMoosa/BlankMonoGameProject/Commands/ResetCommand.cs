using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class ResetCommand : ICommand
    {
        private readonly Game1 monoProcess;

        public ResetCommand(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            monoProcess.Link.SpriteLink.Position = monoProcess.LinkSpawn;
            monoProcess.Link.StateMachine.DownState();
            monoProcess.Link = new Link(monoProcess.SpriteLink, monoProcess);
            monoProcess.Link.HP = monoProcess.Link.MaxHP;
            monoProcess.EffectsList.Clear();
            foreach (Monster monster in monoProcess.MonsterList)
            {
                monster.Sprite.Position = monoProcess.spawnPosition;
                monster.StateMachine.IdleState();
            }
        }
    }

}