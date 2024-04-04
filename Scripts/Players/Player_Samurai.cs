using System.Diagnostics;
using Godot;


public partial class Player_Samurai : Player
{


    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private AnimatedSprite2D _animatedSprite;
    private PlayerStateMachine _stateMachine;
    

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _stateMachine = new PlayerStateMachine(this);


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
