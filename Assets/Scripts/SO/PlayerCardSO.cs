using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Player Card")]
public class PlayerCardSO : ScriptableObject
{
    public string cardName;
    public int level;

    public int topRank;
    public int rightRank;
    public int bottomRank;
    public int leftRank;

    public Sprite monsterSprite;
    public Sprite elementSprite;

    public void Init(string cardName, int level, int topRank, int rightRank, int bottomRank, int leftRank, Sprite monsterSprite, Sprite elementSprite)
    {
        this.cardName = cardName;
        this.level = level;
        this.topRank = topRank;
        this.rightRank = rightRank;
        this.bottomRank = bottomRank;
        this.leftRank = leftRank;
        this.monsterSprite = monsterSprite;
        this.elementSprite = elementSprite;
    }
}
