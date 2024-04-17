using System;
using System.Diagnostics;
using System.Threading;
using Godot;

public class EnemyDeath : EnemyState
{
    private AnimatedSprite2D animation;
    private System.Threading.Timer _timer;
    public EnemyDeath(Enemy enemy, EnemyStateMachine enemyStateMachine, Player player) : base(enemy, enemyStateMachine, player)
    {
        animation = _enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _enemy.EnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        animation.Play("Enemy_Death");
        _enemy.Velocity = new Vector2(0, 0);
        _timer = new System.Threading.Timer(TimerElapsed, null, 2000, Timeout.Infinite);
    }

    private void TimerElapsed(object state)
    {
        _enemy.QueueFree();
    }


    public override void Enter(EnemyState previousState)
    {
        if (!_enemy.IsOnFloor())
        {
            _enemyStateMachine.ChangeState(nameof(EnemyIdle));
        }
    }
}