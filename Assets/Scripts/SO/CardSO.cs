using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite artwork;
    public Sprite element;

    public string cardName;
    public int[] rangeOfRanks;
    public int minRank;
    public int maxRank;

    public int level;
}
