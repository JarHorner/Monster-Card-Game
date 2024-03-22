using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;

public class EndGameUI : NetworkBehaviour
{
    public GameObject endGamePanel;
    public TMP_Text winnerText;
    public Button PlayAgainButton;
    public Button MainMenuButton;

    void Start()
    {
        PlayAgainButton.interactable = true;
        MainMenuButton.interactable = true;

        PlayAgainButton.onClick.AddListener(PlayAgainOnClick);
        MainMenuButton.onClick.AddListener(MainMenuOnClick);
    }

    void Update()
    {
        if (CardGameManager.Instance.state == CardGameManager.State.Ending)
        {

        }
    }

    private void PlayAgainOnClick()
    {
        Debug.Log("Play Again!");
    }

    private void MainMenuOnClick()
    {
        Debug.Log("Main Menu!");
    }
}
