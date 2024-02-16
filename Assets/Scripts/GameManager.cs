using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    //public event EventHandler OnLocalGamePaused;
    //public event EventHandler OnLocalGameUnpaused;
   // public event EventHandler OnMultiplayerGamePaused;
    // public event EventHandler OnMultiplayerGameUnpaused;
    //public event EventHandler OnLocalPlayerReadyChanged;

    // enum state machine to help guide the gameplay
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        Turn, // state for each players turn
        Battle, // state between Turn and Ending, and allows for effects to play before ending turn
        Ending, // state after Battle, wrapping up and preparing for Turn state
        GameOver,
    }

    [SerializeField] private Transform playerPrefab;

    private NetworkVariable<State> state = new NetworkVariable<State>(State.CountdownToStart);
    //private bool isLocalPlayerReady;

    private NetworkVariable<float> countdownToStartTimer = new NetworkVariable<float>(3f);
    private NetworkVariable<float> playerTurnTimer = new NetworkVariable<float>(0f);
    private float playerTurnTimerMax = 90f;

   // private bool autoTestGamePauseState;


    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

    }

    public override void OnNetworkSpawn()
    {
        state.OnValueChanged += State_OnValueChanged;

        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += NetworkManager_OnLoadEventCompleted;
        }
    }

    private void State_OnValueChanged(State previousValue, State newValue)
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void NetworkManager_OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab);
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);

            //foreach (CardDisplay card in Player.LocalInstance.GetPlayerCardDisplays())
            //{
            //    GameObject newCard = Instantiate(card.gameObject);
            //    newCard.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
            //}
        }
    }


    void Update()
    {
        if (!IsServer) return;

        switch (state.Value)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer.Value -= Time.deltaTime;
                if (countdownToStartTimer.Value <= 0f)
                {
                    state.Value = State.Turn;
                    playerTurnTimer.Value = playerTurnTimerMax;
                }
                break;
            case State.Turn:
                playerTurnTimer.Value -= Time.deltaTime;
                if (playerTurnTimer.Value < 0f)
                {
                    state.Value = State.Battle;
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsPlayerTurn()
    {
        return state.Value == State.Turn;
    }

    public float GetPlayerTurnTimer()
    {
        return playerTurnTimer.Value;
    }

    public bool IsCountdownToStartActive()
    {
        return state.Value == State.CountdownToStart;
    }


    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer.Value;
    }
    public bool IsGameOver()
    {
        return state.Value == State.GameOver;
    }


}
      
