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

    private void Start()
    {
        readyButton.interactable = true;
       
        readyButton.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (CardGameManager.Instance.GetPlayersReady() == 2 && CardGameManager.Instance.state == CardGameManager.State.WaitingToStart)
        {
            // Call a method to start the game on the server
            RpcStartGame();
            CardGameManager.Instance.SetStateAllPlayersReady();

            CardGameUIManager.Instance.RpcDestoryStartGameUI();
        }
    }

    private void OnClick()
    {
        Debug.Log("Ready!");

        startGamePanel.SetActive(false);
        readyText.SetActive(true);

        if (isServer)
        {
            CardGameManager.Instance.AddPlayersReady();
        }
        else {
            PlayerManager.LocalInstance.CmdAddPlayersReady();
        }

        PlayerManager.LocalInstance.CmdDealCards();
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        Debug.Log("Start the Game");

        readyText.SetActive(false);
    }
}
