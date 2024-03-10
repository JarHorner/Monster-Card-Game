using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class StartGameUI : NetworkBehaviour
{
    public Button readyButton;
    public GameObject startGamePanel;
    public GameObject readyText;

    [SyncVar] public int playersReady = 0;

    private void Start()
    {
        // Enable the button on the server
        readyButton.interactable = true;
       
        // Attach the click handler
        readyButton.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (playersReady == 2)
        {
            // Call a method to start the game on the server
            RpcStartGame();
        }
    }

    private void OnClick()
    {
        Debug.Log("Ready!");

        startGamePanel.SetActive(false);
        readyText.SetActive(true);

        PlayerClickedReadyButton();
    }

    public void PlayerClickedReadyButton()
    {
        Debug.Log("Command!");

        playersReady++;

        PlayerManager.LocalInstance.CmdDealCards();

        RpcUpdatePlayerReadyCount(playersReady);
    }

    [ClientRpc]
    public void RpcUpdatePlayerReadyCount(int count)
    {
        Debug.Log("Client RPC!");

        // Update the local player count
        playersReady++;

        // You can add UI updates or other logic here if needed
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        Debug.Log("Start the Game");

        readyText.SetActive(false);

        // Reset the player count for the next round or game
        playersReady = 0;

        // Add logic to start the game on all clients here

        CardGameManager.Instance.SetStateCountdownToStartActive();
    }
}
