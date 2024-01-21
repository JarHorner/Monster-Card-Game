using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoverEnlarge : MonoBehaviour
{
    public TimeTracker timeTracker;

    private GameObject card;
    private bool enlarged = false;

    void Start() 
    {
        timeTracker = GameObject.Find("TimeTracker").GetComponent<TimeTracker>();
    }

    void Update()
    {
        if (timeTracker.currentPhase == Phase.Turn)
        {
            Enlarge();  
        }
    }

    // casts a ray that checks if the mouse cursor is over a card. If it is, the scale of the card will enlarge.
    private void Enlarge()
    {
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        // if nothing hit, ensures if a card was enlarged before, scales it to normal size.
        if (!rayHit) {
            if (enlarged)
            {
                card.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                enlarged = false;
                ChangeCardLayer(card.GetComponent<CardDisplay>());
            }
            return; 
        } 

        // an object is hit, and if it is a card that is not already been enlarged, that card will enlarge.
        if (rayHit.collider.gameObject.name.Contains("HandCard") && !enlarged)
        {
            card = rayHit.collider.gameObject;
            Debug.Log(rayHit.collider.gameObject.name);
            card.transform.localScale = new Vector3(card.transform.localScale.x * 1.075f, card.transform.localScale.y * 1.075f, 1f);
            enlarged = true;
            ChangeCardLayer(card.GetComponent<CardDisplay>());
        }
    }

    private void ChangeCardLayer(CardDisplay cardDisplay)
    {
        if (enlarged)
        {
            string layerName = "SelectedCard";
            cardDisplay.nameText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;

            cardDisplay.topLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.rightLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.bottomLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.leftLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;

            cardDisplay.monsterArtwork.sortingLayerName = layerName;
            cardDisplay.monsterArtworkBackground.sortingLayerName = layerName;
            cardDisplay.element.sortingLayerName = layerName;
            cardDisplay.selectedBorder.sortingLayerName = layerName; 

            cardDisplay.monsterArtworkBackground.color = new Color(0.1715319f, 0.4243631f, 0.9245283f, 1f);
        }
        else
        {
            string layerName = "Card";
            cardDisplay.nameText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;

            cardDisplay.topLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.rightLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.bottomLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;
            cardDisplay.leftLevelText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = layerName;

            cardDisplay.monsterArtwork.sortingLayerName = layerName;
            cardDisplay.monsterArtworkBackground.sortingLayerName = layerName;
            cardDisplay.element.sortingLayerName = layerName;
            cardDisplay.selectedBorder.sortingLayerName = layerName; 

            cardDisplay.monsterArtworkBackground.color = new Color(0.3607843f, 0.5405295f, 0.8980392f, 1f);
        }
        

    }
}
