﻿/* Contributors
* Stephen Hogg
*/
using System;
using Microsoft.Xna.Framework;

namespace Sprint03
{
    public class Monster: IGameObject
    {

        public IStateMachine StateMachine;
        protected Game1 Game;
        public string Name;

        // Keep track of basic info about NPC
        public float BaseSpeed; // Measured in Pixels per Frame
        public int HP;
        public int MaxHP;
        public int AttackDamage;

        // Private copies of basic info about NPC used to unpause them
        private float BaseSpeedRestore;

        public States.MonsterState State;
        public States.Direction Direction;
         
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public MonsterSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public bool Flies { get; set; } = false;

        public Monster(MonsterSprite sprite, Vector2 spawn, string name, Game1 game)
        {
            Sprite = sprite;
            Name = name;
            Game = game;
            sprite.UpdatePosition(spawn);
            Position = spawn;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            State = States.MonsterState.Spawning;
            Direction = States.Direction.Up;
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            CanDamage = false;
            if (State == States.MonsterState.Attacking || State == States.MonsterState.Moving
                || State == States.MonsterState.Idle)
                {
                HP -= damage;
                if (HP <= 0)
                {
                    State = States.MonsterState.Dead;
                }
                else
                {
                    Sprite.ChangeSpriteAnimation(Name + "Damaged");
                    Direction = directionHit;
                    State = States.MonsterState.Damaged;
                }
            }
        }

        public void Pause()
        {
            State = States.MonsterState.Idle;
            BaseSpeedRestore = BaseSpeed;
            BaseSpeed = 0f;
        }

        public void Unpause()
        {
            State = States.MonsterState.Idle;
            BaseSpeed = BaseSpeedRestore;
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            switch (State)
            {
                case (States.MonsterState.Spawning):
                    StateMachine.SpawnState();
                    break;

                case (States.MonsterState.Attacking):
                    StateMachine.AttackState();
                    break;
 
                case (States.MonsterState.Idle):
                    StateMachine.IdleState();
                    break;
                case (States.MonsterState.Moving):
                    StateMachine.MoveState();
                    break;
                case (States.MonsterState.Damaged):
                    StateMachine.DamagedState();
                    break;
                case (States.MonsterState.Dead):
                    StateMachine.DeadState();
                    break;
                default:
                    State = States.MonsterState.Idle;
                    break;
            }

        }

    }

}