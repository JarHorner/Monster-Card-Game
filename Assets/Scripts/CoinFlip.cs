using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinFlip : MonoBehaviour
{
    public FlipScript flipScript;

    public TMP_Text flipWinnerText;
    public GameObject choicePanel;

    public bool isSideChosen;
    public int sideChosen { get; private set; }
    public int decision { get; private set; }
    
    public bool flipComplete;

    public void ChooseHeads()
    {
        sideChosen = 1;
        isSideChosen = true;

        choicePanel.SetActive(false);

        FlipCoin();
    }

    public void ChooseTails()
    {
        sideChosen = 2;
        isSideChosen = true;

        choicePanel.SetActive(false);

        FlipCoin();
    }

    public void FlipCoin()
    {
        // chooses between 1 & 2
        decision = Random.Range(1, 3);

        flipScript.Flip(decision);

        if (decision == 1)
        {
            if (sideChosen == decision)
            {
                flipWinnerText.text = "Heads! You Go First";
            }
            else
            {
                flipWinnerText.text = "Heads! You Go Second";
            }
        }
        else if (decision == 2)
        {
            if (sideChosen == decision)
            {
                flipWinnerText.text = "Tails! You Go First";
            }
            else
            {
                flipWinnerText.text = "Tails! You Go Second";
            }
        }

        StartCoroutine(DisplayWinner(decision));
    }

    IEnumerator DisplayWinner(int decision)
    {
        choicePanel.SetActive(false);

        yield return new WaitForSeconds(3f);

        flipWinnerText.enabled = true;

        yield return new WaitForSeconds(2f);

        flipWinnerText.enabled = false;
        flipScript.spriteRenderer.enabled = false;

        flipComplete = true;
    }

    
}
