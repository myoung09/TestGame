using System;
using System.Diagnostics;
using Godot;

public class EnemyMove : EnemyState
{
    private AnimatedSprite2D _animationNode;

    public EnemyMove(Enemy enemy, EnemyStateMachine playerStateMachine) : base(enemy, playerStateMachine)
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
    }

    public override void PhysicsUpdate(double delta)
    {
        if (_player is not null)
        {
            var direction = (_player.GlobalPosition - _enemy.GlobalPosition).Normalized();
            if (direction.Length() > _enemy.AttackDistance){
                
            if (direction.X > 0){
                _animationNode.FlipH = false;
            }else {
                _animationNode.FlipH = true;
            }
            _enemy.Velocity = direction * _enemy.Speed;
            }
            
        }
        else
        {
            _enemyStateMachine.ChangeState(nameof(EnemyIdle));
        }
        _enemy.MoveAndSlide();
    }

}