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

    [SerializeField] private Card lastCardFlipped;

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

    public void FillPosition(GameObject position, GameObject card)
    {
        int positionIndex = postions.IndexOf(position);

        AddCardToBoard(positionIndex, card.GetComponent<Card>());
    }

    private void AddCardToBoard(int index, Card card)
    {
        cards[index] = card;

        lastCardFlipped = card;
    }

    public void BattleCardsAlgorithm()
    {
        bool cardFlipped = false;
        int cardPosition = cards.IndexOf(lastCardFlipped);
        Card card = cards[cardPosition];
        switch (cardPosition)
        {
            case ZeroPosition:
                if (cards[OnePosition] != null)
                {
                    if (card.rightRank > cards[OnePosition].rightRank)
                    {
                        cardFlipped = true;
                        lastCardFlipped = cards[OnePosition];
                        Debug.Log("Your card won right battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost right battle!");
                    }
                }
                if (cards[ThreePosition] != null)
                {
                    if (card.bottomRank > cards[ThreePosition].bottomRank)
                    {
                        cardFlipped = true;
                        lastCardFlipped = cards[ThreePosition];
                        Debug.Log("Your card won bottom battle!");
                    }
                    else
                    {
                        Debug.Log("Your card lost bottom battle!");
                    }
                }
                break;
            case OnePosition:

                break;
            case TwoPosition:

                break;
            case ThreePosition:

                break;
            case FourPosition:

                break;
            case FivePosition:

                break;
            case SixPosition:

                break;
            case SevenPosition:

                break;
            case EightPosition:

                break;
        }

        if (!cardFlipped)
            return;

        //BattleCardsAlgorithm();
    }


}
