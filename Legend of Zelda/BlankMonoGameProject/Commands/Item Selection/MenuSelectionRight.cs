/* Contributors:
 * Xueying Liang
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    internal class MenuSelectionRight : ICommand
    {
        private readonly ISelectionMenu Menu;

        public MenuSelectionRight(ISelectionMenu menu)
        {
            Menu = menu;
        }

        public void Execute()
        {
            Menu.SelectionRight();
        }
    }
}
