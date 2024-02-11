using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoverChangeColor : MonoBehaviour
{

    [SerializeField] private Color regularColor;
    [SerializeField] private Color hoverColor;


    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }

    private void OnMouseExit()
    {
        Debug.Log("Out");
        gameObject.GetComponent<SpriteRenderer>().color = regularColor;
    }

    // casts a ray that checks if the mouse cursor is over a position. If it is, the color of the position will change.
    //private void HoverBorder()
    //{
    //    var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

    //    // if nothing hit, ensures if a position changed color before, its color will go back to normal.
    //    if (!rayHit) {
    //        if (hovering)
    //        {
    //            positionSprite.color = regularColor;
    //            hovering = false;
    //        }
    //        return; 
    //    } 

    //    // an object is hit, and if it is a position that is has not already changed colors, that position will change color.
    //    if (rayHit.collider.gameObject.name.Contains("Position") && !hovering)
    //    {
    //        positionSprite = rayHit.collider.gameObject.GetComponent<SpriteRenderer>();
    //        positionSprite.color = HoverColor;
    //        hovering = true;
    //    }
    //}
}
