using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    private Transform cardScale;
    private bool enlarged = false;

    public TMP_Text nameText;

    public TMP_Text topLevelText;
    public TMP_Text rightLevelText;
    public TMP_Text bottomLevelText;
    public TMP_Text leftLevelText;

    public SpriteRenderer monsterArtwork;
    public SpriteRenderer element;
    public SpriteRenderer selectedBorder;

    void Start()
    {
        //cardScale = this.gameObject.transform;
        nameText.text = card.cardName;

        topLevelText.text = card.topLevel.ToString();
        rightLevelText.text = card.rightLevel.ToString();
        bottomLevelText.text = card.bottomLevel.ToString();
        leftLevelText.text = card.leftLevel.ToString();

        monsterArtwork.sprite = card.artwork;
        element.sprite = card.element;

        card.Print();
    }

    void Update()
    {
        HoverEnlarge();
    }

    private void HoverEnlarge()
    {
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit) {
            if (enlarged)
            {
                cardScale.localScale = new Vector3(0.5f, 0.5f, 1f);
                enlarged = false;
            }
            return; 
        } 

        if (rayHit.collider.gameObject.name.Contains("Card") && !enlarged)
        {
            cardScale = rayHit.collider.gameObject.transform;
            Debug.Log(rayHit.collider.gameObject.name);
            cardScale.localScale = new Vector3(cardScale.localScale.x * 1.1f, cardScale.localScale.y * 1.1f, 1f);
            enlarged = true;
        }
    }
}
