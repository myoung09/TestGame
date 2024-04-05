using System.Diagnostics;
using Godot;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter(PlayerState previousState)
    {
        _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Samurai_Idle");
        _player.Velocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!_player.IsOnFloor())
        {
            _playerStateMachine.ChangeState(nameof(PlayerFalling));
        }
        else if (Input.IsActionPressed("ui_right") || Input.IsActionPressed("ui_left"))
        {
            _playerStateMachine.ChangeState(nameof(PlayerMove));
        }
        else if (Input.IsActionJustPressed("ui_accept") && _player.IsOnFloor())
        {
            _playerStateMachine.ChangeState(nameof(PlayerJump));
        }
        else if (Input.IsActionPressed("fire"))
        {
            _playerStateMachine.ChangeState(nameof(PlayerMedAttack));
        }
    }

    public override void PhysicsUpdate(double delta)
    {
    }

    public override void Update()
    {

    }
}