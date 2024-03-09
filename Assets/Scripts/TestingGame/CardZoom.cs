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


        string parentTag = this.transform.parent.tag;

        if (parentTag == "PlayerArea")
        {
            InstatiateCardBeside();
        }
        else if (parentTag == "BoardPosition")
        {
            InstatiateCardOnTop();
        }

        GetCardDetails();


        zoomCard.transform.SetParent(Canvas.transform, true);

        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.localScale = new Vector2(1.75f, 1.75f);

    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }

    private void InstatiateCardBeside()
    {
        zoomCard = Instantiate(zoomCardPrefab, new Vector2(gameObject.transform.position.x - 250, gameObject.transform.position.y), Quaternion.identity);
    }

    private void InstatiateCardOnTop()
    {
        zoomCard = Instantiate(zoomCardPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
    }

    private void GetCardDetails()
    {
        CardSO cardSO = gameObject.GetComponent<Card>().GetCardSO();

        zoomCard.GetComponent<ZoomedCard>().PopulateZoomedCard(cardSO.cardName, cardSO.topRank, cardSO.leftRank, cardSO.rightRank, cardSO.bottomRank, 
            cardSO.backgroundSprite, cardSO.monsterSprite, cardSO.elementSprite);
    }

}
