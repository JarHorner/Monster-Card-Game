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
            NetworkServer.Spawn(newCard, connectionToClient);

            RpcDealCard(newCard);
        }
    }

    [ClientRpc]
    private void RpcDealCard(GameObject card)
    {
        card.GetComponent<Card>().SetCardOwnerID(playerID);
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
        CmdStartBattle();
    }

    [Command]
    private void CmdStartBattle()
    {
        CardGameManager.Instance.StartBattle(playerID);
    }

    [Command]
    public void CmdUpdateLosingCard(GameObject cardGO)
    {
        RpcUpdateLosingCard(cardGO);
    }

    [ClientRpc]
    public void RpcUpdateLosingCard(GameObject cardGO)
    {
        Card card = cardGO.GetComponent<Card>();
        card.GetComponent<CardFlipper>().BattleFlip();
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
