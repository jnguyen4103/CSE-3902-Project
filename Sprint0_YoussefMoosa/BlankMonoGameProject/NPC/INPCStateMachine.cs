using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public interface INPCStateMachine
    {
        void IdleState();
        void PatrolState();
        void AttackState();
        void TakeDamageState();
        void DeadState();

        void generateRandomPosition();
    }
 
}
