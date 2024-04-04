using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TargetClick : NetworkBehaviour
{

    public void OnTargetClick()
    {
        if (isOwned)
        {
            PlayerManager.LocalInstance.CmdTargetSelfCard(gameObject);
        }
        else
        {
            PlayerManager.LocalInstance.CmdTargetOtherCard(gameObject);
        }
    }
}
