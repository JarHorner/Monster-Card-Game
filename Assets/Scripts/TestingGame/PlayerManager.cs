using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager LocalInstance;

    [SerializeField] private int playerID;
    public int playerOrder;

    [SerializeField] private List<GameObject> possibleCards;
    [SerializeField] private GameObject playerArea;
    [SerializeField] private GameObject enemyArea;

    [SerializeField] private List<GameObject> positions;

    [SerializeField] private Transform gridPanel;

    private bool pickedUpCards = false;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isOwned)
        {
            LocalInstance = this;
        }

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
        pickedUpCards = true;

        for (int i = 0; i < 5; i++)
        {
            int randomCardIndex = Random.Range(0, possibleCards.Count);

            GameObject newCard = Instantiate(possibleCards[randomCardIndex], new Vector2(0, 0), Quaternion.identity);
            newCard.GetComponent<Card>().AssignPlayerOwnerID(playerID);
            NetworkServer.Spawn(newCard, connectionToClient);

            RpcDealCard(newCard);
        }
    }

    [ClientRpc]
    private void RpcDealCard(GameObject card)
    {
        if (isOwned)
        {
            card.transform.SetParent(playerArea.transform, false);
            card.tag = "Owner";

            card.GetComponent<CardFlipper>().ChangeBackgroundOwnerCardColor();
        }
        else
        {
            card.transform.SetParent(enemyArea.transform, false);
            card.tag = "Enemy";

            card.GetComponent<CardFlipper>().Flip(true);
        }

    }

    public void PlayCard(GameObject card, GameObject dropZonePosition)
    {
        CmdPlayCard(card, dropZonePosition);
        CmdStartBattle(playerID);
    }

    [Command]
    private void CmdStartBattle(int playerId)
    {
        CardGameManager.Instance.StartBattle(playerId);
    }

    [Command]
    private void CmdPlayCard(GameObject card, GameObject dropZonePosition)
    {
        RpcPlayCard(card, dropZonePosition);

        DropZone.Instance.AddCardToBoard(card, dropZonePosition);
        DropZone.Instance.ChangeLastCardPlayed(card);
    }

    [ClientRpc]
    private void RpcPlayCard(GameObject card, GameObject dropZonePosition)
    {
        card.transform.SetParent(dropZonePosition.transform, false);

        if (!isOwned)
            card.GetComponent<CardFlipper>().Flip(false);
    }

    [Command]
    public void CmdTargetSelfCard(GameObject card)
    {
        TargetSelfCard(card);
    }

    [Command]
    public void CmdTargetOtherCard(GameObject targetCard)
    {
        NetworkIdentity enemyIdentity = targetCard.GetComponent<NetworkIdentity>();
        TargetOtherCard(enemyIdentity.connectionToClient);
    }

    [TargetRpc]
    void TargetSelfCard(GameObject card)
    {
        Debug.Log("Targeted by self");
        card.GetComponent<Animator>().SetTrigger("Pulse");
    }

    [TargetRpc]
    void TargetOtherCard(NetworkConnection target)
    {
        Debug.Log("Targeted by other");
    }

    public bool HasPickedUpCards()
    {
        return pickedUpCards;
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public void SetPlayerId(int number)
    {
        playerID = number;
    }


}
