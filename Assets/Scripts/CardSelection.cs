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

            RemoveSelectedCard();
        }
    }


    public void SetSelectedCard(PlayerCardSO card)
    {
        Debug.Log("sets selected card");
        selectedCard = card;
    }

    public void RemoveSelectedCard()
    {
        selectedCard = null;
    }

    public bool IsSelectedCardPopulated()
    {
        return selectedCard != null;
    }

    public List<PlayerCardSO> GetPickedCards()
    {
        return pickedPlayerCardSOList;
    }

    public PlayerCardSO GetPlayerCardSO(int cardNum)
    {
        return pickedPlayerCardSOList[cardNum];
    }


    public int GetPickedCardsAmount()
    {
        return pickedPlayerCardSOList.Count;
    }

    public bool GetMaxCardsSelected()
    {
        return maxCardsSelected;
    }
}
