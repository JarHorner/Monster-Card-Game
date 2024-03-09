using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTimerUI : MonoBehaviour
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
        int timerNumber = Mathf.CeilToInt(CardGameManager.Instance.GetPlayerTurnTimer());
        turnTimerText.text = timerNumber.ToString("F0");

        if (previousTimerNumber != timerNumber)
        {
            previousTimerNumber = timerNumber;
        }
    }

    public void SetTurnTimerText(int timerNum)
    {
        turnTimerText.text = timerNum.ToString("F0");
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
