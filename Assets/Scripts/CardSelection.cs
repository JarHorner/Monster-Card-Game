using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelection : MonoBehaviour
{
    public const int MAX_CARD_AMT = 5;

    public static CardSelection Instance;

    [SerializeField] private PlayerCardSO selectedCard;
    public bool cardSelected = false;

    private bool maxCardsSelected = false;

    [SerializeField] private List<PlayerCardSO> pickedPlayerCardSOList;

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
    }

    public void AddCard()
    {
        if (selectedCard != null && !maxCardsSelected)
        {
            PlayerCardSO addedPlayerCardSO = selectedCard;

            pickedPlayerCardSOList.Add(addedPlayerCardSO);

            CardGenerator.Instance.SpawnNewSetOfCards();

            if (pickedPlayerCardSOList.Count == MAX_CARD_AMT)
            {
                maxCardsSelected = true;
            }
        }
    }

    //private void AddValuesToPlayerCardLists(GameObject addedCard)
    //{
    //    PlayerCardSO playerCardSO = new PlayerCardSO
    //    {
    //        artwork = addedCard.GetComponent<PickCard>().monsterArtworkSprite,
    //        element = null,
    //        cardName = addedCard.GetComponent<PickCard>().cardName,
    //        level = addedCard.GetComponent<PickCard>().level,
    //        topRank = addedCard.GetComponent<PickCard>().GetTopRank(),
    //        rightRank = addedCard.GetComponent<PickCard>().GetRightRank(),
    //        bottomRank = addedCard.GetComponent<PickCard>().GetBottomRank(),
    //        leftRank = addedCard.GetComponent<PickCard>().GetLeftRank(),
    //    };

    //    pickedPlayerCardSO.Add(playerCardSO);
    //}


    public void SetSelectedCard(PlayerCardSO card)
    {
        Debug.Log("sets selected card");
        selectedCard = card;
    }

    public void RemoveSelectedCard()
    {
        selectedCard = null;
    }

    public List<PlayerCardSO> GetPickedCards()
    {
        return pickedPlayerCardSOList;
    }

    public int GetPickedCardsAmount()
    {
        return pickedPlayerCardSOList.Count;
    }

    public bool GetMaxCardsSelected()
    {
        return maxCardsSelected;
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
