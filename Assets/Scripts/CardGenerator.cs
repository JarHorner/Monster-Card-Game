using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public const int MAX_SPAWNED_CARDS = 8;

    public static CardGenerator Instance;

    [SerializeField] private GameObject[] possibleCardsToSpawn;
    [SerializeField] private List<GameObject> spawnedCards;
    [SerializeField] private Vector3[] cardLocations;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        for (int i = 0; i < MAX_SPAWNED_CARDS; i++)
        {
            int randomCard = Random.Range(0, possibleCardsToSpawn.Length);

            GameObject spawnedCard = Instantiate(possibleCardsToSpawn[randomCard], cardLocations[i], Quaternion.identity);

            spawnedCards.Add(spawnedCard);
        }
    }

    public void SpawnNewSetOfCards()
    {
        DestroySpawnedCards();

        SpawnCards();
    }

    private void DestroySpawnedCards()
    {
        foreach(GameObject spawnedCard in spawnedCards)
        {
            Destroy(spawnedCard);
        }
        spawnedCards.Clear();
    }

    public List<GameObject> GetSpawnedCardsList()
    {
        return spawnedCards;
    }

}
