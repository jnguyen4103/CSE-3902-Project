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
        public ISprite Sprite;
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

        protected abstract void Idle();
        protected abstract void Patrol();
        protected abstract void NPCAttack();
        protected abstract void KillNPC();
        protected abstract void Update();
        public abstract void Draw();

    }
}
