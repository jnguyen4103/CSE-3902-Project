using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class ZolSM : IStateMachine
    {
        private Game1 Game;
        private int Timer = 0;
        private int SpawnDelay = 120;
        private int DeathDelay = 20;
        private int StunDelay = 32;
        private int DamagedDelay = 90;
        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);

        public Monster Self { get; set; }

        public ZolSM(Monster Zol, Game1 game)
        {
            Game = game;
            Self = Zol;
        }

        /* Ensures that Stalfos will run through a 0.5 second
         * spawn period. Once the delay has finished it sets his
         * initial states and sprite animations.
         */
        public void SpawnState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.FPS = 1;
                Self.Sprite.ChangeSpriteAnimation("SpawningCloud");
            }
            else if (Timer >= SpawnDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Zol");
                Reset();
                Self.Sprite.FPS = 8;
                Self.State = States.MonsterState.Idle;
            }
        }

        /*
         * If Stalfos currently has no direction (is not moving)
         * it'll reset his movement and have him pathing randomly again
         */
        public void IdleState()
        {
            ResetMovement(States.Direction.None);
        }

        public void MoveState()
        {
            if (Path == Self.Position && Self.State != States.MonsterState.Damaged)
            {
                Self.State = States.MonsterState.Idle;
                Reset();
            }

            else
            {
                if (DetectLink()) { DeadState(); }
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
            }
        }



        public void AttackState()
        {
            // Stalfos Does not attack
        }

        public void DamagedState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("ZolDamaged");
                SetKnockbackVelocity();
            }
            if (Timer < StunDelay)
            {
                Knockback();
            }

            if (Timer >= DamagedDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Zol");
                Reset();
                Self.State = States.MonsterState.Idle;
            }
        }

        public void DeadState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.Remove();
                IAttack explosion = new Explosion(Game, Self.Position,Self);
                IAttack explosion2 = new Explosion(Game, new Vector2(Self.Position.X-16,Self.Position.Y), Self);
                IAttack explosion3 = new Explosion(Game, new Vector2(Self.Position.X + 16, Self.Position.Y), Self);
                IAttack explosion4 = new Explosion(Game, new Vector2(Self.Position.X - 16, Self.Position.Y+16), Self);
                IAttack explosion5 = new Explosion(Game, new Vector2(Self.Position.X - 16, Self.Position.Y-16), Self);
                IAttack explosion6 = new Explosion(Game, new Vector2(Self.Position.X , Self.Position.Y-16), Self);
                IAttack explosion7 = new Explosion(Game, new Vector2(Self.Position.X , Self.Position.Y+16), Self);
                IAttack explosion8 = new Explosion(Game, new Vector2(Self.Position.X + 16, Self.Position.Y+16), Self);
                IAttack explosion9 = new Explosion(Game, new Vector2(Self.Position.X + 16, Self.Position.Y-16), Self);

                explosion.Attack();
                explosion2.Attack();
                explosion3.Attack();
                explosion4.Attack();
                explosion5.Attack();
                explosion6.Attack();
                explosion7.Attack();
                explosion8.Attack();
                explosion9.Attack();

                Reset();
            }
            else if (Timer > DeathDelay)
            {
                Game.IFactory.DropItem(Self.Position);
                Timer = 0;
                Self.Sprite.Remove();
            }
        }



        public void ResetMovement(States.Direction direction)
        {
            int randomDirection = Game1.random.Next(0, 4);
            if (randomDirection == (int)direction)
            {
                randomDirection = Game1.random.Next(0, 4);
            }
            else
            {
                Self.Direction = (States.Direction)randomDirection;
                if (Self.State != States.MonsterState.Damaged)
                {
                    Self.State = States.MonsterState.Moving;
                }
                SetPath();
            }
        }

        private void Reset()
        {
            Self.CanDamage = true;
            Velocity.X = 0;
            Velocity.Y = 0;
            Path.X = 0;
            Path.Y = 0;
        }

        private void SetPath()
        {
            // Length in 16x16 blocks
            int lengthOfPath = 16 * Game1.random.Next(1, 6);
            float distance = 0;

            // Restricts movement so Sprites are in a 16x16 tile
            // x - (x % 16) will always be a multiple of 16
            switch (Self.Direction)
            {
                case (States.Direction.Up):
                    distance = (Self.Position.Y - lengthOfPath) - ((Self.Position.Y - lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = -Self.BaseSpeed;
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = Self.BaseSpeed;

                    break;

                case (States.Direction.Left):
                    distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = -Self.BaseSpeed;
                    Velocity.Y = 0;
                    break;

                case (States.Direction.Right):
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    break;
            }
        }

        private void SetKnockbackVelocity()
        {
            Velocity.X = 0;
            Velocity.Y = 0;
            switch (Self.Direction)
            {
                case (States.Direction.Up):
                    Velocity.Y = 2 * Self.BaseSpeed;
                    break;

                case (States.Direction.Down):
                    Velocity.Y = -2 * Self.BaseSpeed;

                    break;

                case (States.Direction.Left):
                    Velocity.X = 2 * Self.BaseSpeed;
                    break;

                case (States.Direction.Right):
                    Velocity.X = -2 * Self.BaseSpeed;
                    break;
            }
        }

        private void Knockback()
        {
            Self.Position += Velocity;
            Self.Sprite.UpdatePosition(Self.Position);
        }
        private bool DetectLink()
        {
            return Math.Abs(Self.Position.X - Game.Link.Position.X) < 24 && Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 24;
        }
    }
}
