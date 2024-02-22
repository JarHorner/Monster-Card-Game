using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTimer : MonoBehaviour
{

    [SerializeField] TMP_Text turnTimerText;
    private float previousTimerNumber;

    private void Start()
    {
        CardGameManager.Instance.OnStateChanged += CardGameManager_OnStateChanged;

        Hide();
    }

    private void CardGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (CardGameManager.Instance.IsPlayerTurn())
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
        int countdownNumber = Mathf.CeilToInt(CardGameManager.Instance.GetPlayerTurnTimer());
        turnTimerText.text = countdownNumber.ToString("F0");

        if (previousTimerNumber != countdownNumber)
        {
            previousTimerNumber = countdownNumber;
        }
    }

    public void SetTurnTimerText(int timerNum)
    {
        turnTimerText.text = timerNum.ToString("F0");
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
