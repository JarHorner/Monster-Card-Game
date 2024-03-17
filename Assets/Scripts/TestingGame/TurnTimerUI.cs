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
        Debug.Log("TurntimerUI Starting");
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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
