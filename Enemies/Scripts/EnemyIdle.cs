using System.Diagnostics;
using Godot;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter(EnemyState previousState)
    {
        _enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Enemy_Idle");
        _enemy.Velocity = new Vector2(0, 0);
    }

    public override void RegisterPlayer(Player player)
    {
            base.RegisterPlayer(player);
            _enemyStateMachine.ChangeState(nameof(EnemyMove));
    }
}