using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Phase
{
    Setup,
    Turn,
    Effects,
    Ending,
}

public class TimeTracker : MonoBehaviour
{
    public Phase currentPhase { get; private set; }
    public TMP_Text timeText;
    public TMP_Text currentTimeText;
    public float playerTurnTime;
    public float currentTurnTime;

    public TMP_Text turnText;
    public TMP_Text turnChangeText;

    public int playersTurn;

    void Start()
    {
        currentPhase = Phase.Setup;
    }

    public void StartFirstTurn(int firstTurnPlayer)
    {
        currentTimeText.text = playerTurnTime.ToString("0.00");

        playersTurn = firstTurnPlayer;
        turnText.text = $"Player {playersTurn} Turn";

        timeText.enabled = true;

         currentPhase = Phase.Turn;
    }

    public void TrackTime()
    {
        currentTurnTime -= Time.deltaTime;
        currentTimeText.text = currentTurnTime.ToString("0.00");

        if (currentTurnTime <= 0f)
        {
            PlayerOutOfTimeEndTurn();
        }
    }

    private void PlayerOutOfTimeEndTurn()
    {
        currentPhase = Phase.Ending;

        currentTurnTime = playerTurnTime;
        currentTimeText.text = currentTurnTime.ToString("0.00");

        if (playersTurn == 1)
            playersTurn = 2;
        else
            playersTurn = 1;
        
        turnText.text = $"Player {playersTurn} Turn";

        StartCoroutine(ShowChangeTurnText());
    }

    IEnumerator ShowChangeTurnText()
    {
        turnChangeText.text = $"Player {playersTurn} Turn";
        turnChangeText.enabled = true;

        yield return new WaitForSeconds(2f);

        turnChangeText.enabled = false;
        currentPhase = Phase.Turn;
    }

    public void EffectsOfMovePlayingOut()
    {
        currentPhase = Phase.Effects;
    }

    public void PlayerPlayCardEndTurn()
    {
        currentPhase = Phase.Ending;

        currentTurnTime = playerTurnTime;
        currentTimeText.text = currentTurnTime.ToString("0.00");

        if (playersTurn == 1)
            playersTurn = 2;
        else
            playersTurn = 1;
        
        turnText.text = $"Player {playersTurn} Turn";

        StartCoroutine(ShowChangeTurnText());
    }

}

