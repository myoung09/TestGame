using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public class EnemyStateMachine
{
    public static float Direction = 1f;


    public EnemyState CurrentState;
    public Stack<EnemyState> TransitionTable;


    private Enemy _enemy;
    private EnemyIdle _enemyIdle;
    private EnemyMove _enemyMove;
    private EnemyAttack _enemyAttack;
    private EnemyDeath _enemyDeath;
    private bool isHalted = false;

    //initialize
    public EnemyStateMachine(Enemy enemy, Player player)
    {
        _enemy = enemy;
        _enemy.EnemyDeath += OnEnemyDeath;
        _enemyIdle = new EnemyIdle(enemy, this, player);
        _enemyMove = new EnemyMove(enemy, this, player);
        _enemyAttack = new EnemyAttack(enemy, this, player);
        _enemyDeath = new EnemyDeath(enemy, this, player);
        CurrentState = _enemyIdle;
        TransitionTable = new Stack<EnemyState>();
        TransitionTable.Push(CurrentState);
        TransitionTable.First().Enter(_enemyIdle);
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        isHalted = true;
    }

    public void ChangeState(string newState)
    {
        if (isHalted) {

        }
        var previousState = CurrentState;
        CurrentState.Exit();
        // dont pop when you want to save a history of the previous state, for example when user is attacking they should continue the previous state
        // this means they should have an change state option that uses the "previous" word as the newState
        if (newState != nameof(EnemyAttack))
        {
            TransitionTable.Pop();
        }
        //only push for a new state if it is not "previous" there may be a reason for other keywords but for now its just the one.
        if (newState != "previous")
        {
            TransitionTable.Push(GetState(newState));
        }
        CurrentState = TransitionTable.First();
        CurrentState.Enter(previousState);
    }
    private EnemyState GetState(string state)
    {
        switch (state)
        {
            case nameof(EnemyIdle):
                return _enemyIdle;
            case nameof(EnemyMove):
                return _enemyMove;
            case nameof(EnemyAttack):
                return _enemyAttack;
            case nameof(EnemyDeath):
                return _enemyDeath;
            default:
                return _enemyIdle;
        }
    }
}