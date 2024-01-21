using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{

    public Card card;

    public TMP_Text nameText;

    public TMP_Text topLevelText;
    public TMP_Text rightLevelText;
    public TMP_Text bottomLevelText;
    public TMP_Text leftLevelText;

    public SpriteRenderer monsterArtwork;
    public SpriteRenderer element;

    void Start()
    {
        nameText.text = card.cardName;

        topLevelText.text = card.topLevel.ToString();
        rightLevelText.text = card.rightLevel.ToString();
        bottomLevelText.text = card.bottomLevel.ToString();
        leftLevelText.text = card.leftLevel.ToString();

        monsterArtwork.sprite = card.artwork;
        element.sprite = card.element;

        card.Print();
    }
}
