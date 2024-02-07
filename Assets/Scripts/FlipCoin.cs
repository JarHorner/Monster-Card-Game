using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCoin : MonoBehaviour
{
    public Image image;
    public Sprite[] sides;
    private int flipCount = 1;
    private int amtFlips;

    // determines how many flips will play based on winning side and starts flip animation coroutine
    public void Flip(int winningSide)
    {
        if (winningSide == 1)
        {
            amtFlips = 21;
        }
        else
        {
            amtFlips = 20;
        }

        StartCoroutine(FlipAnimation(0.0001f, 1.0f));
    }

    // "flips" the coin using a duration and size of the coin
    // the duration is how fast between changes in size is done
    // the size is the y size of the coin, which is beeing changed every frame after duration
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

            image.sprite = sides[flipCount % 2];

            while (size < 0.99)
            {
                size = size + 0.07f;
                transform.localScale = new Vector3(1f, size, size);
                yield return new WaitForSeconds(duration);
            }

            flipCount++;
        }
        
    }
}
