using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : NetworkBehaviour
{
    [SerializeField] TMP_Text countdownTimerText;
    private float previousTimerNumber;

    private void Start()
    {
        Debug.Log("CountdowntimerUI Starting");
        CardGameManager.Instance.OnStateChanged += CardGameManager_OnStateChanged;

        //Hide();
    }

    private void CardGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        Debug.Log("is the state Countdown timer " + CardGameManager.Instance.IsCountdownToStart());
        if (CardGameManager.Instance.IsCountdownToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        if (CardGameManager.Instance.IsCountdownToStart())
        {
            int countdownNumber = Mathf.CeilToInt(CardGameManager.Instance.GetCountdownToStartTimer());
            countdownTimerText.text = countdownNumber.ToString("F0");

            if (previousTimerNumber != countdownNumber)
            {
                previousTimerNumber = countdownNumber;
            }
        }
    }

    public void SetCountdownTimerText(int timerNum)
    {
        countdownTimerText.text = timerNum.ToString("F0");
    }


    public void Show()
    {
        Debug.Log("Showing");
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        Debug.Log("Showing");
        gameObject.SetActive(false);
    }
}
