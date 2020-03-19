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

        // Allows for a GameObject to have both Up and Left as a Direction
        //[Flags]
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
