using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTurnUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerTurnText;

    public void SetPlayerTurnText(string turnText)
    {
        playerTurnText.text = turnText;
    }
}
