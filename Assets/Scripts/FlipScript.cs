using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sides;
    private int flipCount = 1;
    private int amtFlips;

    public void Flip(int winningSide)
    {
        if (winningSide == 1)
        {
            amtFlips = 20;
        }
        else
        {
            amtFlips = 21;
        }

        spriteRenderer.enabled = true;

        StartCoroutine(FlipAnimation(0.0001f, 1.0f));
    }

    IEnumerator FlipAnimation(float duration, float size)
    {
        while (flipCount <= amtFlips)
        {
            while (size > 0.1)
            {
                size = size - 0.07f;
                transform.localScale = new Vector3(1f, size, 1f);
                yield return new WaitForSeconds(duration);
            }

            spriteRenderer.sprite = sides[flipCount % 2];

            while (size < 0.99)
            {
                size = size + 0.07f;
                transform.localScale = new Vector3(1f, size, 1f);
                yield return new WaitForSeconds(duration);
            }

            flipCount++;
        }
        
    }
}
