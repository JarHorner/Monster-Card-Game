using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DropZone : NetworkBehaviour
{
    //Constant numbers to avoid magic numbers
    private const int ZeroPosition = 0;
    private const int OnePosition = 1;
    private const int TwoPosition = 2;
    private const int ThreePosition = 3;
    private const int FourPosition = 4;
    private const int FivePosition = 5;
    private const int SixPosition = 6;
    private const int SevenPosition = 7;
    private const int EightPosition = 8;

    public static DropZone Instance;

    [SerializeField] private List<GameObject> postions;
    [SerializeField] private List<Card> cards;

    [SerializeField] private Card lastCardPlayed;
    private int cardsPlayed = 0;

    private bool battlePlayedOut = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (cardsPlayed == 9)
        {
            CardGameManager.Instance.SetStateEndGame();
        }
    }

    public bool PositionExists(GameObject go)
    {
        return postions.Contains(go);
    }

    public bool IsPositionFilled(GameObject position)
    {
        int positionIndex = postions.IndexOf(position);
        return postions[positionIndex].transform.childCount > 0;
    }

    public void AddCardToBoard(GameObject card, GameObject position)
    {
        int positionIndex = postions.IndexOf(position);
        cards[positionIndex] = card.GetComponent<Card>();
        cardsPlayed++;
    }

    public void ChangeLastCardPlayed(GameObject card)
    {
        lastCardPlayed = card.GetComponent<Card>();
    }

    public void BattleCardsAlgorithm()
    {
        int cardPosition = cards.IndexOf(lastCardPlayed);
        Card card = cards[cardPosition];

        Debug.Log("Battling! with cardPosition: " + cardPosition + " and the card: " + card.name);

        switch (cardPosition)
        {
            case ZeroPosition:
                if (cards[OnePosition] != null)
                {
                    BattleRight(card, OnePosition);
                }
                if (cards[ThreePosition] != null)
                {
                    BattleBottom(card, ThreePosition);
                }
                break;

            case OnePosition:
                if (cards[ZeroPosition] != null)
                {
                    BattleLeft(card, ZeroPosition);
                }
                if (cards[TwoPosition] != null)
                {
                    BattleRight(card, TwoPosition);
                }
                if (cards[FourPosition] != null)
                {
                    BattleBottom(card, FourPosition);
                }
                break;

            case TwoPosition:
                if (cards[OnePosition] != null)
                {
                    BattleLeft(card, OnePosition);
                }
                if (cards[FivePosition] != null)
                {
                    BattleBottom(card, FivePosition);
                }
                break;

            case ThreePosition:
                if (cards[ZeroPosition] != null)
                {
                    BattleTop(card, ZeroPosition);
                }
                if (cards[FourPosition] != null)
                {
                    BattleRight(card, FourPosition);
                }
                if (cards[SixPosition] != null)
                {
                    BattleBottom(card, SixPosition);
                }
                break;

            case FourPosition:
                if (cards[OnePosition] != null)
                {
                    BattleTop(card, OnePosition);
                }
                if (cards[ThreePosition] != null)
                {
                    BattleLeft(card, ThreePosition);
                }
                if (cards[FivePosition] != null)
                {
                    BattleRight(card, FivePosition);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleBottom(card, SevenPosition);
                }
                break;

            case FivePosition:
                if (cards[TwoPosition] != null)
                {
                    BattleTop(card, TwoPosition);
                }
                if (cards[FourPosition] != null)
                {
                    BattleLeft(card, FourPosition);
                }
                if (cards[EightPosition] != null)
                {
                    BattleBottom(card, EightPosition);
                }
                break;

            case SixPosition:
                if (cards[ThreePosition] != null)
                {
                    BattleTop(card, ThreePosition);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleRight(card, SevenPosition);
                }
                break;

            case SevenPosition:
                if (cards[FourPosition] != null)
                {
                    BattleTop(card, FourPosition);
                }
                if (cards[SixPosition] != null)
                {
                    BattleLeft(card, SixPosition);
                }
                if (cards[EightPosition] != null)
                {
                    BattleRight(card, EightPosition);
                }
                break;

            case EightPosition:
                if (cards[FivePosition] != null)
                {
                    BattleTop(card, FivePosition);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleLeft(card, SevenPosition);
                }
                break;
        }
        battlePlayedOut = true;
    }

    public bool BattlePlayedOut()
    {
        return battlePlayedOut;
    }

    private void BattleTop(Card card, int position)
    {
        if (card.topRank > cards[position].bottomRank)
        {
            Debug.Log("Your card won top battle!");
            cards[position].AssignCardOwnerID(PlayerManager.LocalInstance.GetPlayerID());
            PlayerManager.LocalInstance.CmdUpdateLosingCard(cards[position].gameObject, cards[position].GetCardOwnerID());
        }
        else
        {
            Debug.Log("Your card lost top battle!");
        }
    }

    private void BattleLeft(Card card, int position)
    {
        if (card.leftRank > cards[position].rightRank)
        {
            Debug.Log("Your card won left battle!");
            cards[position].AssignCardOwnerID(PlayerManager.LocalInstance.GetPlayerID());
            PlayerManager.LocalInstance.CmdUpdateLosingCard(cards[position].gameObject, cards[position].GetCardOwnerID());
        }
        else
        {
            Debug.Log("Your card lost left battle!");
        }
    }

    private void BattleRight(Card card, int position)
    {
        if (card.rightRank > cards[position].leftRank)
        {
            Debug.Log("Your card won right battle!");
            cards[position].AssignCardOwnerID(PlayerManager.LocalInstance.GetPlayerID());
            PlayerManager.LocalInstance.CmdUpdateLosingCard(cards[position].gameObject, cards[position].GetCardOwnerID());
        }
        else
        {
            Debug.Log("Your card lost right battle!");
        }
    }

    private void BattleBottom(Card card, int position)
    {
        if (card.bottomRank > cards[position].topRank)
        {
            Debug.Log("Your card won top battle!");
            cards[position].AssignCardOwnerID(PlayerManager.LocalInstance.GetPlayerID());
            PlayerManager.LocalInstance.CmdUpdateLosingCard(cards[position].gameObject, cards[position].GetCardOwnerID());
        }
        else
        {
            Debug.Log("Your card lost top battle!");
        }
    }

    public string DetermineWinner()
    {
        int playerCardCount = 0;
        int enemyCardCount = 0;

        foreach(Card card in cards)
        {
            CardFlipper cardflipper = card.GetComponent<CardFlipper>();
            if (cardflipper.DeterminePlayerCard())
            {
                playerCardCount += 1;
            }
            else
            {
                enemyCardCount += 1;
            }
        }


        if (playerCardCount == enemyCardCount)
        {
            return "Tie!" + "\n" + "You Had " + playerCardCount + " Cards" + "\n" + "Enemy Had " + enemyCardCount + " Cards";
        }
        else if (playerCardCount > enemyCardCount)
        {
            return "You Win!" + "\n" + "You Had " + playerCardCount + " Cards" + "\n" + "Enemy Had " + enemyCardCount + " Cards";
        }
        else
        {
            return "You Lose!" + "\n" + "You Had " + playerCardCount + " Cards" + "\n" + "Enemy Had " + enemyCardCount + " Cards";
        }
    }
}
