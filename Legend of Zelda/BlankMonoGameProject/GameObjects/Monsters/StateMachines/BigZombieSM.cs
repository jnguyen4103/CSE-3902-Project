/* Contributors
* Nico Negrete
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class BigZombieSM : IStateMachine
    {
        private Game1 Game;
        private string direction;
        private int Timer = 0;

        private int SpawnDelay = 120;
        private int DeathDelay = 10;
        private int StunDelay = 32;
        private int DamagedDelay = 90;
        private int AttackDelay = 40;
        private int ExplosionDelay = 2;
        private int ResetCounter = 4;
        private bool canAttack = true;
        private readonly int ResetThreshold = 4;

        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);

        public Monster Self { get; set; }

        public BigZombieSM(Monster BigZombie, Game1 game)
        {
            Game = game;
            Self = BigZombie;
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
                Self.Sprite.ChangeSpriteAnimation("BigZombieDown");
                Reset();
                Self.Sprite.FPS = 4;
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
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
                if(DetectLink() && canAttack)
                {
             
                    Self.State = States.MonsterState.Attacking;
                }
            }
        }
        private bool DetectLink()
        {
            return Math.Abs(Self.Position.X - Game.Link.Position.X) < 40 && Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 40;
        }

        private void createExplosion()
        {
            IAttack explosion = new Explosion(Game, new Vector2(Self.Position.X - 16, Self.Position.Y + Self.Sprite.Size.Y), Self);
            IAttack explosion2 = new Explosion(Game, new Vector2(Self.Position.X, Self.Position.Y + Self.Sprite.Size.Y), Self);
            IAttack explosion3 = new Explosion(Game, new Vector2(Self.Position.X + 16, Self.Position.Y+Self.Sprite.Size.Y), Self);
            IAttack explosion4 = new Explosion(Game, new Vector2(Self.Position.X+32, Self.Position.Y + Self.Sprite.Size.Y), Self);
            explosion.Attack();
            explosion2.Attack();
            explosion3.Attack();
            explosion4.Attack();
        }

        public void AttackState()
        {
            Timer++;
            
            if (Timer  ==1)
            {
                
                Self.Sprite.ChangeSpriteAnimation("BigZombieAttack");
            }
            if(Timer == ExplosionDelay)
            {
                createExplosion();
            }
            if (Timer >= AttackDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("BigZombie" + direction);
                Reset();
                Self.State = States.MonsterState.Idle;
                canAttack = false;
            }


        }

        public void DamagedState()
        {
            Timer++;
            canAttack = false;
            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("BigZombie" + direction + "Damaged");
                Game.soundEffects[7].Play();
                SetKnockbackVelocity();
            }
            if (Timer < StunDelay)
            {
                Knockback();

            }

            if (Timer >= DamagedDelay)
            {
                Timer = 0;
                Self.Sprite.ChangeSpriteAnimation("BigZombie" + direction);
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
                Timer = 0;
                Game.IFactory.DropItem(Self.Position);
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
                    if (ResetCounter == ResetThreshold)
                    {
                        Pursue();
                    }
                    else
                    {
                        ResetCounter++;
                        SetPath();

                    }
                }
            }
        }

        private void Reset()
        {
            Self.CanDamage = true;
            canAttack = true;
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
                    Self.Sprite.ChangeSpriteAnimation("BigZombieUp");
                    direction = "Up";
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16);
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = Self.BaseSpeed;
                    Self.Sprite.ChangeSpriteAnimation("BigZombieDown");
                    direction = "Down";
                    break;

                case (States.Direction.Left):
                    distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = -Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("BigZombieLeft");
                    direction = "Left";
                    break;

                case (States.Direction.Right):
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("BigZombieRight");
                    direction = "Right";
                    break;
            }
        }

        private void Pursue()
        {
            Path = Vector2.Zero;

            if ((Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 70) && Math.Abs(Self.Position.X - Game.Link.Position.X) < 70)
            {
                Reset();
                Self.State = States.MonsterState.Attacking;

            }
            else
            {
                if (Math.Abs(Self.Position.Y - Game.Link.Position.Y) > 16)
                {
                    // If Link is above Darknut
                    if (Self.Position.Y > Game.Link.Position.Y)
                    {
                        Self.Direction = States.Direction.Up;
                        Path.X = Self.Position.X;
                        Path.Y = Game.Link.Position.Y;
                        Velocity.X = 0;
                        Velocity.Y = -Self.BaseSpeed;
                        Self.Sprite.ChangeSpriteAnimation("BigZombieUp");
                        direction = "Up";
                    }
                    // If Link is below Darknut
                    else
                    {
                        Self.Direction = States.Direction.Down;
                        Path.X = Self.Position.X;
                        Path.Y = Game.Link.Position.Y;
                        Velocity.X = 0;
                        Velocity.Y = Self.BaseSpeed;
                        Self.Sprite.ChangeSpriteAnimation("BigZombieDown");
                        direction = "Down";
                    }
                }
                else if (Math.Abs(Self.Position.X - Game.Link.Position.X) > 16)
                {
                    // If Link is to the left of Darknut
                    if (Self.Position.X > Game.Link.Position.X)
                    {
                        Self.Direction = States.Direction.Left;
                        Path.X = Game.Link.Position.X;
                        Path.Y = Self.Position.Y;
                        Velocity.X = -Self.BaseSpeed;
                        Velocity.Y = 0;
                        Self.Sprite.ChangeSpriteAnimation("BigZombieLeft");
                        direction = "Left";
                    }
                    // If Link is to the right of Darknut
                    else
                    {
                        Self.Direction = States.Direction.Right;
                        Path.X = Game.Link.Position.X;
                        Path.Y = Self.Position.Y;
                        Velocity.X = Self.BaseSpeed;
                        Velocity.Y = 0;
                        Self.Sprite.ChangeSpriteAnimation("BigZombieRight");
                        direction = "Right";
                    }
                }
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

    }
}
