using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button ConnectButton;
    [SerializeField] private Button addCardButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        ConnectButton.onClick.AddListener(() => {
            GameMultiplayer.playMultiplayer = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        addCardButton.onClick.AddListener(() => {
            CardSelection.Instance.AddCard();
        });
    }


}
