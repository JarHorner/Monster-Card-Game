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

    private int firstTurnPlayer;

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


    public void setCard(GameObject position, GameObject cardSelected)
    {
        if (position.transform.childCount == 0)
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
            foreach(GameObject slot in player1CardSlots)
            {
                if (selectedSlot == slot) 
                {
                    Debug.Log(selectedSlot.name + " = " + slot.name);
                    Destroy(selectedSlot.transform.GetChild(0).gameObject);
                }
            }
        }
        else
        {
            foreach(GameObject slot in player2CardSlots)
            {
                if (selectedSlot == slot) 
                {
                    Debug.Log(selectedSlot.name + " = " + slot.name);
                    Destroy(selectedSlot.transform.GetChild(0).gameObject);
                }
            }
        }

        yield return new WaitForSeconds(2f);

        timeTracker.PlayerPlayCardEndTurn();
    }
}
