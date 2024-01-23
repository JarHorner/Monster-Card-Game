using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public BattleManager battleManager;

    public GameObject board;
    public GameObject[] boardPositions = new GameObject[9];
    public CardDisplay[] placedCards = new CardDisplay[9];

    public Vector3 baseCardOnBoardScale;

    void Start() 
    {
    
    
    }

    // enables the entire board
    public void ActivateBoard()
    {
        board.SetActive(true);

        for (int i = 0; i < boardPositions.Length; i++)
        {
            boardPositions[i].SetActive(true);      
        }
    }

    // instantiates card only the board as a child of the position
    public void AddCardToPosition(GameObject chosenPosition, GameObject cardSelected)
    {
        if (chosenPosition.transform.childCount == 0)
        {
            GameObject placedCard = Instantiate(cardSelected, chosenPosition.transform);
            placedCard.name = "BoardCard";
            placedCard.GetComponent<CardDisplay>().baseScale = baseCardOnBoardScale;
            placedCard.transform.localScale = baseCardOnBoardScale;
            placedCard.GetComponent<CardDisplay>().selectedBorder.enabled = false;


        }
    }

    public void CommenceBattle()
    {

    }
}
