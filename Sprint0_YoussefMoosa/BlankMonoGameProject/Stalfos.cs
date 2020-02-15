using Microsoft.Xna.Framework;
using Sprint0_YoussefMoosa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlankMonoGameProject.NPC;

namespace BlankMonoGameProject
{
    class Stalfos : NPC
    {
        Stalfos(ISprite sprite)
        {
            state = State.Spawning;
            StateMachine = new StalfosSM(this);
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 0;
            contactDamage = 1;
        }

        protected override void NPCAttack()
        {
            // Depends if enemy attacks via contact or projectile
            StateMachine.AttackState();
        }

        protected override void KillNPC()
        {
            state = State.Dead;
            StateMachine.DeadState();
        }

        protected override void Spawn()
        {
            StateMachine.SpawnState();
            state = State.Idle;
        }
    }

    public class StalfosSM : INPCStateMachine
    {
        NPC self;

        public StalfosSM(NPC Stalfos)
        {
            self = Stalfos;
        }

        public void SpawnState()
        {
            // Displays spawn animation
        }

        public void AttackState()
        {

        }

        public void DeadState()
        {
            // Turn off visibility in sprite
        }

        public void IdleState()
        {
            // Do Nothing
        }

        public void TakeDamageState()
        {
            // Takes damage from Link, depending on which weapon

        }

        public void Update()
        {

        }
    }
}
