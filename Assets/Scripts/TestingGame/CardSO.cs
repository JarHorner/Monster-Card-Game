using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public int level;

    public int topRank;
    public int rightRank;
    public int bottomRank;
    public int leftRank;

    public Sprite backgroundSprite;
    public Sprite monsterSprite;
    public Sprite elementSprite;

}
