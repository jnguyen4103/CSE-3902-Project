using Sprint0_YoussefMoosa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameProject
{
    public interface INPCStateMachine
    {
        void SpawnState();
        void IdleState();
        void AttackState();
        void TakeDamageState();
        void DeadState();
        void Update();
    }
 
}
