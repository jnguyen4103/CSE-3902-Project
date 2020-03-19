using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class LynelSM : IStateMachine
    {
        private Game1 Game;
        private string direction;
        private int Timer = 0;
        private int AttackTimer = 120;
        private int AttackDelay = 120;
        private bool Charging = false;
        private int SpawnDelay = 120;
        private int DeathDelay = 20;
        private int StunDelay = 32;
        private int DamagedDelay = 90;
        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);

        public Monster Self { get; set; }

        public LynelSM(Monster Lynel, Game1 game)
        {
            Game = game;
            Self = Lynel;
        }

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
                Self.Sprite.ChangeSpriteAnimation("LynelDown");
                Reset();
                Self.Sprite.FPS = 8;
                Self.State = States.MonsterState.Idle;
            }
        }

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
                if (AttackDelay == AttackTimer && !Charging) { DetectLink(); ThrowSword(); }
                else { AttackTimer++; }
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
            }
        }



        public void AttackState()
        {
            if (Math.Abs(Self.Position.X - Game.Link.Position.X) < Math.Abs(Self.Position.Y - Game.Link.Position.Y))
            {
                if (Game.Link.Position.Y < Self.Position.Y)
                {
                    Velocity.Y = -2f;
                    Path.X = Self.Position.X;
                    Path.Y = Self.Position.Y - 64;
                    Self.Sprite.ChangeSpriteAnimation("LynelUp");
                    Self.Direction = States.Direction.Up;
                }
                else if (Game.Link.Position.Y > Self.Position.Y)
                {
                    Velocity.Y = 2f;
                    Path.X = Self.Position.X;
                    Path.Y = Self.Position.Y + 80;
                    Self.Sprite.ChangeSpriteAnimation("LynelDown");
                    Self.Direction = States.Direction.Down;
                }
            }
            else
            {
                if (Game.Link.Position.X < Self.Position.X)
                {
                    Velocity.X = -2f;
                    Path.X = Self.Position.X - 64;
                    Path.Y = Self.Position.Y;
                    Self.Sprite.ChangeSpriteAnimation("LynelLeft");
                    Self.Direction = States.Direction.Left;
                }
                else if (Game.Link.Position.X > Self.Position.X)
                {
                    Velocity.X = 2f;
                    Path.X = Self.Position.X + 80;
                    Path.Y = Self.Position.Y;
                    Self.Sprite.ChangeSpriteAnimation("LynelRight");
                    Self.Direction = States.Direction.Right;
                }
            }
            Self.State = States.MonsterState.Moving;
            AttackTimer = 0;
        }

        public void DamagedState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("Lynel" + direction + "Damaged");
                SetKnockbackVelocity();
            }
            if (Timer < StunDelay)
            {
                Knockback();
            }

            if (Timer >= DamagedDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Lynel" + direction);
                Reset();
                Self.State = States.MonsterState.Idle;
            }
        }

        public void DeadState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("Death");
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
            Charging = false;
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
                    SetPath();
                }
            }
        }

        private void Reset()
        {
            Self.CanDamage = true;
            Charging = false;
            Velocity.X = 0;
            Velocity.Y = 0;
            Path.X = 0;
            Path.Y = 0;
        }

        private void SetPath()
        {
            int lengthOfPath = 16 * Game1.random.Next(1, 6);
            float distance = 0;
            switch (Self.Direction)
            {
                case (States.Direction.Up):
                    distance = (Self.Position.Y - lengthOfPath) - ((Self.Position.Y - lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = -Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("LynelUp");
                    direction = "Up";
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("LynelDown");
                    direction = "Down";
                    break;

                case (States.Direction.Left):
                    distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = -Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("LynelLeft");
                    direction = "Left";
                    break;

                case (States.Direction.Right):
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("LynelRight");
                    direction = "Right";
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

        private void ThrowSword()
        {
            IAttack swordBeam = new SwordBeam(Game, Self, Self.Direction);
            if (Math.Abs(Self.Position.X - Game.Link.Position.X) < 8)
            {
                if(Game.Link.Position.Y < Self.Position.Y && Self.Direction == States.Direction.Up)
                {
                    AttackTimer = 0;
                    swordBeam.Attack();
                }
                else if (Game.Link.Position.Y > Self.Position.Y && Self.Direction == States.Direction.Down)
                {
                    AttackTimer = 0;
                    swordBeam.Attack();
                }
            }
            if (Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 8)
            {
                if (Game.Link.Position.X < Self.Position.X && Self.Direction == States.Direction.Left)
                {
                    AttackTimer = 0;
                    swordBeam.Attack();
                }
                else if (Game.Link.Position.X > Self.Position.X && Self.Direction == States.Direction.Right)
                {
                    AttackTimer = 0;
                    swordBeam.Attack();
                }
            }
        }

        private void DetectLink()
        {
            if(Math.Abs(Self.Position.X - Game.Link.Position.X) < 32 && Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 32)
            {
                Reset();
                Charging = true;
                Self.State = States.MonsterState.Attacking;
            }
        }
    }
}
