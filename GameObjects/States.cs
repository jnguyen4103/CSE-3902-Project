/* Contributors
* Stephen Hogg
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class States
    {
        public enum MonsterState
        {
            Idle,
            Moving,
            Attacking,
            Spawning,
            Damaged,
            Dead

        }
        
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        };

        public enum LinkState
        {
            Idle,
            Moving,
            Attacking,
            SecondaryAttack,
            Damaged,
            Dead
        }

    }
}
