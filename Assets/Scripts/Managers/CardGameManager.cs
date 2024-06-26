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
        Player1Turn,
        Player2Turn,
        Battle,
        Ending,
        GameOver,
        Standby,
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
    [SyncVar, SerializeField] public int playersReady = 0;

    [SyncVar] private float countdownToStartTimer = 0f;
    [SyncVar] private float playerTurnTimer = 0f;

    private float playerTurnTimerMax = 60f; //60
    private float countdownToStartTimerMax = 0f; //3

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

                    string turnText = "PLAYER 1 \n TURN!";
                    StartCoroutine(SpawnChangeTurnText(turnText));

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
                    string turnText = "PLAYER 2 \n TURN!";
                    StartCoroutine(SpawnChangeTurnText(turnText));
                    state = State.Player2Turn;
                }
                else
                {
                    string turnText = "PLAYER 1 \n TURN!";
                    StartCoroutine(SpawnChangeTurnText(turnText));
                    state = State.Player1Turn;
                }
                break;
            case State.GameOver:
                CardGameUIManager.Instance.SpawnEndGameUI();

                state = State.Standby;
                break;
            case State.Standby:

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

    IEnumerator SpawnChangeTurnText(string turnText)
    {
        CardGameUIManager.Instance.SpawnChangeTurnUI(turnText);

        yield return new WaitForSeconds(1f);

        CardGameUIManager.Instance.RpcDestoryChangeTurnUI();
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

    public void UpdateCardOwnerID(int newID , Card updatedCard)
    {
        Debug.Log("The new ID will be: " + newID + " and the card being updated is: " + updatedCard.name);

        RpcSyncCardOwnerID(newID, updatedCard);
    }

    [ClientRpc]
    public void RpcSyncCardOwnerID(int newID, Card updatedCard)
    {
        updatedCard.SetCardOwnerID(newID);
    }

    public void AddPlayersReady()
    {
        Debug.Log("Adding readied player");
        playersReady++;
    }

    public int GetPlayersReady()
    {
        return playersReady;
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
