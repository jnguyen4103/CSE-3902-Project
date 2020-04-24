/* Contributors
* Nico Negrete
*/
using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class RopeSM : IStateMachine
    {
        private Game1 Game;
        private string direction;
        private int Timer = 0;

        private int SpawnDelay = 120;
        private int DeathDelay = 20;
        private int StunDelay = 32;
        private int DamagedDelay = 90;

        private int ResetCounter = 4;
        private readonly int ResetThreshold = 4;

        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);

        public Monster Self { get; set; }

        public RopeSM(Monster Rope, Game1 game)
        {
            Game = game;
            Self = Rope;
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
                Self.Sprite.ChangeSpriteAnimation("RopeRight");
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
            Console.WriteLine("Path: " + Path);
            Console.WriteLine();
            Console.WriteLine("Position: " + Self.Position);
            Console.WriteLine();
            if (Path == Self.Position && Self.State != States.MonsterState.Damaged)
            {
                Self.State = States.MonsterState.Idle;
                Reset();
            }

            else
            {
                Console.WriteLine("HELPPPP");
                Self.Position += Velocity;
                Self.Sprite.UpdatePosition(Self.Position);
            }
        }

        public void AttackState()
        {
        
            
                if (Game.Link.Position.X < Self.Position.X)
                {
                    Velocity.X = -2f;
                    Path.X = Self.Position.X - 48;
                    Path.Y = Self.Position.Y;
                    Self.Sprite.ChangeSpriteAnimation("RopeLeft");
                    direction = "Left";
                    Self.Direction = States.Direction.Left;
                Console.WriteLine("WE STUCK LEFT INHERE");

            }
            else if (Game.Link.Position.X > Self.Position.X)
                {
                    Velocity.X = 2f;
                    Path.X = Self.Position.X + 64;
                    Path.Y = Self.Position.Y;
                    Self.Sprite.ChangeSpriteAnimation("RopeRight");
                    direction = "Right";
                Console.WriteLine("WE STUCK RIGHT IN HERE");

                Self.Direction = States.Direction.Right;
                }

            
            ResetCounter = 0;
            Self.State = States.MonsterState.Moving;
        }

        public void DamagedState()
        {
            Timer++;

            if (Timer == 1)
            {
                Self.Sprite.ChangeSpriteAnimation("Rope" + direction + "Damaged");
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
                Self.Sprite.ChangeSpriteAnimation("Rope" + direction);
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
            int randomDirection = Game1.random.Next(3, 4);
          
            if (randomDirection == (int)direction)
            {
                randomDirection = Game1.random.Next(3, 4);
                Console.WriteLine(Self.Direction);

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
                        ResetCounter = 0;
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
            Velocity.X = 0;
            Velocity.Y = 0;
            Path.X = 0;
            Path.Y = 0;
        }

        private void SetPath()
        {
            int lengthOfPath = 16 * Game1.random.Next(1, 2);
            float distance = 0;
            Console.WriteLine("Direction is "+Self.Direction);
            switch (Self.Direction)
            {

                case (States.Direction.Left):
                    distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                 
                    Velocity.X = -Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("RopeLeft");
                    direction = "Left";
                    break;

                case (States.Direction.Right):
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("RopeRight");
                    direction = "Right";
                    break;
                default:
                    distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                    Path.X = distance;
                    Path.Y = Self.Position.Y;
                    Velocity.X = Self.BaseSpeed;
                    Velocity.Y = 0;
                    Self.Sprite.ChangeSpriteAnimation("RopeRight");
                    direction = "Right";
                    break;
            }
        }

        private void Pursue()
        {
            Path = Vector2.Zero;

            if ((Math.Abs(Self.Position.Y - Game.Link.Position.Y) < 48) && Math.Abs(Self.Position.X - Game.Link.Position.X) < 48)
            {
                Reset();
                Self.State = States.MonsterState.Attacking;
            }
            else
            {
               

                    // If Link is to the left of Rope
                    if (Self.Position.X > Game.Link.Position.X)
                    {
                        Self.Direction = States.Direction.Left;
                        Path.X = Game.Link.Position.X;
                        Path.Y = Self.Position.Y;
                        Velocity.X = -Self.BaseSpeed;
                        Velocity.Y = 0;
                        Self.Sprite.ChangeSpriteAnimation("RopeLeft");
                        direction = "Left";
                    }
                    // If Link is to the right of Rope
                    else
                    {
                        Self.Direction = States.Direction.Right;
                        Path.X = Game.Link.Position.X;
                        Path.Y = Self.Position.Y;
                        Velocity.X = Self.BaseSpeed;
                        Velocity.Y = 0;
                        Self.Sprite.ChangeSpriteAnimation("RopeRight");
                        direction = "Right";
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
