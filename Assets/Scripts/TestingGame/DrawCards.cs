using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private GameObject playerArea;
    public void OnClick()
    {
        for(int i = 0; i < 5; i++)
        {
            int randomCardIndex = Random.Range(0, cards.Count);

            GameObject card = Instantiate(cards[randomCardIndex], new Vector2(0,0), Quaternion.identity);
            card.transform.SetParent(playerArea.transform, false);
        }
    }
}
