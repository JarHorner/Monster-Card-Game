using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public const int MAX_SPAWNED_CARDS = 8;
    
    [SerializeField] private GameObject[] possibleCards;
    [SerializeField] private Vector3[] cardLocations;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MAX_SPAWNED_CARDS; i++)
        {
            int randomCard = Random.Range(0, possibleCards.Length);

            Instantiate(possibleCards[randomCard], cardLocations[i], Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
