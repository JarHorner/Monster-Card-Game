using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void Awake()
    {
    //    // makes a clone so it does not apply to anything else 
    //    material = new Material(headMeshRenderer.material);
    //    headMeshRenderer.material = material;

    }


    public void SetPlayerColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
