using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PickCard : NetworkBehaviour
{
    public CardSO card;

    public bool selected = false;
    private bool enlarged = false;

    public TMP_Text cardNameText;
    public string cardName;
    public TMP_Text levelText;
    public int level;

    public TMP_Text topRankText;
    public int topRank;

    public TMP_Text rightRankText;
    public int rightRank;

    public TMP_Text bottomRankText;
    public int bottomRank;

    public TMP_Text leftRankText;
    public int leftRank;

    public SpriteRenderer monsterArtworkBackground;
    public SpriteRenderer monsterArtworkSpriteRenderer;
    public Sprite monsterArtworkSprite;
    public SpriteRenderer element;
    public SpriteRenderer selectedBorder;

    public Color playerColor;
    public Color playerHoverColor;

    public Vector3 baseScale;

    private void Awake()
    {
        AssignRanks();

        topRankText.text = topRank.ToString();
        rightRankText.text = rightRank.ToString();
        bottomRankText.text = bottomRank.ToString();
        leftRankText.text = leftRank.ToString();

        cardNameText.text = card.cardName;
        cardName = card.cardName;
        levelText.text = card.level.ToString();
        level = card.level;

        monsterArtworkSpriteRenderer.sprite = card.artwork;
        element.sprite = card.element;
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
        List<GameObject> spawnedCardsList = CardGenerator.Instance.GetSpawnedCardsList();
        
        foreach (GameObject spawnedCard in spawnedCardsList)
        {
            spawnedCard.GetComponent<PickCard>().UnselectCard();
        }
    }

    //"selects" card by adding border and changing sorting layer/order for greater visiblility.
    private void SelectCard()
    {
        selectedBorder.enabled = true;
        selected = true;
        CardSelection.Instance.SetSelectedCard(this.gameObject);
    }

    //"unselects" card by removing border.
    public void UnselectCard()
    {

        selectedBorder.enabled = false;
        selected = false;
        CardSelection.Instance.RemoveSelectedCard();
    }

    // Assigns ranks to each side of the card randomly, 
    // based on information on the Card scriptable object
    private void AssignRanks()
    {
        int[] assignedRanks = new int[4];
        int totalOfRanks;

        int attempts = 0;
        int maxAttempts = 100;

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
        monsterArtworkBackground.color = playerColor;
    }
    // Changes background color, used when enlarging
    public void ChangeBGColorOnHover()
    {
        monsterArtworkBackground.color = playerHoverColor;
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

        monsterArtworkBackground.sortingLayerName = newLayer;
        monsterArtworkSpriteRenderer.sortingLayerName = newLayer;
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
}
