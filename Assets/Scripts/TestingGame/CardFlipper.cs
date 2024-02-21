using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFlipper : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite ownerCardSprite;
    [SerializeField] private Sprite enemyCardSprite;
    [SerializeField] private Sprite cardBackSprite;

    public void Flip()
    {
        Sprite currentSprite = backgroundImage.sprite;

        if (currentSprite == null)
        {
            backgroundImage.sprite = cardBackSprite;

            MoveChildToLaterPosition();
        }
        else
        {
            ChangeBackgroundEnemyCardColor();

            MoveChildBackToOriginalPosition();
        }
    }

    public void MoveChildToLaterPosition()
    {
        Transform childToMove = backgroundImage.gameObject.transform;

        int newIndex = transform.childCount - 1;

        childToMove.SetSiblingIndex(newIndex);
        
    }

    public void MoveChildBackToOriginalPosition()
    {
        Transform childToMove = backgroundImage.gameObject.transform;

        int originalIndex = 0;

        childToMove.SetSiblingIndex(originalIndex);
    }

    public void ChangeBackgroundOwnerCardColor()
    {
        backgroundImage.sprite = ownerCardSprite;
    }

    public void ChangeBackgroundEnemyCardColor()
    {
        backgroundImage.sprite = enemyCardSprite;
    }
}
