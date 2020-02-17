using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sprint02
{
    public class Geese : NPC
    {
        public Geese(GeeseSprite sprite)
        {
            state = State.Patrolling;
            this.Sprite = sprite;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 0;
            contactDamage = 1;
            StateMachine = new GeeseSM(this);
        }

        public override void Draw()
        {
            this.Update();
            Sprite.DrawSprite();
        }

    }

    public class GeeseSM : INPCStateMachine
    {
        NPC self;
        Random random = new Random();
        bool isPathing = false;
        Vector2 positionPathingTo;

        public GeeseSM(NPC Geese)
        {
            self = Geese;
        }


        public void PatrolState()
        {
            if (!isPathing)
            {
                self.Sprite.PathToPosition(positionPathingTo);
                isPathing = true;
            }
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