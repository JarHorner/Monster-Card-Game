using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private bool selectedCard = false;
    private CardDisplay cardSelected;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    // when mouse clicked, checks what object has been clicked, and processes accordingly.
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit) 
        {
            if (selectedCard)
            {
                UnselectCard(cardSelected);
            }
            
            return;
        }

        Debug.Log(rayHit.collider.gameObject.name);
        if (rayHit.collider.gameObject.name.Contains("Card") && !selectedCard)
        {
            cardSelected = rayHit.collider.gameObject.GetComponent<CardDisplay>();
            SelectCard(cardSelected);
        }
        else if (rayHit.collider.gameObject.name.Contains("Card") && selectedCard)
        {
            UnselectCard(cardSelected);

            cardSelected = rayHit.collider.gameObject.GetComponent<CardDisplay>();
            SelectCard(cardSelected);
        }
    }

    // "selects" card by adding border.
    private void SelectCard(CardDisplay cardDisplay)
    {
        cardDisplay.selectedBorder.enabled = true;
        selectedCard = true;
    }

    // "unselects" card by removing border.
    private void UnselectCard(CardDisplay cardDisplay)
    {
        cardDisplay.selectedBorder.enabled = false;
        selectedCard = false;
        cardSelected = null;
    }
}
