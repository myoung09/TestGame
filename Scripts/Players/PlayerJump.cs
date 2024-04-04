using System.Diagnostics;
using Godot;

public class PlayerJump : PlayerState
{

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private bool isFalling;
    private bool isMoving;
    private float gravityMultiplier = .9f;
    private AnimatedSprite2D animationNode;

    public PlayerJump(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter(PlayerState previousState)
    {
        animationNode = _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationNode.Play("Samurai_Jumping");
        _player.Velocity = new Vector2(_player.Velocity.X * _player.PlayerSpeedMultiplier, _player.JumpSpeed);
    }

    public override void Exit()
    {
    }
    public override void HandleInput()
    {
        if (isFalling)
        {
            _playerStateMachine.ChangeState(nameof(PlayerFalling));
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            animationNode.FlipH = false;
            PlayerStateMachine.Direction = 1f;
            isMoving = true;
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animationNode.FlipH = true;
            PlayerStateMachine.Direction = -1f;
            isMoving = true;
        }

    }

    public override void PhysicsUpdate(double delta)
    {
        if (!isFalling)
        {
            if (isMoving) {
                _player.Velocity = new Vector2(_player.Speed * _player.PlayerSpeedMultiplier * PlayerStateMachine.Direction, _player.Velocity.Y);
                Debug.WriteLine("Velocity: " + _player.Velocity);
            }
            _player.Velocity += new Vector2(0, gravity * gravityMultiplier * (float)delta);
            _player.MoveAndSlide();
        }
    }

    public override void Update()
    {
        isFalling = _player.Velocity.Y >= 0;
    }
}