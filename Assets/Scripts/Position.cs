using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Position : MonoBehaviour
{
    private bool hovering = false;
    private SpriteRenderer positionSprite;

    void Update()
    {
        HoverBorder();
    }

    // casts a ray that checks if the mouse cursor is over a position. If it is, the color of the position will change.
    private void HoverBorder()
    {
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));

        // if nothing hit, ensures if a position changed color before, its color will go back to normal.
        if (!rayHit) {
            if (hovering)
            {
                positionSprite.color = new Color(0.8584906f, 0.8584906f, 0.8584906f);
                hovering = false;
            }
            return; 
        } 

        // an object is hit, and if it is a position that is has not already changed colors, that position will change color.
        if (rayHit.collider.gameObject.name.Contains("Position") && !hovering)
        {
            positionSprite = rayHit.collider.gameObject.GetComponent<SpriteRenderer>();
            positionSprite.color = new Color(0.8117647f, 0.4627451f, 0.4705882f, 0.5f);
            hovering = true;
        }
    }
}
