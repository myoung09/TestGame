using Godot;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected Player _player;
    protected EnemyStateMachine _enemyStateMachine;
    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, Player player)
    {
        _enemy = enemy;
        _enemyStateMachine = enemyStateMachine;
        _player = player;
    }
    public virtual void Enter(EnemyState previousState) { }
    public virtual void Exit() { }
    public virtual void PhysicsUpdate(double delta)
    {
        if (_player.IsDead)
        {
            _enemyStateMachine.ChangeState(nameof(EnemyIdle));
        }
        else if ( _enemy.Health <= 0) 
        {
            _enemyStateMachine.ChangeState(nameof(EnemyDeath));
        }
    }
    public virtual void Update() { }

}