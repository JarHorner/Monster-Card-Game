using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public RelayManager relayManager;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_InputField joinCodeInput;

    // Start is called before the first frame update
    void Start()
    {
        //PlayButton.onClick.AddListener(PlayOnClick);
        hostButton.onClick.AddListener(HostOnClickAsync);
        joinButton.onClick.AddListener(JoinOnClickAsync);

        quitButton.onClick.AddListener(QuitOnClick);
    }

    private async void HostOnClickAsync()
    {
        string joinCode = await relayManager.StartHost();
        // Display or use joinCode as needed
        Debug.Log($"Host started with join code: {joinCode}");
    }

    private async void JoinOnClickAsync()
    {
        string joinCode = joinCodeInput.text;
        if (!string.IsNullOrEmpty(joinCode))
        {
            await relayManager.JoinHost(joinCode);
            Debug.Log("Joining host with join code: " + joinCode);
        }
        else
        {
            Debug.LogError("Join code is empty!");
        }
    }

    private void QuitOnClick()
    {
        Application.Quit();
    }

}
