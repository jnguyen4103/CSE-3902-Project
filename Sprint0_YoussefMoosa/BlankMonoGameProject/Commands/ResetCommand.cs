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
            //monoProcess.Link.SpriteLink.position = monoProcess.LinkSpawn;
            //monoProcess.Link.StateMachine.DownState();
            monoProcess.Monster = monoProcess.MonsterList[0];
            //monoProcess.Monster.Sprite.UpdatePosition(monoProcess.spawnPosition);
            monoProcess.Item = monoProcess.ItemList[0];
            monoProcess.EffectsList.Clear();
        }
    }

}