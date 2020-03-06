﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint03
{
    class LinkWalkUp : ICommand
    {
        private readonly Game1 monoProcess;
        public LinkWalkUp(Game1 monoInstance)
        {
            monoProcess = monoInstance;
        }

        public void Execute()
        {
            if (monoProcess.Link.GetState() != Link.LinkState.Damaged)
            {
                monoProcess.Link.StateMachine.UpState();
            }
        }
    }
}