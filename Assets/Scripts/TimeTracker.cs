using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTracker : MonoBehaviour
{

    public TMP_Text timeText;
    public float playerTurnTime;
    public float currentTurnTime;
    public bool timeRunning { get; private set; }

    public TMP_Text turnText;

    public bool playerTurnOver { get; private set; }

    void Start()
    {
        timeText.text = playerTurnTime.ToString("0.00");

        turnText.text = "Player1 Turn";
        timeRunning = true;
        playerTurnOver = false;
    }

    public void TrackTime()
    {
        currentTurnTime -= Time.deltaTime;
        timeText.text = currentTurnTime.ToString("0.00");

        if (currentTurnTime <= 0f)
        {
            timeRunning = false;
            PlayerEndTurn();
        }
    }

    private void PlayerEndTurn()
    {
        currentTurnTime = playerTurnTime;
        timeText.text = currentTurnTime.ToString("0.00");

        turnText.text = "Player2 Turn";

        playerTurnOver = true;
    }
    
    // public void AiEndTurn()
    // {
    //     currentTurnTime = playerTurnTime;
    //     timeText.text = currentTurnTime.ToString("0.00");

    //     turnText.text = "Player Turn";

    //     timeRunning = true;
    //     playerTurnOver = false;
    // }

}

