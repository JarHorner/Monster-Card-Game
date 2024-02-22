using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class CardGameManager : NetworkBehaviour
{
    public static CardGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        Turn, 
        Battle, 
        Ending, 
        GameOver,
    }

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

    private float playerTurnTimerMax = 90f;

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
                    state = State.Turn;
                }
                break;
            case State.Turn:
                playerTurnTimer -= Time.deltaTime;
                if (playerTurnTimer < 0f)
                {
                    state = State.Battle;
                }
                break;
            case State.Ending:
                playerTurnTimer = playerTurnTimerMax;
                state = State.Turn;
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsPlayerTurn()
    {
        return state == State.Turn;
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
