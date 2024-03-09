using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OtherTurnTimerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    private int previousTimerNumber;


    private void Awake()
    {

    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsPlayerTurn())
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
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetPlayerTurnTimer());
        timerText.text = countdownNumber.ToString();

        if (previousTimerNumber != countdownNumber)
        {
            previousTimerNumber = countdownNumber;
        }
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
