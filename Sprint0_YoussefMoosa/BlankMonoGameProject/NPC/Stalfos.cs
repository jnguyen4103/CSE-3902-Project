using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sprint02
{
    public class Stalfos : NPC
    {
        public Stalfos(StalfosSprite sprite)
        {
            state = State.Patrolling;
            this.Sprite = sprite;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 0;
            contactDamage = 1;
            StateMachine = new StalfosSM(this);
        }

        protected override void Idle()
        {
            state = State.Idle;
        }
        protected override void Patrol()
        {
            state = State.Patrolling;
        }

        protected override void NPCAttack()
        {
            // Depends if enemy attacks via contact or projectile
            state = State.Attacking;
        }

        protected override void KillNPC()
        {
            state = State.Dead;
        }
        protected override void Update()
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
                    state = State.Patrolling;
                    break;
            }
            
        }

        public override void Draw()
        {
            this.Update();
            Sprite.DrawSprite();
        }

    }

    public class StalfosSM : INPCStateMachine
    {
        NPC self;
        int randomCounter = 0;

        public StalfosSM(NPC Stalfos)
        {
            self = Stalfos;
        }


        public void PatrolState()
        {
            randomCounter++;
            if(randomCounter == 20)
            {
                Random random = new Random();
                int randomDirection = random.Next(1, 3);
                int randomDistance = random.Next(-5, 5);
                switch (randomDirection)
                {
                    case (1):
                        self.Sprite.MoveToPosition( new Vector2(0, randomDistance));
                        break;
                    case (2):
                        self.Sprite.MoveToPosition(new Vector2(randomDistance, 0));
                        break;
                    default:
                        break;
                }
                randomCounter = 0;
            }
 

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
            // If clock is triggered then maintain idle state
            // Else switch to patrolling state
            self.state = NPC.State.Patrolling;

        }

        public void TakeDamageState()
        {
            // Takes damage from Link, depending on which weapon
            self.hitpoints--;

        }

    }
}
