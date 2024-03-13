using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class CardGameManager : NetworkBehaviour
{
    public enum State
    {
        WaitingToStart,
        ReadyToStart,
        CountdownToStart,
        EndTurn,
        Player1Turn,
        Player2Turn,
        Battle,
        Ending,
        GameOver,
    }

    public static CardGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private int lastPlayersTurn; //can be 1 or 2

    [SyncVar] private State _syncedState;
    public State state
    { 
        get
        {
            return _syncedState;
        }
        set
        {
            State previousStateValue = _syncedState;
            _syncedState = value;
            State_OnValueChanged(previousStateValue, value);
        }
    }

    [SyncVar] private float countdownToStartTimer = 0f;
    [SyncVar] private float playerTurnTimer = 0f;

    private float playerTurnTimerMax = 10f;
    private float countdownToStartTimerMax = 3f;

    private void Awake()
    {
        Instance = this;
    }
    public override void OnStartClient()
    {
        state = State.WaitingToStart;
    }

    private void State_OnValueChanged(State previousValue, State newValue)
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        if (!isServer) return;

        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.ReadyToStart:
                countdownToStartTimer = countdownToStartTimerMax;
                state = State.CountdownToStart;
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    playerTurnTimer = playerTurnTimerMax;
                    state = State.Player1Turn;
                }
                break;
            case State.Player1Turn:
                playerTurnTimer -= Time.deltaTime;
                if (playerTurnTimer < 0f)
                {
                    lastPlayersTurn = 1;
                    state = State.Battle;
                }
                break;
            case State.Player2Turn:
                playerTurnTimer -= Time.deltaTime;
                if (playerTurnTimer < 0f)
                {
                    lastPlayersTurn = 2;
                    state = State.Battle;
                }
                break;
            case State.Battle:
                // Battle
                state = State.Ending;
                break;
            case State.Ending:
                playerTurnTimer = playerTurnTimerMax;
                if (lastPlayersTurn == 1)
                {
                    Debug.Log("IT IS PLAYER 2 TURN!");
                    state = State.Player2Turn;
                }
                else
                {
                    Debug.Log("IT IS PLAYER 1 TURN!");
                    state = State.Player1Turn;
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsPlayersTurn(int playerNumber)
    {
        Debug.Log(playerNumber + " is trying to drag on " + state + " state");

        if (state == State.Player1Turn)
        {
            if (playerNumber == 1)
                return true;
            else
                return false;
        }
        else if (state == State.Player2Turn)
        {
            if (playerNumber == 2)
                return true;
            else
                return false;
        }
        else
        {
            Debug.LogError("IsPlayersTurn should not get here. This means a players number is being assigned incorrectly.");
            return false;
        }
    }

    public bool IsPlayerTurn()
    {
        return state == State.Player1Turn || state == State.Player2Turn;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public void AllPlayersReady()
    {
        state = State.ReadyToStart;
    }

    public float GetPlayerTurnTimer()
    {
        return playerTurnTimer;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
}
