using Godot;

public class PlayerFalling : PlayerState
{
    private bool isMoving;
    private AnimatedSprite2D animationNode;

    public PlayerFalling(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter(PlayerState previousState)
    {
        animationNode = _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animationNode.Play("Samurai_Falling");
    }

    public override void Exit()
    {
    }

    public override void HandleInput()
    {
        if (_player.IsOnFloor())
        {
            _playerStateMachine.ChangeState(nameof(PlayerIdle));
        }
        else if (Input.IsActionPressed("fire"))
        {
            _playerStateMachine.ChangeState(nameof(PlayerMedAttack));
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
        if (isMoving) {
                _player.Velocity = new Vector2(_player.Speed * _player.PlayerSpeedMultiplier * PlayerStateMachine.Direction, _player.Velocity.Y);
                
            }
    }

    public override void Update()
    {

    }
}