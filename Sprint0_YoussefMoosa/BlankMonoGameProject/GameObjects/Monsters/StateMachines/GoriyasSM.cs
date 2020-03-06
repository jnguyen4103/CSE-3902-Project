using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class GoriyasSM : IStateMachine
    {
        IEffect Boomerang;
        private readonly int damgeDuration = 45;
        private int AttackTimer = 0;
        private int AttackThreshold = 5;

        public GoriyasSM(Monster Goriyas, Game1 game)
        {
            self = Goriyas;
            Game = game;
        }

        public override void IdleState()
        {
            if (AttackTimer >= AttackThreshold)
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
                AttackTimer++;
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
            Timer++;
            if(Timer == 1)
            {
                Boomerang = new BoomerangEffect(self.Sprite, Game, ConvertToLinkDirection(self.Direction), Game.EffectSpriteSheet, Game.spriteBatch);
                Boomerang.CreateEffect();
            }
            else if (Timer >= 32*AttackThreshold)
            {
                Timer = 0;
                AttackTimer = 0;
                IdleState();
            }
        }

        protected override void DirectionString()
        {
            string direction = "";
            switch(self.Direction)
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
            self.Name = "Goriyas" + direction;
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

        private Link.LinkDirection ConvertToLinkDirection(Monster.MonsterDirection givenDirection)
        {
            Link.LinkDirection monsterLinkDirection;
            switch (givenDirection)
            {
                case (Monster.MonsterDirection.Up):
                    monsterLinkDirection = Link.LinkDirection.Up;
                    break;
                case (Monster.MonsterDirection.Right):
                    monsterLinkDirection = Link.LinkDirection.Right;
                    break;
                case (Monster.MonsterDirection.Down):
                    monsterLinkDirection = Link.LinkDirection.Down;
                    break;
                default:
                    monsterLinkDirection = Link.LinkDirection.Left;
                    break;
            }

            return monsterLinkDirection;
        }
    }
}