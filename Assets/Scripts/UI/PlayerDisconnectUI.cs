using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisconnectUI : MonoBehaviour
{

    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button newCardsButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called before the first frame update
    private void Awake()
    {
        quickJoinButton.onClick.AddListener(() => {
            //NetworkManager.Singleton.Shutdown();
            //Loader.Load(Loader.Scene.LobbyScene);
        });
        newCardsButton.onClick.AddListener(() => {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.CardSelectionScene);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        Hide();

    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        Debug.Log("calling me!");
        Show();
        GameManager.Instance.IsGameOver(); 
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}
