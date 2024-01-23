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
        gameManager.ResetGame();
    }

    // exits out of the game
    public void Quit() 
    {
        Application.Quit();
    }
}
