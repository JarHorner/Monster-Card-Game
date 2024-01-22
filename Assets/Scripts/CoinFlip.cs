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

    // chooses 1, deactivates choice panel commencing coin flip
    public void ChooseHeads()
    {
        sideChosen = 1;
        isSideChosen = true;

        choicePanel.SetActive(false);

        FlipCoin();
    }

    // chooses 2, deactivates choice panel commencing coin flip
    public void ChooseTails()
    {
        sideChosen = 2;
        isSideChosen = true;

        choicePanel.SetActive(false);

        FlipCoin();
    }

    // Makes a 50/50 choice on the winner, starts the flip coin animation and changes text based on the decision. 
    public void FlipCoin()
    {
        // chooses between 1 & 2
        decision = Random.Range(1, 3);

        flipScript.Flip(decision);

        if (decision == 1)
        {
            flipWinnerText.text = "You Chose Heads!";
        }
        else if (decision == 2)
        {
            flipWinnerText.text = "You Chose Tails!";
        }

        StartCoroutine(DisplayWinner(decision));
    }

    // displays the results of the flip, timed when the coin flip anim is finished, waits, the disables all.
    IEnumerator DisplayWinner(int decision)
    {
        flipWinnerText.enabled = true;

        yield return new WaitForSeconds(2.5f);

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

        yield return new WaitForSeconds(2f);

        flipWinnerText.enabled = false;
        flipScript.spriteRenderer.enabled = false;

        flipComplete = true;
    }

    
}
