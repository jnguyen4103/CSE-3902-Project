using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    // Enemies have basic behavior
    // Patrolling - Randomly moving around
    // Attacking - Using attacks and/or chasing Link
    // Dead - Got rekt by Link
    // TakeDamage - Got injured
    public abstract class IStateMachine
    {
        protected Game1 Game;
        protected Monster self;
        protected static Random random = new Random();
        protected int WalkCounter = 0;
        protected int Timer = 0;
        protected string defaultSpriteName;

        public virtual void SpawnState()
        {
            Timer++;
            if (Timer == 1)
            {
                self.Sprite.ChangeSpriteAnimation("SpawningCloud");
                self.Sprite.CurrentSpeed.X = 0;
                self.Sprite.CurrentSpeed.Y = 0;
                self.Sprite.FPS = 3;
            }
            else if (Timer >= 60)
            {
                Timer = 0;
                self.Sprite.ChangeSpriteAnimation(self.Name);
                self.Sprite.FPS = 8;
                IdleState();
            }
        }


        public abstract  void IdleState();
        public abstract void MoveState();

        public virtual void AttackState() { }
        public abstract void DamagedState(int directionDamaged);
        public virtual void DeadState()
        {
            Timer++;
            if (Timer == 1)
            {
                WalkCounter = 0;
                self.Sprite.CurrentSpeed = new Vector2(0, 0);
                self.State = Monster.MonsterState.Dead;
                self.Sprite.ChangeSpriteAnimation("Death");
            }
            else if (Timer >= (20 / self.Sprite.FPS * 8))
            {
                self.Sprite.KillSprite();
                Timer = 0;
            }
        }

        protected void Pushback(int damagedDirection)
        {
            switch (damagedDirection)
            {
                case (0):
                    self.Sprite.CurrentSpeed.X = 0;
                    self.Sprite.CurrentSpeed.Y = -self.Sprite.BaseSpeed;
                    break;
                case (1):
                    self.Sprite.CurrentSpeed.X = 0;
                    self.Sprite.CurrentSpeed.Y = self.Sprite.BaseSpeed;
                    break;
                case (2):
                    self.Sprite.CurrentSpeed.X = self.Sprite.BaseSpeed;
                    self.Sprite.CurrentSpeed.Y = 0;
                    break;
                case (3):
                    self.Sprite.CurrentSpeed.X = -self.Sprite.BaseSpeed;
                    self.Sprite.CurrentSpeed.Y = 0;
                    break;
                default:
                    break;
            }
        }

        protected virtual void DirectionString() { }
    }

}