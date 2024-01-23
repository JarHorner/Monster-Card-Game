using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public GameManager gameManager;

    public TMP_Text title;
    public GameObject panel;

    // begins the resetting of all values in the game to play a new game
    public void Replay() 
    {
        Debug.Log("Replay Game");
        gameManager.ResetGame();
    }

    // exits out of the game
    public void Quit() 
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
