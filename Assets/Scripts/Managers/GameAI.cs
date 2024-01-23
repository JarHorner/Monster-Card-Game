using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAI : MonoBehaviour
{
    public TimeTracker timeTracker;

    private bool aiTurnStarted;

    // void Start()
    // {
    //     aiTurnStarted = false;
    // }

    // void Update()
    // {
    //     if (timeTracker.playerTurnOver && !aiTurnStarted)
    //     {
    //         aiTurnStarted = true;
    //         StartCoroutine(AITurn());
    //     }
    // }

    // IEnumerator AITurn()
    // {
    //     Debug.Log("AI Turn! wait 2 seconds");

    //     yield return new WaitForSeconds(2f);

    //     aiTurnStarted = false;
    //     timeTracker.AiEndTurn();
    // }

}
