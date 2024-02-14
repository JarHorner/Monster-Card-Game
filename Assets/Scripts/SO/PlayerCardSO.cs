using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Player Card")]
public class PlayerCardSO : ScriptableObject
{
    public Sprite artwork;
    public Sprite element;

    public string cardName;
    public int level;

    public int topRank;
    public int rightRank;
    public int bottomRank;
    public int leftRank;
}
