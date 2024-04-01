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
        AllPlayersReady,
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

    [SyncVar, SerializeField] private State _syncedState;
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
            RpcState_OnStateChanged(previousStateValue, value);
        }
    }

    [SyncVar] private int lastPlayersTurn; //can be 1 or 2

    [SyncVar] private float countdownToStartTimer = 0f;
    [SyncVar] private float playerTurnTimer = 0f;

    private float playerTurnTimerMax = 60f; //60
    private float countdownToStartTimerMax = 1f; //3

    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject CountdownTimerUIObject;

    private void Awake()
    {
        Instance = this;
    }
    public override void OnStartClient()
    {
        state = State.WaitingToStart;

    }

    [ClientRpc]
    private void RpcState_OnStateChanged(State previousValue, State newValue)
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
            case State.AllPlayersReady:
                countdownToStartTimer = countdownToStartTimerMax;
                CardGameUIManager.Instance.SpawnCountdownTimerUI();
                state = State.CountdownToStart;
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    CardGameUIManager.Instance.RpcDestoryCountdownTimerUI();

                    playerTurnTimer = playerTurnTimerMax;

                    CardGameUIManager.Instance.SpawnTurnTimerUI();

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
                    state = State.Ending;
                }
                break;
            case State.Battle:
                CardsBattle();
                if (DropZone.Instance.BattlePlayedOut())
                {
                    state = State.Ending;
                }
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
                CardGameUIManager.Instance.SpawnEndGameUI();

                state = State.WaitingToStart;
                break;
        }
    }

    public bool IsPlayersTurn(int playerId)
    {
        Debug.Log(playerId + " is trying to drag on " + state + " state");

        if (state == State.Player1Turn)
        {
            if (playerId == 1)
                return true;
            else
                return false;
        }
        else if (state == State.Player2Turn)
        {
            if (playerId == 2)
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

    [ClientRpc]
    public void RpcAssignPlayerId(PlayerManager playerManager, int playerId)
    {
        playerManager.SetPlayerId(playerId);

        Debug.Log("Player " + playerId + " joined the game.");
    }

    public void StartBattle(int playerId)
    {
        Debug.Log("Starting Battle, " + playerId);

        lastPlayersTurn = playerId;
        state = State.Battle;
    }

    private void CardsBattle()
    {
        DropZone.Instance.BattleCardsAlgorithm();
    }

    public bool IsPlayerTurn()
    {
        return state == State.Player1Turn || state == State.Player2Turn;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public void SetStateEndGame()
    {
        state = State.GameOver;
    }

    public void SetStateAllPlayersReady()
    {
        state = State.AllPlayersReady;
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
