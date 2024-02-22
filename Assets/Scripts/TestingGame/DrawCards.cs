using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{

    public void OnClick()
    {
        if (!PlayerManager.LocalInstance.HasPickedUpCards())
        {
            CardGameManager.Instance.SetStateCountdownToStartActive();

            PlayerManager.LocalInstance.CmdDealCards();

            CmdDestoryDrawCardButton();
        }
    }

    [Command]
    public void CmdDestoryDrawCardButton()
    {
        TargetDestoryDrawCardButton();
    }

    [TargetRpc]
    void TargetDestoryDrawCardButton()
    {
        Debug.Log("Destory Draw Card Button");
        Destroy(gameObject);
    }

}
