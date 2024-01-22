using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;

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

    // Assigns ranks to each side of the card randomly, 
    // based on information on the Card scriptable object
    private void AssignRanks()
    {
        int[] assignedRanks = new int[4];
        int totalOfRanks;

        int attempts = 0;
        int maxAttempts = 50;

        do
        {
            int[] ranks = ShuffleArray(card.rangeOfRanks);

            // Take the first 4 values from the shuffled array
            Array.Copy(ranks, assignedRanks, 4);

            totalOfRanks = CalculateSum(assignedRanks);

            attempts++;

            // Check if the attempts exceed the limit to prevent infinit loop
            if (attempts >= maxAttempts)
            {
                Debug.LogError("Exceeded maximum attempts. Unable to find a valid combination.");
                return;
            }
        }
        while (Array.Exists(assignedRanks, v => v == 0) || totalOfRanks < card.minRank || totalOfRanks > card.maxRank);

        topRank = assignedRanks[0];
        rightRank = assignedRanks[1];
        bottomRank = assignedRanks[2];
        leftRank = assignedRanks[3];
    }

    // Shuffles the array using the Fisher-Yates shuffle
    private int[] ShuffleArray(int[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            // Swap array[i] and array[j]
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return array;
    }

    // calculates sum of array
    private int CalculateSum(int[] array)
    {
        int sum = 0;
        foreach (int value in array)
        {
            sum += value;
        }
        return sum;
    }
    // Changes background color, used when assigning card to players
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

    // Changes background color, used when enlarging
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

    // Changes layers, used for enlarging the card when cursor is over it
    public void ChangeCardLayers(string newLayer)
    {
        nameText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        topRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        rightRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        bottomRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        leftRankText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;
        levelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = newLayer;

        monsterArtwork.sortingLayerName = newLayer;
        monsterArtworkBackground.sortingLayerName = newLayer;
        element.sortingLayerName = newLayer;
        selectedBorder.sortingLayerName = newLayer; 
    }

}
