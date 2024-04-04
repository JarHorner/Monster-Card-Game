using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CardZoom : NetworkBehaviour
{
    public GameObject Canvas;
    public GameObject zoomCardPrefab;

    private GameObject zoomCard;

    void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        if (gameObject.tag == "Enemy" || gameObject.GetComponent<DragDrop>().IsDragging()) return;

        zoomCard = Instantiate(zoomCardPrefab, new Vector2(gameObject.transform.position.x - 250, gameObject.transform.position.y), Quaternion.identity);

        GetCardDetails();


        zoomCard.transform.SetParent(Canvas.transform, true);

        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.localScale = new Vector2(1.6f, 1.6f);

    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }

    private void GetCardDetails()
    {
        CardSO cardSO = gameObject.GetComponent<Card>().GetCardSO();

        zoomCard.GetComponent<ZoomedCard>().PopulateZoomedCard(cardSO.cardName, cardSO.level, cardSO.topRank, cardSO.leftRank, cardSO.rightRank, cardSO.bottomRank,
            cardSO.backgroundSprite, cardSO.monsterSprite, cardSO.elementSprite);
    }

}
