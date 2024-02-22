using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardSO;

    private void Awake()
    {
        
    }

    public CardSO GetCardSO()
    {
        return cardSO;
    }
}
