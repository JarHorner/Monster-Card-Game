using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedResultUI : MonoBehaviour
{

    [SerializeField] private TMP_Text resultText;

    void Start()
    {

        Hide();
    }

    public void DisplayMessage(bool maxCardsAdded)
    {
        if (!maxCardsAdded)
            resultText.text = "Card added!";
        else
            resultText.text = "Already have 5 cards!";

        Show();

        StartCoroutine(UpdateMessageOnTimer());
    }

    IEnumerator UpdateMessageOnTimer()
    {

        yield return new WaitForSeconds(1f);
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
