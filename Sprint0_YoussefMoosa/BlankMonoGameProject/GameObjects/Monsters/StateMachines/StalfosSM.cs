using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class StalfosSM : IStateMachine
    {
        private Game1 Game;
        private Monster self;
        private Random random = new Random();
        private int WalkCounter = 0;
        private int Timer = 0;
        private int damgeDuration = 45;


        public StalfosSM(Monster Stalfos, Game1 game)
        {
            self = Stalfos;
            Game = game;
        }

        public void SpawnState()
        {
            Timer++;
            if (Timer == 1)
            {
                self.Sprite.ChangeSpriteAnimation("SpawningCloud");
                self.Sprite.CurrentSpeed.X = 0;
                self.Sprite.CurrentSpeed.Y = 0;
                self.Sprite.FPS = 3;
            } else if (Timer >= 60)
            {
                self.Sprite.ChangeSpriteAnimation("StalfosWalk");
                self.Sprite.FPS = 8;
                IdleState();
            }
        }


        public void MoveState()
        {
            if(self.Sprite.Position.X >= Game.WalkingRect.Width || self.Sprite.Position.Y >= Game.WalkingRect.Height)
            {
                WalkCounter = 0;
                IdleState();
            }

            if (WalkCounter > 0)
            {
                switch (self.Direction)
                {
                    case (Monster.MonsterDirection.Down):
                        self.Sprite.CurrentSpeed.X = 0;
                        self.Sprite.CurrentSpeed.Y = self.Sprite.BaseSpeed;
                        break;
                    case (Monster.MonsterDirection.Up):
                        self.Sprite.CurrentSpeed.X = 0;
                        self.Sprite.CurrentSpeed.Y = -self.Sprite.BaseSpeed;
                        break;
                    case (Monster.MonsterDirection.Left):
                        self.Sprite.CurrentSpeed.X = -self.Sprite.BaseSpeed;
                        self.Sprite.CurrentSpeed.Y = 0;
                        break;
                    case (Monster.MonsterDirection.Right):
                        self.Sprite.CurrentSpeed.X = self.Sprite.BaseSpeed;
                        self.Sprite.CurrentSpeed.Y = 0;
                        break;

                    default:
                        break;
                }
                WalkCounter--;

            } else
            {
                self.State = Monster.MonsterState.Idle;
            }
        }


        public void AttackState()
        {
            // There is no attack state for this NPC
            // If we want more advanced actions then implement later on
        }


        public void DamagedState(int directionDamaged)
        {
            if(Timer == 0)
            {
                self.Sprite.ChangeSpriteAnimation("StalfosDamaged");
            }

            Timer++;
            Pushback(directionDamaged);
            if (Timer >= damgeDuration)
            {
                self.Sprite.CurrentSpeed.X = 0;
                self.Sprite.CurrentSpeed.Y = 0;
                Timer = 0;
                IdleState();
                self.State = Monster.MonsterState.Idle;
                self.Sprite.ChangeSpriteAnimation("StalfosWalk");

            }
        }

        private void Pushback(int damagedDirection)
        {
            switch (damagedDirection)
            {
                case (0):
                    self.Sprite.CurrentSpeed.X = 0;
                    self.Sprite.CurrentSpeed.Y = self.Sprite.BaseSpeed;
                    break;
                case (1):
                    self.Sprite.CurrentSpeed.X = 0;
                    self.Sprite.CurrentSpeed.Y = -self.Sprite.BaseSpeed;
                    break;
                case (2):
                    self.Sprite.CurrentSpeed.X = -self.Sprite.BaseSpeed;
                    self.Sprite.CurrentSpeed.Y = 0;
                    break;
                case (3):
                    self.Sprite.CurrentSpeed.X = self.Sprite.BaseSpeed;
                    self.Sprite.CurrentSpeed.Y = 0;
                    break;
                default:
                    break;
            }
        }

        public void DeadState()
        {
            Timer++;
            if (Timer == 1)
            {
                WalkCounter = 0;
                self.Sprite.CurrentSpeed = new Vector2(0, 0);
                self.State = Monster.MonsterState.Dead;
                self.Sprite.ChangeSpriteAnimation("Death");
            } else if (Timer >= (20 / self.Sprite.FPS * 8))
            {
                self.Sprite.KillSprite();
                Timer = 0;
            }

        }

        public void IdleState()
        {
            if (Timer == 1)
            {
                int wait = random.Next(20, 60);

            }

            self.State = Monster.MonsterState.Moving;
            getRandomDirection();
        }

        private void getRandomDirection()
        {
            // Generates random distance to path to
            // NPC only moves in X or Y direction, never
            // moves at a diagonal
            int randDirection = random.Next(1, 5);
            WalkCounter = 30 * random.Next(2, 6);

            switch (randDirection)
            {
                case (1):
                    self.Direction = Monster.MonsterDirection.Down;
                    break;
                case (2):
                    self.Direction = Monster.MonsterDirection.Up;
                    break;
                case (3):
                    self.Direction = Monster.MonsterDirection.Left;
                    break;
                case (4):
                    self.Direction = Monster.MonsterDirection.Right;
                    break;
                default:
                    break;
            }
        }
    }
}
