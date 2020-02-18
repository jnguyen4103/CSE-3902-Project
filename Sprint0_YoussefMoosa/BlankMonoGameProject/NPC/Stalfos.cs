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
        // Setting info about NPC and default parameters
        public Stalfos(StalfosSprite sprite)
        {
            this.state = State.Patrolling;
            this.Sprite = sprite;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 0;
            contactDamage = 1;
            StateMachine = new StalfosSM(this);
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
        Random random = new Random();
        Vector2 positionPathingTo;
        // Keeps track of whether or not NPC is pathing to a different location
        bool isPathing = false;

        public StalfosSM(NPC Stalfos)
        {
            self = Stalfos;
        }


        public void PatrolState()
        {
            // If NPC is not pathing then find new location to path too
            if (!isPathing)
            {
                self.Sprite.PathToPosition(positionPathingTo);
                isPathing = true;
            }
            // If NPC is at it's pathing location then idle
            else if (self.Sprite.AtTargetLocation())
            {
                self.Idle();
            }
        }


        public void AttackState()
        {
            // There is no attack state for this NPC
            // If we want more advanced actions then implement later on
        }

        public void DeadState()
        {
            // Turn off visibility in sprite

        }

        public void IdleState()
        {
            // Doesn't attack so just switches back to patrolling
            // Can use this method if we want NPC to idle after each burst of movement
            isPathing = false;
            self.Patrol();

        }

        public void TakeDamageState()
        {
            // Takes damage from Link, depending on which weapon
            self.hitpoints--;

        }
        public void generateRandomPosition()
        {
            // Generates random distance to path to
            // NPC only moves in X or Y direction, never
            // moves at a diagonal
            int randDirection = random.Next(1, 3);
            int randDistance = random.Next(0, 100) - 50;
            switch (randDirection)
            {
                case (1):
                    positionPathingTo = new Vector2(0, randDistance);
                    break;
                case (2):
                    positionPathingTo = new Vector2(randDistance, 0);
                    break;
                default:
                    positionPathingTo = self.Sprite.getLocation();
                    isPathing = false;
                    break;
            }
        }
    }
}
