using System;
using System.Diagnostics;
using Godot;


public partial class EnemyFighter : Enemy
{
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private EnemyStateMachine _stateMachine;

    public override void _Ready()
    {
        _stateMachine = new EnemyStateMachine(this);
        GetNode<Area2D>("InteractionBubble").BodyEntered += InteractiveArea_BodyEntered;
        GetNode<Area2D>("InteractionBubble").BodyExited += InteractiveArea_BodyExited;

    }

    private void InteractiveArea_BodyExited(Node2D body)
    {
        if (body is Player player)
        {
            Debug.WriteLine("Body exited: " + body);
            _stateMachine.CurrentState.UnregisterPlayer(player);
        }
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

    private void InteractiveArea_BodyEntered(Node2D body)
    {

        if (body is Player player)
        {
            Debug.WriteLine("Body entered: " + body);
            _stateMachine.CurrentState.RegisterPlayer(player);
        }
    }

}
