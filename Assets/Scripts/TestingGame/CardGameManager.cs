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

    [SyncVar] private float countdownToStartTimer = 3f;
    [SyncVar] private float playerTurnTimer = 0f;

    private float playerTurnTimerMax = 90f;
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
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    playerTurnTimer = playerTurnTimerMax;
                    state = State.EndTurn;
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
                    state = State.Player2Turn;
                }
                else
                {
                    state = State.Player1Turn;
                }
                break;
            case State.GameOver:
                break;
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

    public void SetStateCountdownToStartActive()
    {
        state = State.CountdownToStart;
    }

    public float GetPlayerTurnTimer()
    {
        return playerTurnTimer;
    }
}
