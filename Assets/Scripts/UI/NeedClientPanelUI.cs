using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NeedClientPanelUI : NetworkBehaviour
{
    private bool hiddenPanel = false;
    [SerializeField] private TMP_Text joinCode;

    private void Start()
    {
        joinCode.text = GameObject.Find("NetworkManager").GetComponent<CardNetworkManager>().joinCode;
    }

    void Update()
    {
        if (!hiddenPanel && NetworkServer.connections.Count == 2)
        {
            RpcDestroyPanel();
            Debug.Log("Hiding need client panel!");
        }
    }

    // ClientRpc to destroy the object on all clients
    [ClientRpc]
    private void RpcDestroyPanel()
    {
        gameObject.SetActive(false);
        hiddenPanel = true;
    }
}
