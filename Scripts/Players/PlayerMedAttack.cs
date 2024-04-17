
using System;
using System.Diagnostics;
using System.Timers;
using Godot;

public class PlayerMedAttack : PlayerState
{
    private AnimatedSprite2D attackAnimation;
    private bool isAnimationComplete = false;
    private Enemy _currentEnemy;

    public PlayerMedAttack(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
        attackAnimation = _player.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        attackAnimation.AnimationFinished += AnimationFinished;
    }
    private void AnimationFinished()
    {
        isAnimationComplete = true;
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        isAnimationComplete = true;
    }
    public override void Enter(PlayerState previousState)
    {
        isAnimationComplete = false;
        attackAnimation.Play("Samurai_Medium_Attack");
        _currentEnemy = _player.AggroedEnemy();

        if (_currentEnemy != null)
        {
            _currentEnemy.Damage(_player.MediumAttackDamage);

        }
    }

    public override void Exit()
    {
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (isAnimationComplete)
        {
            _playerStateMachine.ChangeState("previous");
        }
    }

    public override void PhysicsUpdate(double delta)
    {
    }

    public override void Update()
    {

    }
}