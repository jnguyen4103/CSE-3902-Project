namespace Sprint03
{
    public interface IStateMachine
    {
        Monster Self {get; set;}
        void IdleState();
        void MoveState();
        void AttackState();
        void DamagedState();
        void SpawnState();
        void DeadState();
        void ResetMovement(States.Direction direction);
    }
}
