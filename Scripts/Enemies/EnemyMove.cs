using System;
using System.Diagnostics;
using Godot;

public class EnemyMove : EnemyState
{
    private AnimatedSprite2D _animationNode;
private bool isAttackingDistance = false;
    public EnemyMove(Enemy enemy, EnemyStateMachine playerStateMachine, Player player) : base(enemy, playerStateMachine, player)
    {
        _animationNode = enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public override void Enter(EnemyState previousState)
    {
        _animationNode.Play("Enemy_Moving");

    }
    public override void Exit()
    {

    }

    public override void Update()
    {
        var posDifference = _player.GlobalPosition - _enemy.GlobalPosition;
        var isAttackingDistance = posDifference.Length() <= _enemy.AttackDistance;

        if (isAttackingDistance) {
            Debug.WriteLine("ATTACKING");
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        var posDifference = _player.GlobalPosition - _enemy.GlobalPosition;
        var isFollowing = posDifference.Length() > _enemy.AttackDistance && posDifference.Length() < _enemy.SightDistance;

        if (isFollowing)
        {
            var direction = posDifference.Normalized();
            if (direction.X > 0)
            {
                _animationNode.FlipH = false;
            }
            else
            {
                _animationNode.FlipH = true;
            }
            _enemy.Velocity = new Vector2(direction.X, 0) * _enemy.Speed;
            _enemy.MoveAndSlide();
        }
        else if (isAttackingDistance)
        {
            _enemyStateMachine.ChangeState(nameof(EnemyIdle));
        }

    }

}