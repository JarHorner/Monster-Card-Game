using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] private List<GameObject> possibleCards;
    [SerializeField] private GameObject playerArea;
    [SerializeField] private GameObject enemyArea;
    [SerializeField] private List<GameObject> positions;

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("PlayerArea");
        enemyArea = GameObject.Find("EnemyArea");

        GameObject dropZone = GameObject.Find("DropZone");
        for (int i = 0; i < dropZone.transform.childCount; i++)
        {
            positions.Add(dropZone.transform.GetChild(i).gameObject);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomCardIndex = Random.Range(0, possibleCards.Count);

            GameObject card = Instantiate(possibleCards[randomCardIndex], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);

            RpcDealCard(card);
        }
    }

    [ClientRpc]
    private void RpcDealCard(GameObject card)
    {
        if (isOwned)
        {
            card.transform.SetParent(playerArea.transform, false);
        }
        else
        {
            card.transform.SetParent(enemyArea.transform, false);
        }

    }

    public void PlayCard(GameObject card, GameObject dropZonePosition)
    {
        cmdPlayCard(card, dropZonePosition);
    }

    [Command]
    private void cmdPlayCard(GameObject card, GameObject dropZonePosition)
    {
        RpcPlayCard(card, dropZonePosition);
    }

    [ClientRpc]
    private void RpcPlayCard(GameObject card, GameObject dropZonePosition)
    {
        card.transform.SetParent(dropZonePosition.transform, false);
        
    }
}
