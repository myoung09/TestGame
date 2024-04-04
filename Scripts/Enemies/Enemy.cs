using Godot;

public partial class Enemy : CharacterBody2D
{
    public float Speed { get; protected set; } = 200.0f;
    public float GravityMultiplier { get; protected set; } = 1f;
    public float PlayerSpeedMultiplier { get; protected set; } = .75f;
    public float JumpSpeed { get; protected set; } = -500f;
    public float AttackDistance { get; protected set; } = 20f;
}