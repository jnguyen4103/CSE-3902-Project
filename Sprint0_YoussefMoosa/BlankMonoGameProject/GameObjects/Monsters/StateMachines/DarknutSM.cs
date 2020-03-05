using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class DarknutSM : IStateMachine
    {
        private readonly int damgeDuration = 45;


        public DarknutSM(Monster Darknut, Game1 game)
        {
            self = Darknut;
            Game = game;
        }

        public override void IdleState()
        {
            if(DetectLink())
            {
                self.State = Monster.MonsterState.Attacking;
                AttackState();
            }
            else
            {
                getRandomDirection();
                self.State = Monster.MonsterState.Moving;
            }
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


        public override void AttackState()
        {
            int xDiff = (int)Math.Abs(Game.Link.SpriteLink.Position.X - self.Sprite.Position.X);
            int yDiff = (int)Math.Abs(Game.Link.SpriteLink.Position.Y - self.Sprite.Position.Y);

            if (Game.Link.SpriteLink.Position.Y >= self.Sprite.Position.Y && yDiff > 8)
            {
                self.Sprite.ChangeSpriteAnimation("DarknutDown");
                self.Sprite.Position.Y += 2 * self.Sprite.BaseSpeed;
            }
            else if (Game.Link.SpriteLink.Position.Y <= self.Sprite.Position.Y && yDiff > 8)
            {
                self.Sprite.ChangeSpriteAnimation("DarknutUp");
                self.Sprite.Position.Y -= 2 * self.Sprite.BaseSpeed;
            }
            else if (Game.Link.SpriteLink.Position.X > self.Sprite.Position.X && xDiff > 8)
            {
                self.Sprite.Position.X += 2 * self.Sprite.BaseSpeed;
                self.Sprite.ChangeSpriteAnimation("DarknutRight");
            }
            else if (Game.Link.SpriteLink.Position.X <= self.Sprite.Position.X && xDiff > 8)
            {
                self.Sprite.ChangeSpriteAnimation("DarknutLeft");
                self.Sprite.Position.X -= 2 * self.Sprite.BaseSpeed;
            }


        }

        protected override void DirectionString()
        {
            string direction = "";
            switch (self.Direction)
            {
                case (Monster.MonsterDirection.Up):
                    direction = "Up";
                    break;

                case (Monster.MonsterDirection.Down):
                    direction = "Down";
                    break;

                case (Monster.MonsterDirection.Left):
                    direction = "Left";

                    break;

                case (Monster.MonsterDirection.Right):
                    direction = "Right";
                    break;

                default:
                    break;
            }
            self.Name = "Darknut" + direction;
        }


        private void getRandomDirection()
        {
            int randDirection = random.Next(1, 5);
            WalkCounter = 25 * random.Next(1, 3);

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
            DirectionString();
            self.Sprite.ChangeSpriteAnimation(self.Name);
        }

        private bool DetectLink()
        {
            double DistanceApart = Math.Sqrt(Math.Pow(self.Sprite.Position.X - Game.Link.SpriteLink.Position.X, 2)
                + Math.Pow(self.Sprite.Position.Y - Game.Link.SpriteLink.Position.Y, 2));

            return (DistanceApart - 48 < 1);
        }

    }
}