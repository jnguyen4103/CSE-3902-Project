using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class GelSM : IStateMachine
    {
        private int sleep;
        public GelSM(Monster Gel, Game1 game)
        {
            self = Gel;
            Game = game;
            defaultSpriteName = "Gel";
        }

        public override void IdleState()
        {
            Timer++;
            if (Timer >= sleep)
            {
                getRandomDirection();
                self.State = Monster.MonsterState.Moving;
                Timer = 0;

            }  
        }

        public override void MoveState()
        {
            if (self.Sprite.Position.X >= Game.WalkingRect.Width || self.Sprite.Position.Y >= Game.WalkingRect.Height)
            {
                WalkCounter = 0;
                Timer = 0;
                sleep = 0;
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


            }
            else
            {
                sleep = 45*random.Next(1, 5);
                Timer = 0;
                self.State = Monster.MonsterState.Idle;
            }
        }


        public override void DamagedState(int directionDamaged)
        {
            self.State = Monster.MonsterState.Damaged;
        }

        private void getRandomDirection()
        {
            int randDirection = random.Next(1, 5);
            WalkCounter = 25*random.Next(1, 3);

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
