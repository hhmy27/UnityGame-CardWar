using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMode_UI_Manager : MonoBehaviour {
    [SerializeField]
    private Button BtnReturn;
    [SerializeField]
    private Button BtnMode1;
    [SerializeField]
    private AudioSource Btnclick;

    private void Start()
    {
        BtnReturn.onClick.AddListener(OnReturnClick);
        BtnMode1.onClick.AddListener(OnMode1Click);
    }

    private void OnMode1Click()
    {
        Btnclick.Play();

        SceneManager.Instance.ChangeScene(GameManager.Scene.ChoiceHero, "ChoiceHero");
    }

    private void OnReturnClick()
    {
        Btnclick.Play();
        SceneManager.Instance.ChangeScene(GameManager.Scene.Start, "Start");
    }

    private void OnDestroy()
    {
        BtnReturn.onClick.RemoveListener(OnReturnClick);
        BtnMode1.onClick.RemoveListener(OnMode1Click);
    }
}
