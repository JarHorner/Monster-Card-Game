using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject endGameMenuPanel;

    private bool gameStarted = false;
    public TimeTracker timeTracker;
    public CoinFlip coinFlip;
    public GameBoard gameBoard;

    private int firstTurnPlayer;

    // player 1 variables
    public GameObject player1Hand;
    public List<GameObject> player1CardSlots;

    // player 2 variables
    public GameObject player2Hand;
    public List<GameObject> player2CardSlots;

    public List<GameObject> monsterCards = new List<GameObject>();
    public int maxNumCardsInHand;

    void Start()
    {
        gameStarted = false;
    }

    void Update()
    {
        // if game hasnt started but coin flip sequence is complete, sets up the game
        if (!gameStarted && coinFlip.flipComplete)
        {
            firstTurnPlayer = coinFlip.decision;
            timeTracker.StartFirstTurn(firstTurnPlayer);

            DrawCards();

            SwapHands();

            gameBoard.ActivateBoard();
            gameStarted = true;
        }

        // once coin flip sequence is complete and a Turn phase begins, the timer starts.
        // Doing this also stops timer when not in Turn phase
        if (coinFlip.flipComplete && timeTracker.currentPhase == Phase.Turn)
        {
            timeTracker.TrackTime();
        }

        // if in Ending phase, player hands swap, so other player can play
        if (timeTracker.currentPhase == Phase.Ending)
        {
            if (player1CardSlots.Count == 0 || player2CardSlots.Count == 0)
            {
                EndGame();
            }
            SwapHands();
        }
    }

    // swaps hands by enabling and disabling the different player hands
    private void SwapHands()
    {
        if( timeTracker.playersTurn == 1)
        {
            player1Hand.SetActive(true);
            player2Hand.SetActive(false);
        }
        else
        {
            player1Hand.SetActive(false);
            player2Hand.SetActive(true);
        }
    }

    // randomly grabs different monster cards from an arry and instantiates them in each players hands
    public void DrawCards()
    {
        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = monsterCards[Random.Range(0, monsterCards.Count)];
            randCard.GetComponent<CardDisplay>().playerOwner = 1;
            Instantiate(randCard, player1CardSlots[i].transform);       
        }

        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = monsterCards[Random.Range(0, monsterCards.Count)];
            randCard.GetComponent<CardDisplay>().playerOwner = 2;
            Instantiate(randCard, player2CardSlots[i].transform);       
        }
    }

    // sets the card up using method of other classes to put it on the board and battle
    public void setCard(Transform position, GameObject cardSelected)
    {
        if (transform.childCount == 0)
        {
            timeTracker.BattlePlayingOut();

            gameBoard.AddCardToPosition(position, cardSelected);

            StartCoroutine(removeCardFromHand(cardSelected));

            gameBoard.CommenceBattle();
        }
    }

    // finds the card selected and place on the board, and destorys it from the players hand, then ends the turn
    IEnumerator removeCardFromHand(GameObject cardSelected)
    {
        GameObject selectedSlot = cardSelected.transform.parent.gameObject;

        if (timeTracker.playersTurn == 1)
        {
            for(int i = 0; i < player1CardSlots.Count; i++)
            {
                if (selectedSlot == player1CardSlots[i]) 
                {
                    Debug.Log(selectedSlot.name + " = " + player1CardSlots[i].name);
                    Destroy(selectedSlot.transform.GetChild(0).gameObject);
                    player1CardSlots.RemoveAt(i);
                    yield return null;
                }
            }
        }
        else
        {
            for(int i = 0; i < player2CardSlots.Count; i++)
            {
                if (selectedSlot == player2CardSlots[i]) 
                {
                    Debug.Log(selectedSlot.name + " = " + player2CardSlots[i].name);
                    Destroy(selectedSlot.transform.GetChild(0).gameObject);
                    player2CardSlots.RemoveAt(i);
                    yield return null;
                }
            }
        }

        yield return new WaitForSeconds(2f);

        timeTracker.PlayerPlayCardEndTurn();
    }

    // ends the game after the board is full, by calculating a winner and opening end game panel
    private void EndGame()
    {
        Debug.Log("Ending Game!");
        
        // Calculate who won

        //pull up menu for exiting to replaying
        endGameMenuPanel.SetActive(true);
    }

    // resets all values needed to get the game to the beginning state
    public void ResetGame()
    {
        endGameMenuPanel.SetActive(false);

        coinFlip.EnableCoinFlipMenu();

        gameStarted = false;

        timeTracker.ResetTimer();

        ResetPlayerHands();
    }

    // since the card slots for each player was deleted, adds them back for a fresh game.
    private void ResetPlayerHands()
    {
        foreach (Transform child in player1Hand.transform)
        {
            GameObject childGameObject = child.gameObject;

            player1CardSlots.Add(childGameObject);
        }   

        foreach (Transform child in player2Hand.transform)
        {
            GameObject childGameObject = child.gameObject;

            player2CardSlots.Add(childGameObject);
        }  
    }
}
