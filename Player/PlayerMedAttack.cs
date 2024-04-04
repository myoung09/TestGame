
using System;
using System.Diagnostics;
using System.Timers;
using Godot;

public class PlayerMedAttack : PlayerState
{

    private float delay = 300f;
    private static System.Timers.Timer _timer;
    private bool isAttacking = false;
    public PlayerMedAttack(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {       
    }
    private void AddTimer()
    {
        if (_timer == null)
        {
            // Create the timer instance if it doesn't exist
            _timer = new System.Timers.Timer();
            _timer.Interval = delay;
            _timer.Elapsed += TimerElapsed;
        }
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        isAttacking = false;
    }
    public override void Enter(PlayerState previousState)
    {
        AddTimer();
        _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Samurai_Medium_Attack");
        isAttacking = true;
        _timer.Start();
    }

    public override void Exit()
    {
        isAttacking = false;
        _timer.Stop();
    }

    public override void HandleInput()
    {
        if (!isAttacking)
        {
            _playerStateMachine.ChangeState("previous");
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        if (isAttacking)
        {
           //Todo: Add attack logic
        }
    }

    public override void Update()
    {

    }
}