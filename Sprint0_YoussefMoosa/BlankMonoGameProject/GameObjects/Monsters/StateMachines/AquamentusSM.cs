using Microsoft.Xna.Framework;
using System;
namespace Sprint03
{
    public class AquamentusSM : IStateMachine
    {
        IEffect FireBallTop;
        IEffect FireBallMiddle;
        IEffect FireBallBottom;
        private readonly int damageDuration = 45;
        private int AttackTimer = 0;
        private int AttackThreshold = 4;

        public AquamentusSM(Monster Aquamentus, Game1 game)
        {
            self = Aquamentus;
            Game = game;
            self.Direction = Monster.MonsterDirection.Left;
        }

        public override void IdleState()
        {

            if (AttackTimer >= AttackThreshold)
            {
                Vector2 Speed = GetFireballSpeed();
                FireBallTop = new FireballEffect(self.Sprite, Game, new Vector2(Speed.X, Speed.Y - 0.5f), Game.EffectSpriteSheet, Game.spriteBatch);
                FireBallMiddle = new FireballEffect(self.Sprite, Game, Speed, Game.EffectSpriteSheet, Game.spriteBatch);
                FireBallBottom = new FireballEffect(self.Sprite, Game, new Vector2(Speed.X, Speed.Y + 0.5f), Game.EffectSpriteSheet, Game.spriteBatch);
                FireBallTop.CreateEffect();
                FireBallMiddle.CreateEffect();
                FireBallBottom.CreateEffect();
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
                IdleState();
                self.State = Monster.MonsterState.Idle;
                AttackTimer++;
            }
        }



        public override void AttackState()
        {
            Timer++;
            Console.WriteLine(Timer);

            if (Timer == 1)
            {
                self.Sprite.ChangeSpriteAnimation("AquamentusAttack");
            }
            else if (Timer >= 4*self.Sprite.FPS)
            {
                self.Sprite.ChangeSpriteAnimation("Aquamentus");
                AttackTimer = 0;
                Timer = 0;
                self.State = Monster.MonsterState.Idle;
                IdleState();
            }
        }


        public override void DamagedState(int directionDamaged)
        {
            self.State = Monster.MonsterState.Damaged;
            Timer++;
            Pushback(directionDamaged);
            if (Timer >= damageDuration)
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
            WalkCounter = 8 * random.Next(2, 6);
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

        private Vector2 GetFireballSpeed()
        {
            float xVel = (Game.Link.SpriteLink.Position.X - self.Sprite.Position.X) / 45;
            float yVel = (Game.Link.SpriteLink.Position.Y - self.Sprite.Position.Y) / 45;
            return new Vector2(xVel, yVel);
        }

    }
}