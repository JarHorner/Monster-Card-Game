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
        CardGameManager.Instance.OnStateChanged += CardGameManager_OnStateChanged;

        Hide();
    }

    private void CardGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
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
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
