using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedResultUI : MonoBehaviour
{

    [SerializeField] private TMP_Text selectText;
    [SerializeField] private TMP_Text connectText;

    void Start()
    {

        Hide();
    }

    public void DisplayConnectMessage(bool maxCardsAdded)
    {
        if (!maxCardsAdded)
            connectText.text = "You need 5 cards!";

        Show();

        StartCoroutine(UpdateMessageOnTimer());
    }

    public void DisplaySelectMessage(bool maxCardsAdded)
    {
        if (!maxCardsAdded)
            selectText.text = "Card added!";
        else
            selectText.text = "Already have 5 cards!";

        Show();

        StartCoroutine(UpdateMessageOnTimer());
    }

    IEnumerator UpdateMessageOnTimer()
    {

        yield return new WaitForSeconds(1f);

        selectText.text = "";
        connectText.text = "";
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
