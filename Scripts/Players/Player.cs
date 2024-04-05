using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D
{
    public long Health { get; private set; } = 100;
    public float Speed { get; protected set; } = 300.0f;
    public float GravityMultiplier { get; protected set; } = 1f;
    public float PlayerSpeedMultiplier { get; protected set; } = .75f;
    public float JumpSpeed { get; protected set; } = -500f;
    public long Coins { get; protected set; } = 0;
    public float AttackRange { get; set; } = 100f;
    public bool IsDead { get { return Health <= 0; } }
    public List<Node2D> VisibleObjects { get; set; } = new List<Node2D>();
    public long MediumAttackDamage { get; protected set; } = 50;

    public void AddCoins(long amount)
    {
        Coins += amount;
    }

    public void Damage(long amount)
    {
        Health = Math.Max(0, Health - amount);
    }

    public void Heal(long amount)
    {
        Health = Math.Min(100, Health + amount);
    }
    public void AddSightedObject(Node2D obj)
    {
        VisibleObjects.Add(obj);
        if (obj is Enemy enemy)
        {
            enemy.EnemyDeath += RemoveSightedObject;
        }
    }
    public void RemoveSightedObject(Node2D obj)
    {
        VisibleObjects.Remove(obj);
        if (obj is Enemy enemy)
        {
            Debug.WriteLine("Enemy Removed");
            enemy.EnemyDeath -= RemoveSightedObject;
        }
    }
    public Enemy AggroedEnemy()
    {
        if (VisibleObjects != null && VisibleObjects.Count > 0)
        {
            {
                var enemies = VisibleObjects.Where(x => x is Enemy && x.GlobalPosition.DistanceTo(GlobalPosition) <= AttackRange).OrderBy(x => x.GlobalPosition.DistanceTo(GlobalPosition)).Cast<Enemy>();
                if (enemies.Count() > 0)
                {
                    {
                        return enemies.First();
                    }
                }
            }

        }
        return null;
    }
}