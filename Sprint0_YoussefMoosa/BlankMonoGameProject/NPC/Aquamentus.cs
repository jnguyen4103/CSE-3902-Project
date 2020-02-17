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
        public Aquamentus(AquamentusSprite sprite, IEffect _attackEffect)
        {
            state = State.Patrolling;
            Sprite = sprite;
            hitpoints = 1;
            maxHP = 1;
            attackDamage = 1;
            contactDamage = 1;
            StateMachine = new AquamentusSM(this, _attackEffect);
        }

        public override void Draw()
        {
            Sprite.DrawSprite();
            this.Update();
        }

    }

    public class AquamentusSM : INPCStateMachine
    {
        Aquamentus self;
        Random random = new Random();
        IEffect AttackEffect;
        Vector2 positionPathingTo;
        bool isPathing = false;
        bool isAttacking = false;
        int attackCounter = 0;

        public AquamentusSM(Aquamentus aquamentus, IEffect _attackEffect)
        {
            self = aquamentus;
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
                self.Sprite.UpdateSpriteFrames(1);
                isAttacking = true;
            } else if (attackCounter >= 65)
            {
                // Use Fireball
                AttackEffect.createEffectSprite(self.Sprite.getLocation(), 0, 0);
                attackCounter = 0;
                self.Sprite.UpdateSpriteFrames(2);
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
            if (attackCounter == 3)
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
