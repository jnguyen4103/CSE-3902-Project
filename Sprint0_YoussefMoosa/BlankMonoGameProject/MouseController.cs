using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Sprint0_YoussefMoosa
{
    class MouseController : IController
    {
        private readonly Rectangle[] sectionsOnScreen;
        private readonly ICommand[] lmbCommands;
        private readonly ICommand[] rmbCommands;

        public MouseController(Rectangle[] sections, ICommand[] lmb, ICommand[] rmb) 
        {
            this.sectionsOnScreen = (Rectangle[])sections.Clone();
            this.lmbCommands = (ICommand[])lmb.Clone();
            this.rmbCommands = (ICommand[])rmb.Clone();
        }

        
        public void Update() 
        {
            MouseState currState = Mouse.GetState();
            Vector2 mousePos = new Vector2(currState.X, currState.Y);

            if (currState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lmbCommands.Length; i++)
                {
                    if (sectionsOnScreen[i].Contains(mousePos))
                    {
                        lmbCommands[i].Execute();
                    }
                }
            }
            else if (currState.RightButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lmbCommands.Length; i++)
                {
                    if (sectionsOnScreen[i].Contains(mousePos))
                    {
                        rmbCommands[i].Execute();
                    }
                }
            }
        }
    }
}
