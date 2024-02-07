using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

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

    private State state;


    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 0f;
    private float gamePlayingTimerMax = 90f;

    private bool isGamePaused = false;


    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;
    }


    void Start()
    {

    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.Turn;
                    gamePlayingTimer = gamePlayingTimerMax;
                }
                break;
            case State.Turn:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.Battle;
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.Turn;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }


}
      
