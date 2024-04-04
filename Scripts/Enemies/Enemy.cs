using Godot;

public partial class Enemy : CharacterBody2D
{
    public float Speed { get; protected set; } = 150.0f;
    public float GravityMultiplier { get; protected set; } = 1.2f;
    public float PlayerSpeedMultiplier { get; protected set; } = .75f;
    public float JumpSpeed { get; protected set; } = -500f;
    public float AttackDistance { get; protected set; } = 50f;
    public float SightDistance { get; protected set; } = 400f;
    protected Player Player { get; set; }


    
}