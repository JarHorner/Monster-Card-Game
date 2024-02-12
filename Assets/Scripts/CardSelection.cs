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

    [SerializeField] private List<GameObject> pickedCards;
    [SerializeField] private List<int> topRanks;
    [SerializeField] private List<int> rightRanks;
    [SerializeField] private List<int> bottomRanks;
    [SerializeField] private List<int> leftRanks;

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
            pickedCards.Add(addedCard);

            topRanks.Add(addedCard.GetComponent<PickCard>().topRank);
            rightRanks.Add(addedCard.GetComponent<PickCard>().rightRank);
            bottomRanks.Add(addedCard.GetComponent<PickCard>().bottomRank);
            leftRanks.Add(addedCard.GetComponent<PickCard>().leftRank);

            CardGenerator.Instance.SpawnNewSetOfCards();

            if (pickedCards.Count == MAX_CARD_AMT)
            {
                maxCardsSelected = true;
            }
        }
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





}
