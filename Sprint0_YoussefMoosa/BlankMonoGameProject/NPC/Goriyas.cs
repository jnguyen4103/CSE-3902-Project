using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sprint02
{
    public class Goriyas : NPC
    {
        public Goriyas(GoriyasSprite sprite, IEffect _attackEffect)
        {
            state = State.Patrolling;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 1;
            contactDamage = 1;
            StateMachine = new GoriyasSM(this, _attackEffect);
        }

        public override void Draw()
        {
            Sprite.DrawSprite();
            this.Update();
        }

    }

    public class GoriyasSM: INPCStateMachine
    {
        Goriyas self;
        Random random = new Random();
        IEffect AttackEffect;
        Vector2 positionPathingTo;
        bool isPathing = false;
        bool isAttacking = false;
        int attackCounter = 0;
        int xPositionalDirection = 0; // -1 for left, 1 for right, 0 neither
        int yPositionalDirection = 0; // -1 for down, 1 for up, 0 neither

        public GoriyasSM(Goriyas goriyas, IEffect _attackEffect)
        {
            self = goriyas;
            AttackEffect = _attackEffect;
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
            attackCounter++;
            if(!isAttacking)
            {
                isAttacking = true;
            } else if (attackCounter == 65)
            {
                // Use Boomerang
                AttackEffect.createEffectSprite(self.Sprite.getLocation(), xPositionalDirection, yPositionalDirection);
            }
            if (attackCounter >= 95)
            {
                attackCounter = 0;
                self.Idle();
            }
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
            isAttacking = false;
            attackCounter++;
            if (attackCounter == 6)
            {
                self.NPCAttack();
            }
            else
            {
                self.Patrol();
            }
        }

        public void TakeDamageState()
        {
            // Takes damage from Link, depending on which weapon
            self.hitpoints--;

        }
        public void generateRandomPosition()
        {
            /* 1 is Down
             * 2 is Up
             * 3 is Right
             * 4 is Left
             */
            int randDirection = random.Next(1, 4);
            int randDistance = random.Next(0, 50);
            switch (randDirection)
            {
                case (1):
                    positionPathingTo = new Vector2(0, -1*randDistance);
                    this.self.Sprite.UpdateSpriteFrames(randDirection);
                    xPositionalDirection = 0;
                    yPositionalDirection = -1;

                    break;
                case (2):
                    positionPathingTo = new Vector2(0, randDistance);
                    this.self.Sprite.UpdateSpriteFrames(randDirection-2);
                    xPositionalDirection = 0;
                    yPositionalDirection = 1;

                    break;
                case (3):
                    positionPathingTo = new Vector2(-1*randDistance, 0);
                    this.self.Sprite.UpdateSpriteFrames(randDirection);
                    xPositionalDirection = 1;
                    yPositionalDirection = 0;

                    break;
                case (4):
                    positionPathingTo = new Vector2(randDistance, 0);
                    this.self.Sprite.UpdateSpriteFrames(randDirection-2);
                    xPositionalDirection = -1;
                    yPositionalDirection = 0;


                    break;
                default:
                    positionPathingTo = self.Sprite.getLocation();
                    isPathing = false;
                    break;
            }
        }
    }
}
