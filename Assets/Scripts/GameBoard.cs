using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject board;
    public List<GameObject> boardPositions = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBoard()
    {
        board.SetActive(true);

        for (int i = 0; i < boardPositions.Count; i++)
        {
            boardPositions[i].SetActive(true);      
        }
    }
}
