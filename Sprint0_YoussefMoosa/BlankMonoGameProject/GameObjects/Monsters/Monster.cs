namespace Sprint03
{
    // Abstract class allows for code reuse since a lot of NPC's share similar variables
    // Look at the Stalfos and Goriyas classes for commments about specific implementations
    public abstract class Monster
    {
        public Sprite Sprite;
        public IStateMachine StateMachine;
        public enum MonsterState
        {
            Idle,           // NPC remains idle due to clock item or other effects
            Moving,         // NPC randomly moves around (Only behavior most NPCs)
            Attacking,
            Damaged,        // State for when NPC is damaged by Link
            Dead            // State for when NPC is slain (RIP)
        }
        public enum MonsterDirection
        {
            Up,
            Right,
            Down,
            Left
        }

        // Keep track of basic info about NPC
        public MonsterState State;
        public MonsterDirection Direction;
        public int hitpoints;
        public int maxHP;
        public int attackDamage;

        public virtual void Draw()
        {
            Sprite.DrawSprite();
        }

        public void Update()
        {
            // Calls respective behavior for each state
            switch (State)
            {
                case (MonsterState.Idle):
                    StateMachine.IdleState();
                    break;
                case (MonsterState.Moving):
                    StateMachine.MoveState();
                    break;
                case (MonsterState.Dead):
                    StateMachine.DeadState();
                    break;
                case (MonsterState.Damaged):
                    StateMachine.DamagedState();
                    break;
                default:
                    State = MonsterState.Idle;
                    Direction = MonsterDirection.Down;
                    break;
            }

        }

    }

}