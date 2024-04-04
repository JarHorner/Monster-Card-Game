using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
using System;

public class EndGameUI : NetworkBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private Button PlayAgainButton;
    [SerializeField] private Button MainMenuButton;

    private int[] playersCardCounts = new int[2];

    void Start()
    {
        PlayAgainButton.interactable = true;
        MainMenuButton.interactable = true;

        PlayAgainButton.onClick.AddListener(PlayAgainOnClick);
        MainMenuButton.onClick.AddListener(MainMenuOnClick);

        DetermineWinner(DropZone.Instance.GetCards());
    }

    public void DetermineWinner(List<Card> cards)
    {
        int playerCardCount = 0;
        int enemyCardCount = 0;

        foreach (Card card in cards)
        {
            if (card.GetCardOwnerID() == 1)
            {
                playerCardCount += 1;
            }
            else
            {
                enemyCardCount += 1;
            }
        }

        RpcShowResultsOfGame(playerCardCount, enemyCardCount);
    }

    [ClientRpc]
    private void RpcShowResultsOfGame(int playerCardCount, int enemyCardCount)
    {
        if (isServer)
        {
            if (playerCardCount > enemyCardCount)
            {
                winnerText.text = "You Win!" + "\n" + "You Had " + playerCardCount + " Cards" + "\n" + "Enemy Had " + enemyCardCount + " Cards";
            }
            else if (playerCardCount < enemyCardCount)
            {
                winnerText.text = "You Lose!" + "\n" + "You Had " + playerCardCount + " Cards" + "\n" + "Enemy Had " + enemyCardCount + " Cards";
            }
            else
            {
                throw new ArgumentException("Card count should never be equal. Check for calculation errors.");
            }
        }
        else
        {
            if (enemyCardCount > playerCardCount)
            {
                winnerText.text = "You Win!" + "\n" + "You Had " + enemyCardCount + " Cards" + "\n" + "Enemy Had " + playerCardCount + " Cards";
            }
            else if (enemyCardCount < playerCardCount)
            {
                winnerText.text = "You Lose!" + "\n" + "You Had " + enemyCardCount + " Cards" + "\n" + "Enemy Had " + playerCardCount + " Cards";
            }
            else
            {
                throw new ArgumentException("Card count should never be equal. Check for calculation errors.");
            }
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

    public void SetWinnerText(string newText)
    {
        winnerText.text = newText;
    }
}
