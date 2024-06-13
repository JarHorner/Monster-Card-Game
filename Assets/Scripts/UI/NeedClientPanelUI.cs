using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NeedClientPanelUI : NetworkBehaviour
{
    //private NetworkManager networkManager;

    private void Start()
    {
        //networkManager = FindObjectOfType<NetworkManager>();
    }

    void Update()
    {
        if (NetworkServer.connections.Count == 2)
        {
            RpcRemovePanel();
        }
    }

    [ClientRpc]
    public void RpcRemovePanel()
    {
        gameObject.SetActive(false);
    }
}
