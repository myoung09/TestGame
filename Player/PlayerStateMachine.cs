using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public class PlayerStateMachine
{
    public static float Direction = 1f;


    public PlayerState CurrentState;
    public Stack<PlayerState> TransitionTable;


    private Player _player;
    private PlayerIdle _playerIdle;
    private PlayerMove _playerMove;
    private PlayerJump _playerJump;
    private PlayerFalling _playerFalling;
    private PlayerMedAttack _playerMedAttack;

    //initialize
    public PlayerStateMachine(Player player)
    {
        _player = player;
        _playerIdle = new PlayerIdle(player, this);
        _playerMove = new PlayerMove(player, this);
        _playerJump = new PlayerJump(player, this);
        _playerFalling = new PlayerFalling(player, this);
        _playerMedAttack = new PlayerMedAttack(player, this);
        CurrentState = _playerIdle;
        TransitionTable = new Stack<PlayerState>();
        TransitionTable.Push(CurrentState);
    }

    public void ChangeState(string newState)
    {
        var previousState = CurrentState;
        CurrentState.Exit();
        // dont pop when you want to save a history of the previous state, for example when user is attacking they should continue the previous state
        // this means they should have an change state option that uses the "previous" word as the newState
        if (newState != nameof(PlayerMedAttack))
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
    private PlayerState GetState(string state)
    {
        switch (state)
        {
            case nameof(PlayerIdle):
                return _playerIdle;
            case nameof(PlayerMove):
                return _playerMove;
            case nameof(PlayerJump):
                return _playerJump;
            case nameof(PlayerFalling):
                return _playerFalling;
            case nameof(PlayerMedAttack):
                return _playerMedAttack;
            default:
                return _playerIdle;
        }
    }
}