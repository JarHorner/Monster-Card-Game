using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button newCardsButton;
    [SerializeField] private Button mainMenuButton;

    public TMP_Text title;

    private void Awake()
    {
        quickJoinButton.onClick.AddListener(() => {
            //Loader.Load(Loader.Scene.LobbyScene);
        });
        newCardsButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.CardSelectionScene);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        Hide();
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
