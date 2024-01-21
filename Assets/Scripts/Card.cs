using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite artwork;
    public Sprite artworkBackground;
    public Sprite element;

    public string cardName;
    public int topLevel;
    public int rightLevel;
    public int bottomLevel;
    public int leftLevel;

    public void Print() 
    {
        Debug.Log("Card Played: " + cardName + " | Levels(clockwise): " + topLevel + " " + rightLevel + " " + bottomLevel + " " + leftLevel);
    }
}
