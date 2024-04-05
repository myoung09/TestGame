using System;
using System.Diagnostics;
using System.Threading;
using Godot;

public class PlayerDead : PlayerState
{
    private AnimatedSprite2D animation;
    private System.Threading.Timer _timer;
    public PlayerDead(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
        animation = _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void TimerElapsed(object state)
    {
    }


    public override void Enter(PlayerState previousState)
    {

        animation.Play("Samurai_Death");
        _player.Velocity = new Vector2(0, 0);
        _timer = new System.Threading.Timer(TimerElapsed, null, 2000, Timeout.Infinite);

    }
    public override void Exit()
    {
    }

    public override void HandleInput()
    {
        if (!_player.IsOnFloor())
        {
            _playerStateMachine.ChangeState(nameof(PlayerFalling));
        }
    }
}