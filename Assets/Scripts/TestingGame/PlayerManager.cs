using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] private List<GameObject> possibleCards;
    [SerializeField] private GameObject playerArea;
    [SerializeField] private GameObject enemyArea;
    [SerializeField] private List<GameObject> positions;

    private List<GameObject> currentCards = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("PlayerArea");
        enemyArea = GameObject.Find("EnemyArea");

        GameObject dropZone = GameObject.Find("DropZone");
        for (int i = 0; i < dropZone.transform.childCount; i++)
        {
            positions.Add(dropZone.transform.GetChild(i).gameObject);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }


    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomCardIndex = Random.Range(0, possibleCards.Count);

            GameObject card = Instantiate(possibleCards[randomCardIndex], new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(playerArea.transform, false);
        }
    }
}
