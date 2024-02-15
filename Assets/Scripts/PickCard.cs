using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PickCard : NetworkBehaviour
{
    public CardStatsSO cardStatsSO;
    public PlayerCardSO playerCardSO;

    private bool selected = false;
    private bool enlarged = false;

    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text topRankText;
    [SerializeField] private TMP_Text rightRankText;
    [SerializeField] private TMP_Text bottomRankText;
    [SerializeField] private TMP_Text leftRankText;

    private int topRank;
    private int rightRank;
    private int bottomRank;
    private int leftRank;

    // used when changing layer for hovering
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private SpriteRenderer monsterSpriteRenderer;
    [SerializeField] private SpriteRenderer elementSpriteRenderer;
    [SerializeField] private SpriteRenderer selectedBorder;

    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color hoverColor;

    [SerializeField] private Vector3 baseScale;

    private void Awake()
    {
        AssignRanks();

        SetupState();
    }

    private void SetupState()
    {
        topRankText.text = topRank.ToString();
        rightRankText.text = rightRank.ToString();
        bottomRankText.text = bottomRank.ToString();
        leftRankText.text = leftRank.ToString();

        levelText.text = cardStatsSO.level.ToString();

        CreatePlayerCardSO();
    }

    private void CreatePlayerCardSO()
    {

        playerCardSO = ScriptableObject.CreateInstance<PlayerCardSO>();
        playerCardSO.Init(cardNameText.text, cardStatsSO.level, topRank, rightRank, bottomRank, leftRank, monsterSpriteRenderer.sprite, elementSpriteRenderer.sprite);
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
        CardSelection.Instance.SetSelectedCard(playerCardSO);
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
            int[] ranks = ShuffleArray(cardStatsSO.rangeOfRanks);

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
        while (Array.Exists(assignedRanks, v => v == 0) || totalOfRanks < cardStatsSO.minRank || totalOfRanks > cardStatsSO.maxRank);

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
        backgroundSpriteRenderer.color = backgroundColor;
    }
    // Changes background color, used when enlarging
    public void ChangeBGColorOnHover()
    {
        backgroundSpriteRenderer.color = hoverColor;
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

    public PlayerCardSO GetPlayerCardSO()
    {
        return playerCardSO;
    }

    public Vector3 GetBaseScale()
    {
        return baseScale;
    }

    public SpriteRenderer GetSelectedBorder()
    {
        return selectedBorder;
    }
}

