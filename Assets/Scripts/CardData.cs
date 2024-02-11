using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct CardData : IEquatable<CardData>, INetworkSerializeByMemcpy
{
    public FixedString64Bytes cardName;
    public int level;

    public int topRank;
    public int rightRank;
    public int bottomRank;
    public int leftRank;

    //public SpriteRenderer monsterArtworkBackground;
    //public SpriteRenderer monsterArtwork;
    //public SpriteRenderer element;
    //public SpriteRenderer selectedBorder;

    public bool Equals(CardData other)
    {
        return cardName == other.cardName &&
            level == other.level &&
        topRank == other.topRank &&
            rightRank == other.rightRank &&
            bottomRank == other.bottomRank &&
            leftRank == other.leftRank;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref cardName);
        serializer.SerializeValue(ref level);
        serializer.SerializeValue(ref topRank);
        serializer.SerializeValue(ref rightRank);
        serializer.SerializeValue(ref bottomRank);
        serializer.SerializeValue(ref leftRank);
    }
}
