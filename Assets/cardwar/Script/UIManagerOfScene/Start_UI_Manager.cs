using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_UI_Manager : MonoBehaviour {
    //Button列表
    [SerializeField]
    private Button BtnStart;
    [SerializeField]
    private Button BtnLoad;
    [SerializeField]
    private Button BtnSetGame;
    [SerializeField]
    private Button BtnExit;
    [SerializeField]
    private GameObject ExitWindow;
    [SerializeField]
    private Button BtnConfirm;
    [SerializeField]
    private Button BtnReturn;

    /// <summary>
    /// 注册监听器
    /// </summary>
    private void Start()
    {
        BtnStart.onClick.AddListener(OnStartClick);
        BtnExit.onClick.AddListener(ExitWindows);
        BtnConfirm.onClick.AddListener(BtnConfirmClick);
        BtnReturn.onClick.AddListener(BtnReturnClick);
    }

    private void BtnReturnClick()
    {
        ExitWindow.SetActive(false);
    }

    private void BtnConfirmClick()
    {
        Application.Quit();
    }

    private void ExitWindows()
    {
        ControlMessageBox.Instance.SetMessage("游戏退出");
        Application.Quit();
        ExitWindow.SetActive(true);
    }

    private void OnDestroy()
    {
        BtnStart.onClick.RemoveListener(OnStartClick);
        BtnExit.onClick.RemoveListener(ExitWindows);
        BtnConfirm.onClick.RemoveListener(BtnConfirmClick);
        BtnReturn.onClick.RemoveListener(BtnReturnClick);
    }

    private void OnStartClick()
    {
        GameManager.Instance.GameLevel = 1;
        SceneManager.Instance.ChangeScene(GameManager.Scene.ChioceMode, "ChoiceMode");
    }
}
