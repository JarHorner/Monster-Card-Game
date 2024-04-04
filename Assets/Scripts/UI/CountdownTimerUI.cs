using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text countdownTimerText;
    private float previousTimerNumber;

    private void Start()
    {
        Debug.Log("CountdowntimerUI Starting");
    }

    private void Update()
    {
            int countdownNumber = Mathf.CeilToInt(CardGameManager.Instance.GetCountdownToStartTimer());
            countdownTimerText.text = countdownNumber.ToString("F0");

            if (previousTimerNumber != countdownNumber)
            {
                previousTimerNumber = countdownNumber;
            }
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
