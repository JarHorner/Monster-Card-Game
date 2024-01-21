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
    public TMP_Text turnChangeText;

    public bool playerTurnOver { get; private set; }
    public int playersTurn;

    void Start()
    {

    }

    public void StartFirstTurn(int firstTurnPlayer)
    {
        timeText.text = playerTurnTime.ToString("0.00");

        playersTurn = firstTurnPlayer;
        turnText.text = $"Player {playersTurn} Turn";

        timeRunning = true;
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

        if (playersTurn == 1)
            playersTurn = 2;
        else
            playersTurn = 1;
        
        turnText.text = $"Player {playersTurn} Turn";

        playerTurnOver = true;

        StartCoroutine(ShowChangeTurnText());
    }

    IEnumerator ShowChangeTurnText()
    {
        turnChangeText.text = $"Player {playersTurn} Turn";
        turnChangeText.enabled = true;

        yield return new WaitForSeconds(2f);

        turnChangeText.enabled = false;
        timeRunning = true;
        playerTurnOver = false;
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

