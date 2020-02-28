﻿using System;
namespace Sprint03
{
    public class Stalfos : Monster
    {
        // Setting info about NPC and default parameters
        public Stalfos(StalfosSprite sprite)
        {
            this.State = MonsterState.Idle;
            this.Direction = MonsterDirection.Down;
            this.Sprite = sprite;
            this.hitpoints = 1;
            this.maxHP = 1;
            this.attackDamage = 1;
            this.StateMachine = new StalfosSM(this);
        }
    }

    public class StalfosSM : IStateMachine
    {
        private Monster self;
        private Random random = new Random();
        private int WalkCounter = 0;


        public StalfosSM(Stalfos Stalfos)
        {
            self = Stalfos;
        }


        public void MoveState()
        {
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


        public void DamagedState()
        {

        }

        public void DeadState()
        {
            // Turn off visibility in sprite

        }

        public void IdleState()
        {
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
