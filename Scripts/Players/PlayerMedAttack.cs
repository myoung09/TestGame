
using System;
using System.Diagnostics;
using System.Timers;
using Godot;

public class PlayerMedAttack : PlayerState
{

    private float delay = 500f;
    private static System.Timers.Timer _timer;
    private bool isAttacking = false;
    private Enemy _currentEnemy;

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
        _currentEnemy = _player.AggroedEnemy();
    }

    public override void Exit()
    {
        isAttacking = false;
        _timer.Stop();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!isAttacking)
        {
            _playerStateMachine.ChangeState("previous");
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        if (isAttacking)
        {
            
           if (_currentEnemy != null)
            {
                _currentEnemy.Damage(_player.MediumAttackDamage);
            }
        }
    }

    public override void Update()
    {

    }
}