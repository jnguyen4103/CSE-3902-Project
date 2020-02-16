using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sprint02
{
    public class Aquamentus : NPC
    {
        public Aquamentus(AquamentusSprite sprite)
        {
            state = State.Patrolling;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 1;
            contactDamage = 1;
            StateMachine = new AquamentusSM(this);
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
            Sprite.DrawSprite();
            this.Update();
        }

    }

    public class AquamentusSM : INPCStateMachine
    {
        NPC self;
        Random random = new Random();
        // Keeps track of which location the NPC is going to
        Vector2 goToLocation;
        // Keeps track of whether the NPC is at that location or not
        bool isAtTargetLocation = true;

        //
        int attackCounter = 0;

        public AquamentusSM(NPC Aquamentus)
        {
            self = Aquamentus;
        }


        public void PatrolState()
        {
            if (isAtTargetLocation)
            {
                int randDirection = random.Next(0, 2);
                int randDistance = random.Next(0, 100) - 50;
                switch (randDirection)
                {
                    case (1):
                        goToLocation = new Vector2(0, randDistance);
                        break;
                    case (2):
                        goToLocation = new Vector2(randDistance, 0);
                        break;
                    default:
                        break;
                }
                self.Sprite.MoveToPosition(goToLocation);
                isAtTargetLocation = self.Sprite.MoveToPosition(goToLocation);
            }
        }


        public void AttackState()
        {
            attackCounter++;
            if(attackCounter > 200)
            {
                self.state = NPC.State.Patrolling;
                self.Sprite.UpdateSpriteFrames(2);
            } else if (attackCounter == 200)
            {
                // Activate FIREBALL
            }
            

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
