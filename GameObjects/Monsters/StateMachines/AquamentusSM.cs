/* Contributors
* Nico Negrete
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class AquamentusSM : IStateMachine
    {
        private Game1 Game;
        private int Timer = 0;
        private int AttackTimer = 180;
        private int AttackDelay = 180;
        private readonly int SpawnDelay = 30;
        private readonly int DamagedDelay = 15;
        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);
        private IAttack top;
        private IAttack middle;
        private IAttack bottom;
        public Monster Self { get; set; }

        public AquamentusSM(Monster Aquamentus, Game1 game)
        {
            Game = game;
            Self = Aquamentus;
        }

        public void SpawnState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.FPS = 6;
                Self.Sprite.ChangeSpriteAnimation("SpawningCloud");
            }
            else if (Timer >= SpawnDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Aquamentus");
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
                if (AttackDelay == AttackTimer) { Self.State = States.MonsterState.Attacking; }
                else { AttackTimer++; }
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
            }
        }


        public  void AttackState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("AquamentusAttack");
            }
            else if (Timer >= 4 * Self.Sprite.FPS)
            {
                Self.Sprite.ChangeSpriteAnimation("Aquamentus");
                ShootFireball();
                AttackTimer = 0;
                Timer = 0;
                Self.State = States.MonsterState.Idle;
                IdleState();
            }
        }

        public void DamagedState()
        {
            Timer++;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("AquamentusDamaged");
            }
            else if (Timer >= DamagedDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("Aquamentus");
                Reset();
                Self.State = States.MonsterState.Idle;
            }
        }

        public void DeadState()
        {
            Self.Sprite.Remove();
        }



        public void ResetMovement(States.Direction direction)
        {
            int randomDirection = Game1.random.Next(0, 2);
            if (randomDirection == (int)direction)
            {
                randomDirection = Game1.random.Next(0, 2);
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
            int lengthOfPath = 16 * Game1.random.Next(1, 3);
            float distance = 0;

            switch (Self.Direction)
            {
                case (States.Direction.Up):
                    distance = (Self.Position.Y - lengthOfPath) - ((Self.Position.Y - lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = -Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("Aquamentus");
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("Aquamentus");
                    break;

            }
        }

        private void ShootFireball()
        {
            Vector2 fireballVelocity = Vector2.Zero;
            fireballVelocity.X = (Game.Link.Position.X - Self.Position.X) / 60;
            fireballVelocity.Y = (Game.Link.Position.Y - Self.Position.Y) / 60;

            middle = new Fireball(Game, Self, fireballVelocity);
            top = new Fireball(Game, Self, new Vector2(fireballVelocity.X, fireballVelocity.Y - 0.75f));
            bottom = new Fireball(Game, Self, new Vector2(fireballVelocity.X, fireballVelocity.Y + 0.75f));
            middle.Attack();
            top.Attack();
            bottom.Attack();

        }
    }
}
