using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
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

    private void Awake()
    {
        Instance = this;
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
                    if (card.rightRank > cards[OnePosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                        cards[OnePosition].AssignPlayerOwnerID(PlayerManager.LocalInstance.GetPlayerID());
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[ThreePosition] != null)
                {
                    if (card.bottomRank > cards[ThreePosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                        cards[ThreePosition].AssignPlayerOwnerID(PlayerManager.LocalInstance.GetPlayerID());
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case OnePosition:
                if (cards[ZeroPosition] != null)
                {
                    if (card.leftRank > cards[ZeroPosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[TwoPosition] != null)
                {
                    if (card.rightRank > cards[TwoPosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[FourPosition] != null)
                {
                    if (card.bottomRank > cards[FourPosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case TwoPosition:
                if (cards[OnePosition] != null)
                {
                    if (card.leftRank > cards[OnePosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost left battle!");
                    }
                }
                if (cards[FivePosition] != null)
                {
                    if (card.bottomRank > cards[FivePosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case ThreePosition:
                if (cards[ZeroPosition] != null)
                {
                    if (card.topRank > cards[ZeroPosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[FourPosition] != null)
                {
                    if (card.rightRank > cards[FourPosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[SixPosition] != null)
                {
                    if (card.bottomRank > cards[SixPosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case FourPosition:
                if (cards[OnePosition] != null)
                {
                    if (card.topRank > cards[OnePosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[ThreePosition] != null)
                {
                    if (card.leftRank > cards[ThreePosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost left battle!");
                    }
                }
                if (cards[FivePosition] != null)
                {
                    if (card.rightRank > cards[FivePosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[SevenPosition] != null)
                {
                    if (card.bottomRank > cards[SevenPosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case FivePosition:
                if (cards[TwoPosition] != null)
                {
                    if (card.topRank > cards[TwoPosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[FourPosition] != null)
                {
                    if (card.leftRank > cards[FourPosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost left battle!");
                    }
                }
                if (cards[EightPosition] != null)
                {
                    if (card.bottomRank > cards[EightPosition].topRank)
                    {
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;

            case SixPosition:
                if (cards[FourPosition] != null)
                {
                    if (card.topRank > cards[FourPosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[SevenPosition] != null)
                {
                    if (card.rightRank > cards[SevenPosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                break;

            case SevenPosition:
                if (cards[FourPosition] != null)
                {
                    if (card.topRank > cards[FourPosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[SixPosition] != null)
                {
                    if (card.leftRank > cards[SixPosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost left battle!");
                    }
                }
                if (cards[EightPosition] != null)
                {
                    if (card.rightRank > cards[EightPosition].leftRank)
                    {
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                break;

            case EightPosition:
                if (cards[FivePosition] != null)
                {
                    if (card.topRank > cards[FivePosition].bottomRank)
                    {
                        Debug.Log("Your card won top battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost top battle!");
                    }
                }
                if (cards[SevenPosition] != null)
                {
                    if (card.leftRank > cards[SevenPosition].rightRank)
                    {
                        Debug.Log("Your card won left battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost left battle!");
                    }
                }
                break;
        }
    }


}
