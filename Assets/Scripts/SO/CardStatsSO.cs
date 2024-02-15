using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "CardStats")]
public class CardStatsSO : ScriptableObject
{
    public int level;
    public int[] rangeOfRanks;
    public int minRank;
    public int maxRank;
}
