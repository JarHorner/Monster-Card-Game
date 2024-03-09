using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartGameUI : MonoBehaviour
{
    public Button readyButton;

    public Image readyImage;

    private bool isButtonClicked = false;


    private void Start()
    {
        // Enable the button on the server
        readyButton.interactable = true;
       
        // Attach the click handler
        readyButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Ready!");

        gameObject.SetActive(false);

        PlayerManager.LocalInstance.CmdDealCards();
    }
}
