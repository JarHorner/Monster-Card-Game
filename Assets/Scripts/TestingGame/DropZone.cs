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

    [SerializeField] private GameObject playerArea;
    [SerializeField] private GameObject enemyArea;

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
                    BattleRight(card, cards[OnePosition]);
                }
                if (cards[ThreePosition] != null)
                {
                    BattleBottom(card, cards[ThreePosition]);
                }
                break;

            case OnePosition:
                if (cards[ZeroPosition] != null)
                {
                    BattleLeft(card, cards[ZeroPosition]);
                }
                if (cards[TwoPosition] != null)
                {
                    BattleRight(card, cards[TwoPosition]);
                }
                if (cards[FourPosition] != null)
                {
                    BattleBottom(card, cards[FourPosition]);
                }
                break;

            case TwoPosition:
                if (cards[OnePosition] != null)
                {
                    BattleLeft(card, cards[OnePosition]);
                }
                if (cards[FivePosition] != null)
                {
                    BattleBottom(card, cards[FivePosition]);
                }
                break;

            case ThreePosition:
                if (cards[ZeroPosition] != null)
                {
                    BattleTop(card, cards[ZeroPosition]);
                }
                if (cards[FourPosition] != null)
                {
                    BattleRight(card, cards[FourPosition]);
                }
                if (cards[SixPosition] != null)
                {
                    BattleBottom(card, cards[SixPosition]);
                }
                break;

            case FourPosition:
                if (cards[OnePosition] != null)
                {
                    BattleTop(card, cards[OnePosition]);
                }
                if (cards[ThreePosition] != null)
                {
                    BattleLeft(card, cards[ThreePosition]);
                }
                if (cards[FivePosition] != null)
                {
                    BattleRight(card, cards[FivePosition]);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleBottom(card, cards[SevenPosition]);
                }
                break;

            case FivePosition:
                if (cards[TwoPosition] != null)
                {
                    BattleTop(card, cards[TwoPosition]);
                }
                if (cards[FourPosition] != null)
                {
                    BattleLeft(card, cards[FourPosition]);
                }
                if (cards[EightPosition] != null)
                {
                    BattleBottom(card, cards[EightPosition]);
                }
                break;

            case SixPosition:
                if (cards[ThreePosition] != null)
                {
                    BattleTop(card, cards[ThreePosition]);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleRight(card, cards[SevenPosition]);
                }
                break;

            case SevenPosition:
                if (cards[FourPosition] != null)
                {
                    BattleTop(card, cards[FourPosition]);
                }
                if (cards[SixPosition] != null)
                {
                    BattleLeft(card, cards[SixPosition]);
                }
                if (cards[EightPosition] != null)
                {
                    BattleRight(card, cards[EightPosition]);
                }
                break;

            case EightPosition:
                if (cards[FivePosition] != null)
                {
                    BattleTop(card, cards[FivePosition]);
                }
                if (cards[SevenPosition] != null)
                {
                    BattleLeft(card, cards[SevenPosition]);
                }
                break;
        }
        battlePlayedOut = true;
    }

    public bool BattlePlayedOut()
    {
        return battlePlayedOut;
    }

    private void BattleTop(Card playerCard, Card enemyCard)
    {
        if (playerCard.GetCardOwnerID() == enemyCard.GetCardOwnerID()) return;

        if (playerCard.topRank > enemyCard.bottomRank)
        {
            Debug.Log("Your card won top battle!");
            CardGameManager.Instance.UpdateCardOwnerID(SwapPlayerIDNum(enemyCard.GetCardOwnerID()), enemyCard);
            PlayerManager.LocalInstance.CmdUpdateLosingCard(enemyCard.gameObject);
        }
        else
        {
            Debug.Log("Your card lost top battle!");
        }
    }

    private void BattleLeft(Card playerCard, Card enemyCard)
    {
        if (playerCard.GetCardOwnerID() == enemyCard.GetCardOwnerID()) return;

        if (playerCard.leftRank > enemyCard.rightRank)
        {
            Debug.Log("Your card won left battle!");
            CardGameManager.Instance.UpdateCardOwnerID(SwapPlayerIDNum(enemyCard.GetCardOwnerID()), enemyCard);
            PlayerManager.LocalInstance.CmdUpdateLosingCard(enemyCard.gameObject);
        }
        else
        {
            Debug.Log("Your card lost left battle!");
        }
    }

    private void BattleRight(Card playerCard, Card enemyCard)
    {
        if (playerCard.GetCardOwnerID() == enemyCard.GetCardOwnerID()) return;

        if (playerCard.rightRank > enemyCard.leftRank)
        {
            Debug.Log("Your card won right battle!");
            CardGameManager.Instance.UpdateCardOwnerID(SwapPlayerIDNum(enemyCard.GetCardOwnerID()), enemyCard);
            PlayerManager.LocalInstance.CmdUpdateLosingCard(enemyCard.gameObject);
        }
        else
        {
            Debug.Log("Your card lost right battle!");
        }
    }

    private void BattleBottom(Card playerCard, Card enemyCard)
    {
        if (playerCard.GetCardOwnerID() == enemyCard.GetCardOwnerID()) return;

        if (playerCard.bottomRank > enemyCard.topRank)
        {
            Debug.Log("Your card won bottom battle!");
            CardGameManager.Instance.UpdateCardOwnerID(SwapPlayerIDNum(enemyCard.GetCardOwnerID()), enemyCard);
            PlayerManager.LocalInstance.CmdUpdateLosingCard(enemyCard.gameObject);
        }
        else
        {
            Debug.Log("Your card lost top battle!");
        }
    }

    private int SwapPlayerIDNum(int num)
    {
        if (num == 1)
            return 2;
        else
            return 1;
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
