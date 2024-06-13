using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        //PlayButton.onClick.AddListener(PlayOnClick);
        QuitButton.onClick.AddListener(QuitOnClick);
    }

    private void PlayOnClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void QuitOnClick()
    {
        Application.Quit();
    }

}
