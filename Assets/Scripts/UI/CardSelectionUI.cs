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
            //if (!CardSelection.Instance.GetMaxCardsSelected())
            //{
            //    selectedResultUI.DisplayConnectMessage(CardSelection.Instance.GetMaxCardsSelected());
            //}
            //else
            //{
                GameMultiplayer.playMultiplayer = true;

                Loader.Load(Loader.Scene.LobbyScene);
           // }
           
        });
        addCardButton.onClick.AddListener(() => {
            if (CardSelection.Instance.IsSelectedCardPopulated())
            {
                selectedResultUI.DisplaySelectMessage(CardSelection.Instance.GetMaxCardsSelected());

                CardSelection.Instance.AddCard();

                cardAmountText.text = "Cards: " + CardSelection.Instance.GetPickedCardsAmount();
            }

        });
    }


}
