using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private CardSO cardSO;

    [SerializeField] private TMP_Text cardNameText;

    [SerializeField] private TMP_Text topRankText;
    [SerializeField] private TMP_Text leftRankText;
    [SerializeField] private TMP_Text rightRankText;
    [SerializeField] private TMP_Text bottomRankText;

    [SerializeField] private TMP_Text levelText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image MonsterImage;
    [SerializeField] private Image elementImage;

    private void Awake()
    {
        cardNameText.text = cardSO.cardName;

        topRankText.text = cardSO.topRank.ToString();
        leftRankText.text = cardSO.leftRank.ToString();
        rightRankText.text = cardSO.rightRank.ToString();
        bottomRankText.text = cardSO.bottomRank.ToString();

        levelText.text = cardSO.level.ToString();

        backgroundImage.sprite = cardSO.backgroundSprite;
        MonsterImage.sprite = cardSO.monsterSprite;
        elementImage.sprite = cardSO.elementSprite;
    }

    public CardSO GetCardSO()
    {
        return cardSO;
    }
}
