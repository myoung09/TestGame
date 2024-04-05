using System;
using System.Diagnostics;
using Godot;


public partial class Player_Samurai : Player
{


    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private AnimatedSprite2D _animatedSprite;
    private Area2D _sightRange;

    private PlayerStateMachine _stateMachine;
    

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _sightRange =  GetNode<Area2D>("SightRange");
        _stateMachine = new PlayerStateMachine(this);

        _sightRange.BodyEntered +=  ObjectEnteredSightRange;
        _sightRange.BodyExited +=   ObjectExitedSightRange;

    }

    private void ObjectExitedSightRange(Node2D obj)
    {
        RemoveSightedObject(obj);   
    }
    private void ObjectEnteredSightRange(Node2D obj)
    {
        AddSightedObject(obj);
    }


    public override void _Process(double delta)
    {
        _stateMachine.CurrentState.HandleInput();
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
