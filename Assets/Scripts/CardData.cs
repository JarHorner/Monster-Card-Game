using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class CardData
{
    public int cardId;
    public FixedString64Bytes cardName;
    public int level;

    public int topRank;
    public int rightRank;
    public int bottomRank;
    public int leftRank;

    //public SpriteRenderer monsterArtworkBackground;
    public Sprite monsterArtwork;
    //public SpriteRenderer element;
    //public SpriteRenderer selectedBorder;

    public bool Equals(CardData other)
    {
        return cardId == other.cardId &&
            cardName == other.cardName &&
            level == other.level &&
        topRank == other.topRank &&
            rightRank == other.rightRank &&
            bottomRank == other.bottomRank &&
            leftRank == other.leftRank &&
            monsterArtwork == other.monsterArtwork;
    }

    public void FilloutCardDisplay(CardDisplay cardDisplay)
    {

        cardDisplay.cardNameText.text = cardName.ToString();
        cardDisplay.levelText.text = level.ToString();
        cardDisplay.topRankText.text = topRank.ToString();
        cardDisplay.rightRankText.text = rightRank.ToString();
        cardDisplay.bottomRankText.text = bottomRank.ToString();
        cardDisplay.leftRankText.text = leftRank.ToString();

        cardDisplay.monsterArtwork.sprite = monsterArtwork;

    }

}
