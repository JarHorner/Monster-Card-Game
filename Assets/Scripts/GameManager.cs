using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool gameStarted = false;
    public TimeTracker timeTracker;
    public CoinFlip coinFlip;
    public GameBoard gameBoard;

    // player 1 variables
    public GameObject player1Hand;
    public GameObject[] player1CardSlots;

    // player 2 variables
    public GameObject player2Hand;
    public GameObject[] player2CardSlots;

    public List<GameObject> monsterCards = new List<GameObject>();
    public int maxNumCardsInHand;

    void Start()
    {
        gameStarted = false;
    }

    void Update()
    {
        if (!gameStarted && coinFlip.isSideChosen)
        {
            int firstTurnPlayer = coinFlip.FlipCoin();
            timeTracker.StartFirstTurn(firstTurnPlayer);

            DrawCards();

            SwapHands();

            gameBoard.ActivateBoard();
            gameStarted = true;
        }

        if (coinFlip.flipComplete && timeTracker.currentPhase == Phase.Turn)
        {
            timeTracker.TrackTime();
        }

        if (timeTracker.currentPhase == Phase.Ending)
        {
            SwapHands();
        }
    }

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

    public void DrawCards()
    {
        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = monsterCards[Random.Range(0, monsterCards.Count)];
            Instantiate(randCard, player1CardSlots[i].transform);       
        }

        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = monsterCards[Random.Range(0, monsterCards.Count)];
            Instantiate(randCard, player2CardSlots[i].transform);       
        }
    }

    public void setCard(GameObject position, GameObject cardSelected)
    {
        removeCardFromHand(cardSelected);

        timeTracker.PlayerPlayCardEndTurn();
    }

    private void removeCardFromHand(GameObject cardSelected)
    {
        GameObject slot = cardSelected.transform.parent.gameObject;
        Debug.Log(slot);
        
    }
}
