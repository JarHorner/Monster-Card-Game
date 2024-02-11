using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelection : MonoBehaviour
{
    public static CardSelection Instance;

    private GameObject selectedCard;

    [SerializeField] private List<GameObject> pickedCards;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        pickedCards = new List<GameObject>();
    }

    public void AddCard()
    {
        if (selectedCard != null)
        {
            pickedCards.Add(selectedCard);
            selectedCard.GetComponent<PickCard>().UnselectCard();
        }
           

        foreach(GameObject card in pickedCards)
        {
            Debug.Log(card.name);
        }
    }

    public void SetSelectedCard(GameObject card)
    {
        selectedCard = card;
    }

    public void RemoveSelectedCard()
    {
        selectedCard = null;
    }






}
