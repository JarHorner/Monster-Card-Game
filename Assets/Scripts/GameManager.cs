using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text timeText;
    public float time;
    private bool timeRunning;

    public TMP_Text turnText;

    void Start()
    {
        time = 5f;
        timeText.text = time.ToString("0.00");

        turnText.text = "Player Turn";
        timeRunning = true;
    }

    void Update()
    {
        if(timeRunning)
        {
            trackTime();
        }
    }

    private void trackTime()
    {
        time -= Time.deltaTime;
        timeText.text = time.ToString("0.00");

        if (time <= 0f)
        {
            timeRunning = false;
            PlayerEndTurn();
        }
    }

    private void PlayerEndTurn()
    {
        time = 00.00f;
        timeText.text = time.ToString("0.00");

        turnText.text = "AI Turn";

        StartCoroutine(AiTurn());
    }

    IEnumerator AiTurn()
    {
        Debug.Log("AI Turn!");
        yield return new WaitForSeconds(2f);
        AiEndTurn();
    }

     private void AiEndTurn()
    {
        time = 5f;
        timeText.text = time.ToString("0.00");

        turnText.text = "Player Turn";

        timeRunning = true;
    }

    public void setCard(GameObject position, CardDisplay cardSelected)
    {

    }
}
