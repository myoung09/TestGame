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
    public virtual void PhysicsUpdate(double delta) { }
    public virtual void Update() { }
    public virtual void RegisterPlayer(Player player)
    {
        
    }
    public virtual void UnregisterPlayer(Player player)
    {
        
    }

}