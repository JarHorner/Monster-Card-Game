using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    private Camera mainCamera;
    private bool selectedCard = false;
    [SerializeField] private CardDisplay cardSelected;

    void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // when phase is ending, unselects card so it doesnt stay selected in in player turn swap
        if (cardSelected != null)
        {
            UnselectCard(cardSelected);
        }
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

        CheckClickTarget(rayHit);
    }

    // checks which gameobject the ray hit.
    private void CheckClickTarget(RaycastHit2D rayHit) 
    {
        if (rayHit.collider.gameObject.name.Contains("HandCard"))
        {
            if (selectedCard)
                UnselectCard(cardSelected);

            cardSelected = rayHit.collider.gameObject.GetComponent<CardDisplay>();
            SelectCard(cardSelected);
        }
        //else if (rayHit.collider.gameObject.name.Contains("Position") && selectedCard)
        //{
        //    Transform position = rayHit.collider.gameObject.transform;
        //    //gameManager.setCard(position, cardSelected.gameObject);

        //    UnselectCard(cardSelected);
        //}
    }

    // "selects" card by adding border and changing sorting layer/order for greater visiblility.
    private void SelectCard(CardDisplay cardDisplay)
    {
        Debug.Log("Properly selecting card");
        if (NetworkManager.Singleton.LocalClientId == cardDisplay.ownerClientId)
        {
            Debug.Log("Card has same ID!");
            Debug.Log(cardDisplay.cardNameText.text);

            cardDisplay.GetSelectedBorder().gameObject.SetActive(true);
            selectedCard = true;
            cardSelected = cardDisplay;
        }
    }

    // "unselects" card by removing border.
    private void UnselectCard(CardDisplay cardDisplay)
    {
        cardDisplay.GetSelectedBorder().gameObject.SetActive(false);
        selectedCard = false;
        cardSelected = null;
    }
}
