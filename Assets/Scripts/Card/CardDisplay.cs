using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class CardDisplay : MonoBehaviour
{
    public CardSO card;
    public int playerOwner;

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
        if (playerOwner == 0)
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
        if (playerOwner == 0)
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

}
