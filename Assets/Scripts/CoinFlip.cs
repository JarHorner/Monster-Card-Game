using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinFlip : MonoBehaviour
{
    public TMP_Text flipWinnerText;
    public GameObject choicePanel;

    public bool isSideChosen;
    private int sideChosen;

    public bool flipComplete;

    public void ChooseHeads()
    {
        sideChosen = 1;
        isSideChosen = true;
    }

    public void ChooseTails()
    {
        sideChosen = 2;
        isSideChosen = true;
    }

    public int FlipCoin()
    {
        // chooses between 1 & 2
        int decision = Random.Range(1, 3);
        int playerGoingFirst = 0;

        if (decision == 1)
        {
            if (sideChosen == decision)
            {
                flipWinnerText.text = $"Heads is chosen, You go first!";
                playerGoingFirst = 1;
            }
            else
            {
                flipWinnerText.text = $"Heads is chosen, You go second!";
                playerGoingFirst = 2;
            }
        }
        else if (decision == 2)
        {
            if (sideChosen == decision)
            {
                flipWinnerText.text = $"Tails is chosen, You go first!";
                playerGoingFirst = 1;
            }
            else
            {
                flipWinnerText.text = $"Tails is chosen, You go second!";
                playerGoingFirst = 2;
            }
        }

        StartCoroutine(DisplayWinner(decision));

        return playerGoingFirst;
    }

    IEnumerator DisplayWinner(int decision)
    {
        choicePanel.SetActive(false);
        flipWinnerText.enabled = true;

        yield return new WaitForSeconds(2f);

        flipWinnerText.enabled = false;

        flipComplete = true;
    }

    
}
