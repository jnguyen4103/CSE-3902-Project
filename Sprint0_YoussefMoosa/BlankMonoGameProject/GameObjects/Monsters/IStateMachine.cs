namespace Sprint03
{
    // Enemies have basic behavior
    // Patrolling - Randomly moving around
    // Attacking - Using attacks and/or chasing Link
    // Dead - Got rekt by Link
    // TakeDamage - Got injured
    public interface IStateMachine
    {
        void SpawnState();
        void IdleState();
        void MoveState();
        void AttackState();
        void DamagedState(int directionDamaged);
        void DeadState();
    }

}