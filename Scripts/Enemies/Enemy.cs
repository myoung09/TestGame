using System;
using System.Diagnostics;
using Godot;

public delegate void EnemyDeathHandler(Enemy enemy);
public partial class Enemy : CharacterBody2D
{
    public event EnemyDeathHandler EnemyDeath;
    public virtual long Health { get; set; }= 100;
    public virtual float AttackDelay { get; set; } = .667f;
    public float Speed { get; protected set; } = 150.0f;
    public float GravityMultiplier { get; protected set; } = 1.2f;
    public float PlayerSpeedMultiplier { get; protected set; } = .75f;
    public float JumpSpeed { get; protected set; } = -500f;
    public float AttackDistance { get; protected set; } = 50f;
    public float SightDistance { get; protected set; } = 300f;
    public virtual int DamageAmount { get; set; } = 10;
    protected Player Player { get; set; }

    public void Damage(long amount)
    {
        Health = Math.Max(0, Health - amount);
        Debug.WriteLine($"Enemy Health: {Health}");
        if (Health <= 0){
            EnemyDeath.Invoke(this);
        }
    }

    public void Heal(long amount)
    {
        Health = Math.Min(100, Health + amount);
    }
    
}