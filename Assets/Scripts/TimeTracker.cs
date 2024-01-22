using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// enum state machine to help guide the gameplay
public enum Phase
{
    Setup, // state  before Turn, and the beginning state
    Turn, // state for each players turn
    Effects, // state between Turn and Ending, and allows for effects to play before ending turn
    Ending, // state after Effects, wrapping up and preparing for Turn state
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

    // enables text, and ensures the state of the game is in the Turn phase
    public void StartFirstTurn(int firstTurnPlayer)
    {
        currentTimeText.text = playerTurnTime.ToString("0.00");

        playersTurn = firstTurnPlayer;
        turnText.text = $"Player {playersTurn} Turn";

        timeText.enabled = true;

        currentPhase = Phase.Turn;
    }

    // Used by the Gamemanager to constaly update a timer for a turn
    // if timer runs to 0, the turn will end.
    public void TrackTime()
    {
        currentTurnTime -= Time.deltaTime;
        currentTimeText.text = currentTurnTime.ToString("0.00");

        if (currentTurnTime <= 0f)
        {
            PlayerOutOfTimeEndTurn();
        }
    }

    // sets the current phase to Ending, and changes text indicating the ending of the turn
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

    // shows text indicating the ending of the turn, then initiates the Turn phase
    IEnumerator ShowChangeTurnText()
    {
        turnChangeText.text = $"Player {playersTurn} Turn";
        turnChangeText.enabled = true;

        yield return new WaitForSeconds(2f);

        turnChangeText.enabled = false;
        currentPhase = Phase.Turn;
    }

    // used to change to the Effects phase, when a card has been place in a turn.
    public void EffectsOfMovePlayingOut()
    {
        currentPhase = Phase.Effects;
    }

    // a seperate function for ending a turn for future changes needed when ending turn by playing
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

