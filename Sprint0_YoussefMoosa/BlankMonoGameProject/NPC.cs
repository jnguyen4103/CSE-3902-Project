using Sprint0_YoussefMoosa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameProject
{
    public abstract class NPC
    {
        public ISprite sprite;
        public INPCStateMachine StateMachine;
        public enum State
        {
            Spawning,       // NPC just spawning it, performs animation then switches to Attacking
            Idle,           // NPC is idle due to clock item or other effects
            Attacking,      // Default behavior, NPC moves randomly and/or fires random projectiles
            TakeDamage,     // State for when NPC is damaged by Link
            Dead            // State for when NPC is slain (RIP)
        }

        public State state;
        public int hitpoints;
        public int maxHP;
        public int attackDamage;
        public int contactDamage;

        protected abstract void Spawn();
        protected abstract void NPCAttack();
        protected abstract void KillNPC();

    }
}
