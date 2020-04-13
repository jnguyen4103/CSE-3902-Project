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
    interface ISelectionMenu 
    {
        void SelectionUp();
        void SelectionDown();
        void SelectionLeft();
        void SelectionRight();
        void Choose();
    }
}
