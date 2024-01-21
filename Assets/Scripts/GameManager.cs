using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TimeTracker timeTracker;

    public GameObject player1Hand;
    public int maxNumCardsInHand;
    public List<GameObject> player1Cards = new List<GameObject>();
    public Transform[] cardSlots;

    void Start()
    {
        DrawCards();
    }

    void Update()
    {
        if (timeTracker.timeRunning)
        {
            timeTracker.TrackTime();
        }
    }

    public void DrawCards()
    {
        for (int i = 0; i < maxNumCardsInHand; i++)
        {
            GameObject randCard = player1Cards[Random.Range(0, player1Cards.Count)];
            Instantiate(randCard, cardSlots[i]);       
        }
    }

    public void setCard(GameObject position, Card cardSelected)
    {

    }

    public void removeCardFromHand()
    {

    }
}
