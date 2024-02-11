using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class HoverEnlarge : MonoBehaviour
{
    private bool enlarged = false;


    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        Enlarge();
    }

    private void OnMouseExit()
    {
        Debug.Log("Out");
        Decrease();
    }

    // casts a ray that checks if the mouse cursor is over a card. If it is, the scale of the card will enlarge.
    public void Enlarge()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 1.5f, 1f);
        enlarged = true;
        ChangeCardLayer(gameObject.GetComponent<CardDisplay>());
    }

    private void Decrease()
    {
        gameObject.transform.localScale = gameObject.GetComponent<CardDisplay>().baseScale;
        enlarged = false;
        ChangeCardLayer(gameObject.GetComponent<CardDisplay>());
    }

    // Changes layer when card is hovered over so it appears higher than other cards
    private void ChangeCardLayer(CardDisplay cardDisplay)
    {
        if (enlarged)
        {
            string layerName = "SelectedCard";

            cardDisplay.ChangeCardLayers(layerName);

            cardDisplay.ChangeBGColorOnHover();
        }
        else
        {
            string layerName = "Card";

            cardDisplay.ChangeCardLayers(layerName);

            cardDisplay.ChangeBGColorToPlayer();
        }
    }
}
