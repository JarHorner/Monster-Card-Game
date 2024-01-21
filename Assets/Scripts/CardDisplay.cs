using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public int playerOwner;

    public TMP_Text nameText;

    public TMP_Text topLevelText;
    public TMP_Text rightLevelText;
    public TMP_Text bottomLevelText;
    public TMP_Text leftLevelText;

    public SpriteRenderer monsterArtwork;
    public SpriteRenderer monsterArtworkBackground;
    public SpriteRenderer element;
    public SpriteRenderer selectedBorder;

    public Color player1Color;
    public Color player1HoverColor;
    public Color player2Color;
    public Color player2HoverColor;

    // Assigns the scriptable objects values to the gameobject, and prints the information
    void Start()
    {
        nameText.text = card.cardName;

        topLevelText.text = card.topLevel.ToString();
        rightLevelText.text = card.rightLevel.ToString();
        bottomLevelText.text = card.bottomLevel.ToString();
        leftLevelText.text = card.leftLevel.ToString();

        monsterArtwork.sprite = card.artwork;
        element.sprite = card.element;

        ChangeBGColorToPlayer();
    }

    public void ChangeBGColorToPlayer()
    {
        if (playerOwner == 1)
        {
            monsterArtworkBackground.color = player1Color;
        }
        else
        {
            monsterArtworkBackground.color = player2Color;
        }
    }

    public void ChangeBGColorOnHover()
    {
        if (playerOwner == 1)
        {
            monsterArtworkBackground.color = player1HoverColor;
        }
        else
        {
            monsterArtworkBackground.color = player2HoverColor;
        }
    }

}
