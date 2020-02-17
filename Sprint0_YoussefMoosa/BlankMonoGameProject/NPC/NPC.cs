using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public abstract class NPC
    {
        public NPCSprite Sprite;
        public INPCStateMachine StateMachine;
        public enum State
        {
            Idle,           // NPC remains idle due to clock item or other effects
            Patrolling,     // NPC randomly moves around (Only behavior most NPCs)
            Attacking,      // NPC uses attack if available (most NPCS just damage on contact)
            TakeDamage,     // State for when NPC is damaged by Link
            Dead            // State for when NPC is slain (RIP)
        }

        public State state;
        public int hitpoints;
        public int maxHP;
        public int attackDamage;
        public int contactDamage;

        public abstract void Draw();


        public void Idle()
        {
            state = State.Idle;
        }
        public void Patrol()
        {
            StateMachine.generateRandomPosition();
            state = State.Patrolling;
        }

        public void NPCAttack()
        {
            state = State.Attacking;
        }

        public void KillNPC()
        {
            state = State.Dead;
        }

        protected void Update()
        {
            switch (this.state)
            {
                case (State.Idle):
                    StateMachine.IdleState();
                    break;
                case (State.Patrolling):
                    StateMachine.PatrolState();
                    break;
                case (State.Dead):
                    StateMachine.DeadState();
                    break;
                case (State.Attacking):
                    StateMachine.AttackState();
                    break;
                case (State.TakeDamage):
                    StateMachine.TakeDamageState();
                    break;
                default:
                    state = State.Idle;
                    break;
            }

        }

    }

}
