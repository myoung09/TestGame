using Godot;

public abstract class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _playerStateMachine;
    public PlayerState(Player player, PlayerStateMachine playerStateMachine)
    {
        _player = player;
        _playerStateMachine = playerStateMachine;
    }
    public virtual void Enter(PlayerState previousState){}
    public virtual void Exit(){}

    public virtual void HandleInput(){}
    public virtual void PhysicsUpdate(double delta){}
    public virtual void Update(){}

}