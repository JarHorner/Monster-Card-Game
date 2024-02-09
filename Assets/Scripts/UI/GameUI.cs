using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private TMP_Text playerTurnText;
    [SerializeField] private TMP_Text changePlayerTurnText;
    [SerializeField] private SettingsUI settingsUI;

    private void Awake()
    {
        settingsButton.onClick.AddListener(() => {
            settingsUI.Show();
        });

        changePlayerTurnText.enabled = false;
    }

    public void ActivateChangePlayerTurn()
    {
        StartCoroutine(FlashChangePlayerTurnText());
    }

    IEnumerator FlashChangePlayerTurnText()
    {
        changePlayerTurnText.enabled = true;
        yield return new WaitForSeconds(2f);
        changePlayerTurnText.enabled = false;
    }
}
