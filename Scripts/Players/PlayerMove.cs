using System.Diagnostics;
using Godot;

public class PlayerMove : PlayerState
{
    private AnimatedSprite2D animationNode;
    private bool isMoving = false;

    public PlayerMove(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
        animationNode = player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public override void Enter(PlayerState previousState)
    {
        animationNode.Play("Samurai_Moving");
        isMoving = true;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!_player.IsOnFloor())
        {
            _playerStateMachine.ChangeState(nameof(PlayerFalling));
        }
        else if (Input.IsActionJustPressed("ui_accept"))
        {
            _playerStateMachine.ChangeState(nameof(PlayerJump));
        }
        else if (Input.IsActionPressed("fire"))
        {
            _playerStateMachine.ChangeState(nameof(PlayerMedAttack));
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            animationNode.FlipH = false;
            PlayerStateMachine.Direction = 1f;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animationNode.FlipH = true;
            PlayerStateMachine.Direction = -1f;
        }

        else
        {
            _playerStateMachine.ChangeState(nameof(PlayerIdle));
        }
    }
    public override void Exit()
    {
        isMoving = false;
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate(double delta)
    {
        if (isMoving)
        {
            _player.Velocity = new Vector2(_player.Speed * PlayerStateMachine.Direction, 0);
            _player.MoveAndSlide();
        }
    }
}