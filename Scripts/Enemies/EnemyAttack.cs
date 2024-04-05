using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Godot;

public class EnemyAttack : EnemyState
{
    private System.Timers.Timer _timer;
    private AnimatedSprite2D _animationNode;
    public EnemyAttack(Enemy enemy, EnemyStateMachine playerStateMachine, Player player) : base(enemy, playerStateMachine, player)
    {
        _animationNode = enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _timer = new System.Timers.Timer();
        _timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        _player.Damage(_enemy.DamageAmount);
    }

    public override void Enter(EnemyState previousState)
    {
        _animationNode.Play("Enemy_Attack");
        _timer.Interval = _enemy.AttackDelay * 1000;
        _timer.Start();

    }
    public override void Exit()
    {
        _timer.Stop();
    }

    public override void Update()
    {
        var posDifference = _player.GlobalPosition - _enemy.GlobalPosition;
        var isAttackingDistance = posDifference.Length() <= _enemy.AttackDistance;

        if (!isAttackingDistance)
        {
            _enemyStateMachine.ChangeState("previous");
        }
    }
}