using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelection : MonoBehaviour
{
    public const int MAX_CARD_AMT = 5;

    public static CardSelection Instance;

    [SerializeField]private GameObject selectedCard;
    public bool cardSelected = false;

    private bool maxCardsSelected = false;

    //[SerializeField] private List<CardData> pickedCards;
    [SerializeField] private List<GameObject> pickedCards;

    [SerializeField] private List<string> names;
    [SerializeField] private List<int> levels;
    [SerializeField] private List<int> topRanks;
    [SerializeField] private List<int> rightRanks;
    [SerializeField] private List<int> bottomRanks;
    [SerializeField] private List<int> leftRanks;
    [SerializeField] private List<Sprite> monsterSprites;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        pickedCards = new List<GameObject>();
    }

    public void AddCard()
    {
        if (selectedCard != null && !maxCardsSelected)
        {
            GameObject addedCard = selectedCard;
            Debug.Log(addedCard.name);
            //CardData pickedCard = CreateNewCardData(addedCard.GetComponent<PickCard>());
            pickedCards.Add(selectedCard);

            AddValuesToCardLists(addedCard);

            CardGenerator.Instance.SpawnNewSetOfCards();

            if (pickedCards.Count == MAX_CARD_AMT)
            {
                maxCardsSelected = true;
            }
        }
    }

    private void AddValuesToCardLists(GameObject addedCard)
    {
        names.Add(addedCard.GetComponent<PickCard>().cardName);
        levels.Add(addedCard.GetComponent<PickCard>().level);
        topRanks.Add(addedCard.GetComponent<PickCard>().topRank);
        rightRanks.Add(addedCard.GetComponent<PickCard>().rightRank);
        bottomRanks.Add(addedCard.GetComponent<PickCard>().bottomRank);
        leftRanks.Add(addedCard.GetComponent<PickCard>().leftRank);

        monsterSprites.Add(addedCard.GetComponent<PickCard>().monsterArtworkSprite);
    }

    private CardData CreateNewCardData(PickCard pickCard)
    {
        CardData cardData = new CardData
        {
            cardId = pickedCards.Count,
            cardName = pickCard.name,
            level = pickCard.level,
            topRank = pickCard.topRank,
            rightRank = pickCard.rightRank,
            bottomRank = pickCard.bottomRank,
            leftRank = pickCard.leftRank,
        };

        return cardData;
    }

    public void SaveSelectedCardIDPlayerPrefs()
    {
        List<int> cardIDs = new List<int>();
        for (int i = 0; i < pickedCards.Count; i++)
        {
            cardIDs.Add(i);
        }

        // Convert the list of integers to a comma-separated string
        string cardIDsString = string.Join(",", cardIDs);

        // Save the string in PlayerPrefs
        PlayerPrefs.SetString("SelectedCardIDs", cardIDsString);
        PlayerPrefs.Save();
    }

    public List<int> LoadSelectedCardIDPlayerPrefs()
    {
        // Retrieve the string from PlayerPrefs
        string cardIDsString = PlayerPrefs.GetString("SelectedCardIDs", "");

        // Convert the comma-separated string back to a list of integers
        List<int> cardIDs = new List<int>(Array.ConvertAll(cardIDsString.Split(','), int.Parse));

        return cardIDs;
    }

    public void SetSelectedCard(GameObject card)
    {
        Debug.Log("sets selected card");
        selectedCard = card;
    }

    public void RemoveSelectedCard()
    {
        selectedCard = null;
    }

    public List<GameObject> GetPickedCards()
    {
        return pickedCards;
    }

    public string GetName(int cardNum)
    {
        return names[cardNum];
    }
    public int GetLevel(int cardNum)
    {
        return levels[cardNum];
    }
    public int GetTopRank(int cardNum)
    {
        return topRanks[cardNum];
    }
    public int GetRightRank(int cardNum)
    {
        return rightRanks[cardNum];
    }
    public int GetBottomRank(int cardNum)
    {
        return bottomRanks[cardNum];
    }
    public int GetLeftRank(int cardNum)
    {
        return leftRanks[cardNum];
    }
    public Sprite GetMonsterSprite(int cardNum)
    {
        return monsterSprites[cardNum];
    }





}
