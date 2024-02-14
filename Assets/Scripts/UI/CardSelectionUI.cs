using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button ConnectButton;
    [SerializeField] private Button addCardButton;
    [SerializeField] private SelectedResultUI selectedResultUI;
    [SerializeField] private TMP_Text cardAmountText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        ConnectButton.onClick.AddListener(() => {
            GameMultiplayer.playMultiplayer = true;

            CardSelection.Instance.SaveSelectedCardIDPlayerPrefs();

            Loader.Load(Loader.Scene.LobbyScene);
        });
        addCardButton.onClick.AddListener(() => {
            selectedResultUI.DisplayMessage(CardSelection.Instance.GetMaxCardsSelected());

            CardSelection.Instance.AddCard();

            cardAmountText.text = "Cards: " + CardSelection.Instance.GetPickedCardsAmount();
        });
    }


}
