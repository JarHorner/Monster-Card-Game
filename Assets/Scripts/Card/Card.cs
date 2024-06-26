using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System;

public class Card : NetworkBehaviour
{
    [SerializeField] private CardSO cardSO;

    [SyncVar, SerializeField] private int cardOwnerID = 0;


    [SerializeField] private TMP_Text cardNameText;

    [SerializeField] private TMP_Text topRankText;
    [SerializeField] private TMP_Text leftRankText;
    [SerializeField] private TMP_Text rightRankText;
    [SerializeField] private TMP_Text bottomRankText;

    [SerializeField] private TMP_Text levelText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image MonsterImage;
    [SerializeField] private Image elementImage;

    public int topRank { get; private set; }
    public int leftRank { get; private set; }
    public int rightRank { get; private set; }
    public int bottomRank { get; private set; }

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

        AssignRankValues();
    }

    private void Start()
    {
        if (isOwned)
        {
            Debug.Log("I am the owner of this object.");
        }
        else
        {
            Debug.Log("I am not the owner of this object.");
        }
    }


    private void AssignRankValues()
    {
        topRank = cardSO.topRank;
        leftRank = cardSO.leftRank;
        rightRank = cardSO.rightRank;
        bottomRank = cardSO.bottomRank;
    }

    public int GetCardOwnerID()
    {
        return cardOwnerID;
    }

    public void SetCardOwnerID(int newID)
    {
         cardOwnerID = newID;
    }

    public CardSO GetCardSO()
    {
        return cardSO;
    }
}
