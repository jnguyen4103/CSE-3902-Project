using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class StalfosSM : IStateMachine
    {
        private readonly int damgeDuration = 45;


        public StalfosSM(Monster Stalfos, Game1 game)
        {
            self = Stalfos;
            Game = game;
        }

        public override void IdleState()
        {
            self.State = Monster.MonsterState.Moving;
            getRandomDirection();
        }

        public override void MoveState()
        {
            if (self.Sprite.Position.X >= Game.WalkingRect.Width || self.Sprite.Position.Y >= Game.WalkingRect.Height)
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

            }
            else
            {
                self.State = Monster.MonsterState.Idle;
            }
        }

        public override void DamagedState(int directionDamaged)
        {
            self.State = Monster.MonsterState.Damaged;
            Timer++;
            Pushback(directionDamaged);
            if (Timer >= damgeDuration)
            {
                self.Sprite.CurrentSpeed.X = 0;
                self.Sprite.CurrentSpeed.Y = 0;
                Timer = 0;
                IdleState();
                self.Sprite.ChangeSpriteAnimation(self.Name);

            }
        }


        private void getRandomDirection()
        {
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
