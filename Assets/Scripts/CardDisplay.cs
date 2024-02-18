using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class CardDisplay : NetworkBehaviour
{
    public int cardNum;

    public ulong ownerClientId;

    public bool enlarged = false;
    private bool selected = false;

    public TMP_Text cardNameText;
    public TMP_Text levelText;

    public TMP_Text topRankText;

    public TMP_Text rightRankText;

    public TMP_Text bottomRankText;

    public TMP_Text leftRankText;


    public SpriteRenderer backgroundSpriteRenderer;
    public SpriteRenderer monsterSpriteRenderer;
    public SpriteRenderer elementSpriteRenderer;
    public SpriteRenderer selectedBorder;

    public Color player1Color;
    public Color player1HoverColor;
    public Color player2Color;
    public Color player2HoverColor;

    public Vector3 baseScale;

    // Assigns the scriptable objects values to the gameobject, and prints the information
    public override void OnNetworkSpawn()
    {
        ownerClientId = NetworkManager.Singleton.LocalClientId;

        Debug.Log("Owner of this card: " + ownerClientId);

        ChangeBGColorToPlayer();
    }

    private void OnMouseDown()
    {
        ClearSelectedCards();

        if (!selected)
            SelectCard();
        else
            UnselectCard();
    }

    private void ClearSelectedCards()
    {
        List<CardDisplay> spawnedCardsList = Player.LocalInstance.GetPlayerCardDisplays();

        foreach (CardDisplay spawnedCard in spawnedCardsList)
        {
            spawnedCard.UnselectCard();
        }
    }

    //"selects" card by adding border and changing sorting layer/order for greater visiblility.
    private void SelectCard()
    {
        Debug.Log("Properly selecting card");
        if (NetworkManager.Singleton.LocalClientId == ownerClientId)
        {
            Debug.Log("Card has same ID!");
            selectedBorder.enabled = true;
            selected = true;
        }
    
    }

    //"unselects" card by removing border.
    public void UnselectCard()
    {

        selectedBorder.enabled = false;
        selected = false;
    }


    // Changes background color, used when assigning card to players
    public void ChangeBGColorToPlayer()
    {
        if (ownerClientId == 0)
        {
            backgroundSpriteRenderer.color = player1Color;
        }
        else
        {
            backgroundSpriteRenderer.color = player2Color;
        }
    }

    // Changes background color, used when enlarging
    public void ChangeBGColorOnHover()
    {
        if (ownerClientId == 0)
        {
            backgroundSpriteRenderer.color = player1HoverColor;
        }
        else
        {
            backgroundSpriteRenderer.color = player2HoverColor;
        }
    }

    // Changes layers, used for enlarging the card when cursor is over it
    public void ChangeCardLayers(string newLayer)
    {
        cardNameText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        levelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        topRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        rightRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        bottomRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        leftRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        backgroundSpriteRenderer.sortingLayerName = newLayer;
        monsterSpriteRenderer.sortingLayerName = newLayer;
        elementSpriteRenderer.sortingLayerName = newLayer;
        selectedBorder.sortingLayerName = newLayer;
    }

    private void OnMouseEnter()
    {
        Enlarge();
    }

    private void OnMouseExit()
    {
        Decrease();
    }

    // casts a ray that checks if the mouse cursor is over a card. If it is, the scale of the card will enlarge.
    public void Enlarge()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 1.5f, 1f);
        enlarged = true;
        ChangeCardLayer();
    }

    private void Decrease()
    {
        gameObject.transform.localScale = baseScale;
        enlarged = false;
        ChangeCardLayer();
    }

    // Changes layer when card is hovered over so it appears higher than other cards
    private void ChangeCardLayer()
    {
        if (enlarged)
        {
            string layerName = "SelectedCard";

            ChangeCardLayers(layerName);

            ChangeBGColorOnHover();
        }
        else
        {
            string layerName = "Card";

            ChangeCardLayers(layerName);

            ChangeBGColorToPlayer();
        }
    }

    public void SetPlayerCardObjectParent(Player cardParent, int index)
    {
        cardParent.AddPlayerCardToPlayerCardDisplaysList(this);

        Vector3 playerLocation = cardParent.gameObject.transform.position;
        gameObject.transform.position = new Vector3(playerLocation.x + cardParent.GetcardLocationsList()[index].x, playerLocation.y + cardParent.GetcardLocationsList()[index].y, playerLocation.z + cardParent.GetcardLocationsList()[index].z);
    }

    public SpriteRenderer GetSelectedBorder()
    {
        return selectedBorder;
    }

}
