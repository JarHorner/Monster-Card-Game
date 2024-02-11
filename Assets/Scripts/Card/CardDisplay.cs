using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class CardDisplay : NetworkBehaviour
{
    public CardSO card;

    public bool enlarged = false;

    public TMP_Text cardName;
    public TMP_Text level;

    public TMP_Text topRankText;
    public int topRank;

    public TMP_Text rightRankText;
    public int rightRank;

    public TMP_Text bottomRankText;
    public int bottomRank;

    public TMP_Text leftRankText;
    public int leftRank;


    public SpriteRenderer monsterArtworkBackground;
    public SpriteRenderer monsterArtwork;
    public SpriteRenderer element;
    public SpriteRenderer selectedBorder;

    public Color player1Color;
    public Color player1HoverColor;
    public Color player2Color;
    public Color player2HoverColor;

    public Vector3 baseScale;

    // Assigns the scriptable objects values to the gameobject, and prints the information
    void Start()
    {
        topRankText.text = topRank.ToString();
        rightRankText.text = rightRank.ToString();
        bottomRankText.text = bottomRank.ToString();
        leftRankText.text = leftRank.ToString();

        cardName.text = card.cardName;
        level.text = card.level.ToString();

        monsterArtwork.sprite = card.artwork;
        element.sprite = card.element;


        ChangeBGColorToPlayer();
    }

    // Changes background color, used when assigning card to players
    public void ChangeBGColorToPlayer()
    {
        if (IsLocalPlayer)
        {
            monsterArtworkBackground.color = player1Color;
        }
        else
        {
            monsterArtworkBackground.color = player2Color;
        }
    }

    // Changes background color, used when enlarging
    public void ChangeBGColorOnHover()
    {
        if (IsLocalPlayer)
        {
            monsterArtworkBackground.color = player1HoverColor;
        }
        else
        {
            monsterArtworkBackground.color = player2HoverColor;
        }
    }

    // Changes layers, used for enlarging the card when cursor is over it
    public void ChangeCardLayers(string newLayer)
    {
        cardName.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        level.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        topRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        rightRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        bottomRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        leftRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        monsterArtworkBackground.sortingLayerName = newLayer;
        monsterArtwork.sortingLayerName = newLayer;
        element.sortingLayerName = newLayer;
        selectedBorder.sortingLayerName = newLayer; 
    }

    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        Enlarge();
    }

    private void OnMouseExit()
    {
        Debug.Log("Out");
        Decrease();
    }

    // casts a ray that checks if the mouse cursor is over a card. If it is, the scale of the card will enlarge.
    public void Enlarge()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 1.5f, 1f);
        enlarged = true;
        ChangeCardLayer(gameObject.GetComponent<PickCard>());
    }

    private void Decrease()
    {
        gameObject.transform.localScale = gameObject.GetComponent<PickCard>().baseScale;
        enlarged = false;
        ChangeCardLayer(gameObject.GetComponent<PickCard>());
    }

    // Changes layer when card is hovered over so it appears higher than other cards
    private void ChangeCardLayer(PickCard pickCard)
    {
        if (enlarged)
        {
            string layerName = "SelectedCard";

            pickCard.ChangeCardLayers(layerName);

            pickCard.ChangeBGColorOnHover();
        }
        else
        {
            string layerName = "Card";

            pickCard.ChangeCardLayers(layerName);

            pickCard.ChangeBGColorToPlayer();
        }
    }

}
