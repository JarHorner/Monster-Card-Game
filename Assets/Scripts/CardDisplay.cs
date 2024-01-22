using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public int playerOwner;

    public TMP_Text nameText;

    public TMP_Text topRankText;
    public int topRank;

    public TMP_Text rightRankText;
    public int rightRank;

    public TMP_Text bottomRankText;
    public int bottomRank;

    public TMP_Text leftRankText;
    public int leftRank;

    public TMP_Text levelText;

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

        monsterArtwork.sprite = card.artwork;
        element.sprite = card.element;

        levelText.text = card.level.ToString();

        AssignRanks();

        topRankText.text = topRank.ToString();
        rightRankText.text = rightRank.ToString();
        bottomRankText.text = bottomRank.ToString();
        leftRankText.text = leftRank.ToString();
        
        ChangeBGColorToPlayer();
    }

    private void AssignRanks()
    {
        int[] assignedValues = new int[4];

        // Try assigning values until the total is within the specified range
        do
        {
            // Take the first 4 non-null and non-zero values from the shuffled array
            assignedValues = card.rangeOfRanks.Take(4).Where(v => v != 0).ToArray();
        }
        while (assignedValues.Length < 4 || assignedValues.Sum() < card.minRank || assignedValues.Sum() > card.maxRank);

        topRank = assignedValues[0];
        rightRank = assignedValues[1];
        bottomRank = assignedValues[2];
        leftRank = assignedValues[3];
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

    public void ChangeCardLayers(string newLayer)
    {
        nameText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        topRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        bottomRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        leftRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        levelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        monsterArtwork.sortingLayerName = newLayer;
        monsterArtworkBackground.sortingLayerName = newLayer;
        element.sortingLayerName = newLayer;
        selectedBorder.sortingLayerName = newLayer; 
    }

}
