/* Contributors
* Stephen Hogg
* Grant Gabel
*/
using Microsoft.Xna.Framework;
using System;

namespace Sprint03
{
    public class KeeseSM : IStateMachine
    {
        private Game1 Game;
        private int Timer = 0;
        private int Counter = 0;
        private int SpawnDelay = 120;
        private int DeathDelay = 20;
        private Vector2 Path = new Vector2(0, 0);
        private Vector2 Velocity = new Vector2(0, 0);
        private int sleepDuration = 0;
    
        public Monster Self { get; set; }

        public KeeseSM(Monster Keese, Game1 game)
        {
            Game = game;
            Self = Keese;
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
                Self.Sprite.ChangeSpriteAnimation("Keese");
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
            if (sleepDuration == 0)
            {
                if (Path == Self.Position && Self.State != States.MonsterState.Damaged)
                {
                    Counter++;
                    if(Counter == 6)
                    {
                        Counter = 0;
                        sleepDuration = 30 * Game1.random.Next(0, 5);
                    }
                    Self.State = States.MonsterState.Idle;
                    Reset();
                }
                else
                {
                    Self.Position += Velocity;
                    Self.Sprite.UpdatePosition(Self.Position);
                }
            }
            else
            {
                sleepDuration--;
            }
        }



        public void AttackState()
        {
        }

        public void DamagedState()
        {
            Timer = 0;
            Self.State = States.MonsterState.Dead;
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
            if (randomDirection == (int) direction)
            {
                randomDirection = Game1.random.Next(0, 4);
            }
            else
            {
                Self.Direction = (States.Direction)randomDirection;
                if(Self.State != States.MonsterState.Damaged)
                {
                    SetPath();
                    if (Game1.random.Next(0, 5) < 4) { SetDiagonal(); }
                    Self.State = States.MonsterState.Moving;
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
            // Length in 16x16 blocks
            int lengthOfPath = 16 * Game1.random.Next(1, 4);
            float distance = 0;

            // Restricts movement so Sprites are in a 16x16 tile
            // x - (x % 16) will always be a multiple of 16
            switch (Self.Direction)
            {
                case (States.Direction.Up):
                    distance = (Self.Position.Y - lengthOfPath) - ((Self.Position.Y - lengthOfPath) % 16) - 2;
                    Path.X = Self.Position.X;
                    Path.Y = distance;
                    Velocity.X = 0;
                    Velocity.Y = -Self.BaseSpeed;
                    break;

                case (States.Direction.Down):
                    distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16) - 2;
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

        private void SetDiagonal()
        {
            int randDir = 0;
            int lengthOfPath = 16 * Game1.random.Next(1, 4);
            float distance = 0;

            // If Keese is already moving vertically
            if ((int) Self.Direction > 1)
            {
                randDir = Game1.random.Next(0, 2);
                switch (randDir)
                {
                    case (0):
                        distance = (Self.Position.Y - lengthOfPath) - ((Self.Position.Y - lengthOfPath) % 16) - 2;
                        Path.Y = distance;
                        Velocity.Y = -Self.BaseSpeed;
                        break;

                    case (1):
                        distance = (Self.Position.Y + lengthOfPath) - ((Self.Position.Y + lengthOfPath) % 16) - 2;
                        Path.Y = distance;
                        Velocity.Y = Self.BaseSpeed;
                        break;
                }
            }

            // If Keese is already moving horizontally
            if ((int)Self.Direction < 1)
            {
                randDir = Game1.random.Next(2, 4);
                switch (randDir)
                {
                    case (2):
                        distance = (Self.Position.X - lengthOfPath) - ((Self.Position.X - lengthOfPath) % 16);
                        Path.X = distance;
                        Velocity.X = -Self.BaseSpeed;
                        break;

                    case (3):
                        distance = (Self.Position.X + lengthOfPath) - ((Self.Position.X + lengthOfPath) % 16);
                        Path.X = distance;
                        Velocity.X = Self.BaseSpeed;
                        break;
                }
            }
        }
    }
}
