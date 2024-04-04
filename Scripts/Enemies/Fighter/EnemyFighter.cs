using System;
using System.Diagnostics;
using Godot;


public partial class EnemyFighter : Enemy
{
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private EnemyStateMachine _stateMachine;

    public override void _Ready()
    {
        Player = (Player)GetTree().GetNodesInGroup("Player")[0];
        _stateMachine = new EnemyStateMachine(this, Player);

    }

    public override void _Process(double delta)
    {
        _stateMachine.CurrentState.Update();

    }
    public override void _PhysicsProcess(double delta)
    {
        //Gravity
        if (!IsOnFloor())
        {
            Velocity += new Vector2(0, gravity * GravityMultiplier * (float)delta);
            MoveAndSlide();
        }
        _stateMachine.CurrentState.PhysicsUpdate(delta);

    }
}
