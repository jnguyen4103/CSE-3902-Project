/* Contributors:
 * Xueying Liang
 */

using System.Windows.Input;
using Sprint03;

namespace Sprint03
{
   internal class MenuSelectionChoice: ICommand
    {
        private readonly ISelectionMenu Menu;

        public MenuSelectionChoice (ISelectionMenu menu)
        {
            Menu = menu;
        }

        public void Execute() 
        {
            Menu.Choose();
        }
    }
}
