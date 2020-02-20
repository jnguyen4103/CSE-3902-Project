using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    // Enemies have basic behavior
    // Patrolling - Randomly moving around
    // Attacking - Using attacks and/or chasing Link
    // Dead - Got rekt by Link
    // TakeDamage - Got injured
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
