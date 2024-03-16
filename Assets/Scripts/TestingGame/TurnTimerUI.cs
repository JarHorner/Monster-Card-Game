using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class TurnTimerUI : NetworkBehaviour
{

    [SerializeField] TMP_Text turnTimerText;
    private float previousTimerNumber;

    private void Start()
    {
        CardGameManager.Instance.OnStateChanged += CardGameManager_OnStateChanged;

        //Hide();
    }

    private void CardGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (CardGameManager.Instance.IsPlayerTurn())
        {
            Show();
        }
        else
        {
            //Hide();
        }
        //Show();
    }

    private void Update()
    {
        if (CardGameManager.Instance.IsPlayerTurn())
        {
            int timerNumber = Mathf.CeilToInt(CardGameManager.Instance.GetPlayerTurnTimer());
            turnTimerText.text = timerNumber.ToString("F0");

            if (previousTimerNumber != timerNumber)
            {
                previousTimerNumber = timerNumber;
            }
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
