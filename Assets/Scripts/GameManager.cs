using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool gameStarted = false;
    public TimeTracker timeTracker;
    public CoinFlip coinFlip;

    // player 1 variables
    public GameObject player1Hand;
    public List<GameObject> player1Cards = new List<GameObject>();
    public Transform[] player1CardSlots;

    // player 2 variables
    public GameObject player2Hand;
    public List<GameObject> player2Cards = new List<GameObject>();
    public Transform[] player2CardSlots;

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

            gameStarted = true;
        }

        if (coinFlip.flipComplete && timeTracker.timeRunning)
        {
            timeTracker.TrackTime();
        }

        if (timeTracker.playerTurnOver)
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
            GameObject randCard = player1Cards[Random.Range(0, player1Cards.Count)];
            Instantiate(randCard, player1CardSlots[i]);       
        }

        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = player2Cards[Random.Range(0, player2Cards.Count)];
            Instantiate(randCard, player2CardSlots[i]);       
        }
    }

    public void setCard(GameObject position, Card cardSelected)
    {

    }

    public void removeCardFromHand()
    {

    }
}
