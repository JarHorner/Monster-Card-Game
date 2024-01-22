using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject board;
    public List<GameObject> boardPositions = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // enables the entire board
    public void ActivateBoard()
    {
        board.SetActive(true);

        for (int i = 0; i < boardPositions.Count; i++)
        {
            boardPositions[i].SetActive(true);      
        }
    }

    public void AddCardToPosition(GameObject chosenPosition)
    {

    }
}
