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
        ChangeCardLayer(gameObject.GetComponent<PickCard>());
    }

    private void Decrease()
    {
        gameObject.transform.localScale = gameObject.GetComponent<PickCard>().GetBaseScale();
        enlarged = false;
        ChangeCardLayer(gameObject.GetComponent<PickCard>());
    }

    // Changes layer when card is hovered over so it appears higher than other cards
    private void ChangeCardLayer(PickCard pickCard)
    {
        if (enlarged)
        {
            string layerName = "SelectedCard";

            pickCard.ChangeCardLayers(layerName);

            pickCard.ChangeBGColorOnHover();
        }
        else
        {
            string layerName = "Card";

            pickCard.ChangeCardLayers(layerName);

            pickCard.ChangeBGColorToPlayer();
        }
    }
}
