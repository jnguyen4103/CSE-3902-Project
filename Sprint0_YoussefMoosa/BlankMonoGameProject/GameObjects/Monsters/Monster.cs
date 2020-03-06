using System;

namespace Sprint03
{
    // Abstract class allows for code reuse since a lot of NPC's share similar variables
    // Look at the Stalfos and Goriyas classes for commments about specific implementations
    public class Monster
    {
        public MonsterSprite Sprite;
        public IStateMachine StateMachine;
        protected Game1 Game;
        public string Name;
        private int DamageDirection;
        public enum MonsterState
        {
            Spawning,       // Initial State
            Idle,           // NPC remains idle due to clock item or other effects
            Moving,         // NPC randomly moves around (Only behavior most NPCs)
            Attacking,
            Damaged,        // State for when NPC is damaged by Link
            Dead            // State for when NPC is slain (RIP)
        }
        public enum MonsterDirection
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft
        }

        // Keep track of basic info about NPC
        public MonsterState State;
        public MonsterDirection Direction;
        public int hitpoints;
        public int maxHP;
        public int attackDamage;

        public Monster(MonsterSprite sprite, string name, Game1 game)
        {
            State = MonsterState.Spawning;
            Direction = MonsterDirection.Down;
            Sprite = sprite;
            Name = name;
            Game = game;
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public virtual void TakeDamage(int damage, int damageDirection)
        {
            if (State != MonsterState.Damaged 
                && State != MonsterState.Dead
                && State != MonsterState.Spawning
                && !Name.Equals("BladeTrap"))
            {
                hitpoints -= damage;
                if (hitpoints <= 0)
                {
                    State = MonsterState.Dead;
                }
                else
                {
                    Sprite.ChangeSpriteAnimation(Name + "Damaged");
                    State = MonsterState.Damaged;
                    DamageDirection = damageDirection;
                    StateMachine.DamagedState(damageDirection);
                }
            }
        }

        public virtual void IdleState()
        {
            if(State != MonsterState.Damaged && State != MonsterState.Dead)
            {
                StateMachine.IdleState();
            }
        }

        public void Update()
        {
            // Calls respective behavior for each state
            //Sprite.Update(State, Direction);
            switch (State)
            {
                case (MonsterState.Spawning):
                    StateMachine.SpawnState();
                    break;

                case (MonsterState.Idle):
                    StateMachine.IdleState();
                    break;
                case (MonsterState.Moving):
                    StateMachine.MoveState();
                    break;
                case (MonsterState.Damaged):
                    StateMachine.DamagedState(DamageDirection);
                    break;
                case (MonsterState.Dead):
                    StateMachine.DeadState();
                    break;
                default:
                    State = MonsterState.Idle;
                    Direction = MonsterDirection.Down;
                    break;
            }

        }

    }

}