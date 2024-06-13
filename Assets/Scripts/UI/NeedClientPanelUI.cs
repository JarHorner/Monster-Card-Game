using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NeedClientPanelUI : NetworkBehaviour
{
    private bool hiddenPanel = false;
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
