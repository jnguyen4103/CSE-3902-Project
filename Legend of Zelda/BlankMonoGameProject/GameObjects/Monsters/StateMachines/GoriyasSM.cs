/* Contributors
* Nico Negrete
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class GoriyasSM : IStateMachine
    {
        private Game1 Game;
        private string direction;
        private int Timer = 0;
        private int AttackCounter = 0;
        private readonly int AttackThreshold = 2;
        private readonly int AttackDelay = 90;
        private readonly int SpawnDelay = 120;
        private readonly int DeathDelay = 20;
        private readonly int StunDelay = 32;
        private readonly int DamagedDelay = 90;
        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);

        public Monster Self { get; set; }

        public GoriyasSM(Monster Goriyas, Game1 game)
        {
            Game = game;
            Self = Goriyas;
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
                Self.Sprite.ChangeSpriteAnimation("GoriyasDown");
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
                if (AttackCounter >= AttackThreshold) { Timer = 0; Self.State = States.MonsterState.Attacking; }
            }

            else
            {
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
            }
        }



        public void AttackState()
        {
            Timer++;
            if(Timer == 1)
            {
                IAttack boomerang = new Boomerang(Game, Self, Self.Direction);
                boomerang.Attack();
                Game.soundEffects[0].Play();
                AttackCounter = 0;
            }
            else if (Timer >= AttackDelay)
            {
                Reset();
                Timer = 0;
                Self.State = States.MonsterState.Idle;
            }
        }

        public void DamagedState()
        {
            Timer++;
            if (Timer == 1)
            {

                Game.soundEffects[7].Play();
                Self.Sprite.ChangeSpriteAnimation("Goriyas" + direction + "Damaged");
                SetKnockbackVelocity();
            }
            if (Timer < StunDelay)
            {
                Knockback();
            }

            if (Timer >= DamagedDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Goriyas" + direction);
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
                Game.soundEffects[6].Play();
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
                    SetPath();
                }
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
                    Self.Sprite.ChangeSpriteAnimation("GoriyasUp");
                    direction = "Up";
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("GoriyasDown");
                    direction = "Down";
                    break;

                case (States.Direction.Left):
                    distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = -Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("GoriyasLeft");
                    direction = "Left";
                    break;

                case (States.Direction.Right):
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("GoriyasRight");
                    direction = "Right";
                    break;
            }
            AttackCounter++;
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
    }
}
