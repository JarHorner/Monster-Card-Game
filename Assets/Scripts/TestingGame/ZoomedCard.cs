using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoomedCard : MonoBehaviour
{
    [SerializeField] private TMP_Text cardNameText;
    [SerializeField] private TMP_Text topRankText;
    [SerializeField] private TMP_Text leftRankText;
    [SerializeField] private TMP_Text rightRankText;
    [SerializeField] private TMP_Text bottomRankText;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image MonsterImage;
    [SerializeField] private Image elementImage;

    public void PopulateZoomedCard(string name, int level, int topRank, int leftRank, int rightRank, int bottomRank, Sprite background, Sprite monster, Sprite element)
    {
        cardNameText.text = name;

        topRankText.text = topRank.ToString();
        leftRankText.text = leftRank.ToString();
        rightRankText.text = rightRank.ToString();
        bottomRankText.text = bottomRank.ToString();
        levelText.text = level.ToString();

        backgroundImage.sprite = background;
        MonsterImage.sprite = monster;
        elementImage.sprite = element;
    }
}
