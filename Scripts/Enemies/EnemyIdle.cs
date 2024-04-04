using System.Diagnostics;
using Godot;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(Enemy enemy, EnemyStateMachine enemyStateMachine, Player player) : base(enemy, enemyStateMachine, player)
    {
    }

    public override void Enter(EnemyState previousState)
    {
        _enemy.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Enemy_Idle");
        _enemy.Velocity = new Vector2(0, 0);
    }
    public override void Update()
    {
        var posDifference = _player.GlobalPosition - _enemy.GlobalPosition;
        var isSighted = posDifference.Length() > _enemy.AttackDistance && posDifference.Length() < _enemy.SightDistance;

        if (isSighted) {
            _enemyStateMachine.ChangeState(nameof(EnemyMove));
        }
    }
    public override void RegisterPlayer(Player player)
    {
            base.RegisterPlayer(player);
            
    }
}