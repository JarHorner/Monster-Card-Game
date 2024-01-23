using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public BattleManager battleManager;

    public GameObject board;
    public GameObject[] boardPositions;

    public CardDisplay[] placedCards;


    public Vector3 baseCardOnBoardScale;

    void Start() 
    {
        placedCards = new CardDisplay[9];
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
    public void AddCardToPosition(Transform chosenPosition, GameObject cardSelected)
    {
        GameObject placedCard = Instantiate(cardSelected, chosenPosition);
        placedCard.name = "BoardCard";
        placedCard.GetComponent<CardDisplay>().baseScale = baseCardOnBoardScale;
        placedCard.transform.localScale = baseCardOnBoardScale;
        placedCard.GetComponent<CardDisplay>().selectedBorder.enabled = false;
    }

    public GameObject RandomEmptyPosition()
    {
        List<GameObject> emptyPositions = new List<GameObject>();

        for (int i = 0; i < boardPositions.Length; i++)
        {
            if (boardPositions[i].transform.childCount == 0)
            {
                emptyPositions.Add(boardPositions[i]);
            }    
        }

        int randomIndex = Random.Range(0, emptyPositions.Count);

        return emptyPositions[randomIndex];
    }

    public void ResetGameBoard()
    {
        placedCards = new CardDisplay[9];

        for (int i = 0; i < boardPositions.Length; i++)
        {
            Destroy(boardPositions[i].transform.GetChild(0).gameObject);
            boardPositions[i].SetActive(false);      
        }

        board.SetActive(false);
    }

    public void CommenceBattle()
    {

    }
}
