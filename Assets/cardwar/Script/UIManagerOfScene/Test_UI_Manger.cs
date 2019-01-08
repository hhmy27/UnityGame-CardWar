using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Test_UI_Manger : MonoBehaviour
{
    private Color nowcolor;
    private float AlphaValue = 1;
    private float AlphaValue1 = 1;

    [SerializeField]
    private Button win_BtnNextLevel;//胜利面板
    [SerializeField]
    private Button Win_BtnreturnToStart;//胜利面板返回主菜单
    [SerializeField]
    private Text RoundText;//显示回合的Text组件
    [SerializeField]
    public Button Game_BtnRoundOver;//游戏中回合结束
    [SerializeField]
    private Button Game_BtnEscape;//游戏中逃跑按钮
    [SerializeField]
    private Button Lose_BtnRestartGame;//失败面板重新开始
    [SerializeField]
    private Button Lose_BtnreturnToStart;//失败面板返回主菜单
    [SerializeField]
    private GameObject EventGameObject;//事件
    [SerializeField]
    private GameObject ConfirmReturnStartPanel;//确认返回主菜单的提示栏
    [SerializeField]
    private Button BtnConfirmReturnStart;//确认返回主菜单
    [SerializeField]
    private Button BtnCancelReturnStart;//取消返回主菜单
    [SerializeField]
    private GameObject PausingWindow;//暂停游戏窗口
    [SerializeField]
    private Button BtnReturnToMainMenu;//返回主菜单
    [SerializeField]
    private Button BtnExitGame;//退出游戏的按钮 显示确认退出面板
    [SerializeField]
    private GameObject ConfirmExitGamePanel;//确认退出游戏的提示栏
    [SerializeField]
    private Button BtnComfirmExitGame;//确认退出游戏
    [SerializeField]
    private Button BtnCancleExitGame;//取消退出游戏
    [SerializeField]
    private Text RestCard;//剩余牌数
    [SerializeField]
    private Text HadusedCard;//已经出牌数
    [SerializeField]
    private Text CardCanUse;//能用的牌数
    [SerializeField]
    private Button BtnRestCardGroup;//剩余卡组
    [SerializeField]
    private GameObject CardPanelobject;//卡组Panel
    [SerializeField]
    private Button BtnLoseCardGroup;//弃牌堆
    [SerializeField]
    private GameObject LoseCardPanelobject;//卡组Panel
    public GameObject BtnGameList;//按钮lits
    public GameObject LoseBG;//失败背景
    public GameObject WinBG;//胜利背景

    //抽牌Panel
    public GameObject DragCardPanel;
    //弃牌Panel
    public GameObject LostCardPanel;


    private int escapeRound;
    private bool ShowCardPanle = false;
    private bool ShowLoseCardPanle = false;
    [SerializeField]
    private DOTweenAnimation CardPanel;
    [SerializeField]
    private DOTweenAnimation LoseCardPanel;

    //弃牌堆 用来存放丢弃卡牌，丢弃时设置到LostCard中，然后播放动画
    private GameObject[] LostCards;



    private bool ShowExitWindow = false;
    private bool ShowPausingWindow = false;
    private GameObject Heroour;
    private GameObject Heroene;
    private void Update()
    {
        Heroour.GetComponent<SpriteRenderer>().color = new Color(nowcolor.r, nowcolor.g, nowcolor.b, AlphaValue);
        Heroene.GetComponent<SpriteRenderer>().color = new Color(nowcolor.r, nowcolor.g, nowcolor.b, AlphaValue1);

        if (Input.GetKeyUp(KeyCode.Escape) && ShowPausingWindow == false)
        {
            ShowPausingWindow = true;
            PausingWindow.SetActive(true);
            GameManager.Instance.Pause();
            ConfirmExitGamePanel.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && ShowPausingWindow)
        {
            ShowPausingWindow = false;
            ShowExitWindow = false;
            ConfirmReturnStartPanel.SetActive(false);
            PausingWindow.SetActive(false);
            GameManager.Instance.UnPause();
        }
    }


    private void Start()
    {
        Heroene = GameObject.FindGameObjectWithTag("EnemyHero");
        Heroour = GameObject.FindGameObjectWithTag("Hero");
        nowcolor = GameObject.FindGameObjectWithTag("Hero").GetComponent<SpriteRenderer>().color;
        win_BtnNextLevel.onClick.AddListener(OnBtnNextClick);

        Win_BtnreturnToStart.onClick.AddListener(OnBtnReturnToStartClick);//胜利面板返回主菜单

        Lose_BtnreturnToStart.onClick.AddListener(OnBtnReturnToStartClick);//失败面板返回主菜单

        Game_BtnEscape.onClick.AddListener(OnBtnEscapeClick);//逃跑按钮

        Game_BtnRoundOver.onClick.AddListener(OnBtnOverClick);//回合结束按钮

        Lose_BtnRestartGame.onClick.AddListener(OnBtnRestarBattleClick);//失败面板重新开始

        BtnConfirmReturnStart.onClick.AddListener(OnBtnReturnToStartClick);//提示框确认返回主菜单

        BtnCancelReturnStart.onClick.AddListener(BtnCancelReturnStartClick);//取消返回主菜单

        BtnReturnToMainMenu.onClick.AddListener(ShowReturnMainPanel);//返回主菜单

        BtnExitGame.onClick.AddListener(ShowExitwindow);//退出游戏弹出提示框

        BtnComfirmExitGame.onClick.AddListener(BtnConfirmExitClick);//确认退出游戏

        RestCard.text = (CardManager.Instance.HowManyCard - CardManager.Instance.HowManytoDrug).ToString();

        BtnCancleExitGame.onClick.AddListener(BtnCancleExitGameClick);//取消退出游戏

        BtnRestCardGroup.onClick.AddListener(BtnSeeRestCardGroupClick);//查看剩下卡牌

        BtnLoseCardGroup.onClick.AddListener(BtnSeeLoseCardGroupClick);
    }


    /// <summary>
    /// 查看弃牌堆
    /// </summary>
    private void BtnSeeLoseCardGroupClick()
    {
        //CardPanelobject.GetComponent<CardGroupToSee>().DestroyCardToSee();
        LoseCardPanelobject.GetComponent<LoseCardGroupToSee>().CreatLoseCardToSee();
        LostCardPanel.SetActive(true);
        //if (ShowLoseCardPanle == false)
        //{
        //    //显示出面板
        //    LoseCardPanel.DOPlayForward();
        //    ShowLoseCardPanle = true;
        //}
        //else
        //{

        //    //退出面板
        //    LoseCardPanel.DOPlayBackwards();
        //    ShowLoseCardPanle = false;
        //}
    }

    /// <summary>
    /// 查看抽牌堆
    /// </summary>
    private void BtnSeeRestCardGroupClick()
    {
        //CardPanelobject.GetComponent<CardGroupToSee>().DestroyCardToSee();
        CardPanelobject.GetComponent<CardGroupToSee>().CreatCardToSee();
        DragCardPanel.SetActive(true);

        //if (ShowCardPanle == false)
        //{
        //    显示出面板
        //    CardPanel.DOPlayForward();
        //    ShowCardPanle = true;
        //}
        //else
        //{

        //    退出面板
        //    CardPanel.DOPlayBackwards();
        //    ShowCardPanle = false;
        //}
    }


    //取消返回游戏
    private void BtnCancleExitGameClick()
    {
        ConfirmExitGamePanel.SetActive(false);
    }

    //显示离开面板
    private void ShowExitwindow()
    {
        ShowExitWindow = true;
        ConfirmExitGamePanel.SetActive(true);

    }


    private void ShowReturnMainPanel()
    {
        ConfirmReturnStartPanel.SetActive(true);
    }

    //取消返回主菜单
    private void BtnCancelReturnStartClick()
    {
        ShowExitWindow = false;
        ConfirmReturnStartPanel.SetActive(false);
    }

    //确认退出游戏
    private void BtnConfirmExitClick()
    {
        Application.Quit();
    }

    //重新开始战斗
    private void OnBtnRestarBattleClick()
    {

        CardManager.Instance.HowManyCardCanUse = 5;
        if (GameManager.Instance.GameLevel == 1)
        {
            SceneManager.Instance.ChangeScene(GameManager.Scene.Test, "Test");

        }
        if (GameManager.Instance.GameLevel == 2)
        {
            SceneManager.Instance.ChangeScene(GameManager.Scene.Test1, "Test1");

        }
        if (GameManager.Instance.GameLevel == 3)
        {
            SceneManager.Instance.ChangeScene(GameManager.Scene.Test2, "Test2");

        }
        GameManager.Instance.UnPause();
    }

    void sleep() { BtnGameList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-110.5f, -14.456f, 0), 0.5f); }
    /// <summary>
    /// 回合结束按钮
    /// </summary>
    public void OnBtnOverClick()
    {
        ControlMessageBox.Instance.SetMessage("回合结束");

        Game_BtnRoundOver.interactable=false;
        BtnGameList.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(715, -155, 0), 0.5f).OnComplete(() =>
        {




        });
        Invoke("sleep", 5f);

        EventGameObject.GetComponent<MainSceneEvent>().Round++;
        if (EventGameObject.GetComponent<MainSceneEvent>().Round - escapeRound >= 4)
        {

            Game_BtnEscape.GetComponent<Button>().interactable = true;

        }
        RoundText.text = "回合数：" + EventGameObject.GetComponent<MainSceneEvent>().Round;
        //回合结束计算
        EventGameObject.GetComponent<MainSceneEvent>().OnEndCalculate();
        Debug.Log("回合结束");
        //TODO
        //弃牌堆
        LostCards = GameObject.FindGameObjectsWithTag("Card");

        //动画列表
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < LostCards.Length; i++)
        {
            LostCards[i].GetComponent<Transform>().SetParent(GameObject.Find("LostCardList").GetComponent<Transform>());

            //弃牌时 传入弃牌List
            CardManager.Instance.AddCardToLoseCardList(LostCards[i].GetComponent<CardInstance>().card);



        }
        LostCardAnimation(GameObject.Find("LostCardList"), sequence);
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed = EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed + LostCards.Length;//
        //弃牌更新
        UIUpdate();

        //GameObject[] Cards = GameObject.FindGameObjectsWithTag("Card");
        //foreach(var i in Cards)
        //{
        //    i.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(this.transform.position.x, this.transform.position.y - 200),0.3f);
        //    i.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(this.transform.position.x, this.transform.position.y + 200), 0.3f).SetDelay(0.5f);
        //}

        //动画
        Invoke("SendMessageToNewRoundStart", 4f);
    }
    /// <summary>
    /// 弃牌动画
    /// </summary>
    /// <param name="GO"></param>
    /// <param name="sequence"></param>
    private void LostCardAnimation(GameObject GO, Sequence sequence)
    {
        sequence.Append(GO.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(80, 150, 0), 0.5f));
        sequence.Append(GO.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(1000, -250, 0), 0.5f).OnComplete
            (() =>
        {
            for (int i = 0; i < LostCards.Length; i++)
            {

                /*CardManager.Instance.CardToLoseList.Add()*/
                Destroy(LostCards[i]);
            }
            GO.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-190, -266, 0), 3f);
        }
        ));

    }

    public void SendMessageToNewRoundStart()
    {
        EventGameObject.GetComponent<MainSceneEvent>().NewRoundStart();
    }
    public void sleepAlp()
    {

         DOTween.To(() => AlphaValue, x => AlphaValue = x, 1, 1f);
    }
    //逃跑
    private void OnBtnEscapeClick()
    {
        ControlMessageBox.Instance.SetMessage("玩家逃跑");
        //事件
        // if (EventGameObject.GetComponent<MainSceneEvent>().Round - escapeRound > 4)
        //{
        //回家 todo
        //}  


        HeroFade();

        GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ReduceReward_half();
        OnBtnOverClick();
        GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().Present_HP = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().HP;
        GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().Present_MP = 0;
        Game_BtnEscape.GetComponent<Button>().interactable = false;
        escapeRound = EventGameObject.GetComponent<MainSceneEvent>().Round;
    }

    public void HeroFade()
    {
        DOTween.To(() => AlphaValue, x => AlphaValue = x, 0, 0.5f);
        Invoke("sleepAlp", 3f);
    }

    public void EnemyHeroFade()
    {

        DOTween.To(() => AlphaValue1, x => AlphaValue1 = x, 0, 0.5f);
        DOTween.To(() => AlphaValue1, x => AlphaValue1 = x, 1, 1f).SetDelay(3f);
    }

    private void OnDestroy()
    {
        win_BtnNextLevel.onClick.RemoveListener(OnBtnNextClick);
        Win_BtnreturnToStart.onClick.RemoveListener(OnBtnReturnToStartClick);
        Lose_BtnreturnToStart.onClick.RemoveListener(OnBtnReturnToStartClick);
        Game_BtnEscape.onClick.RemoveListener(OnBtnEscapeClick);
        Game_BtnRoundOver.onClick.RemoveListener(OnBtnOverClick);
        Lose_BtnRestartGame.onClick.RemoveListener(OnBtnRestarBattleClick);
        BtnConfirmReturnStart.onClick.RemoveListener(BtnConfirmExitClick);
        BtnCancelReturnStart.onClick.RemoveListener(BtnCancelReturnStartClick);
        BtnReturnToMainMenu.onClick.RemoveListener(ShowReturnMainPanel);
        BtnExitGame.onClick.RemoveListener(ShowExitwindow);
        BtnRestCardGroup.onClick.RemoveListener(BtnSeeRestCardGroupClick);
        BtnLoseCardGroup.onClick.RemoveListener(BtnSeeLoseCardGroupClick);

    }



    //获胜后点击下一关的按钮
    private void OnBtnNextClick()
    {
        GameManager.Instance.UnPause();
        CardManager.Instance.HowManyCardCanUse = 5;
        GameManager.Instance.GameLevel++;
        //转场后会销毁该脚本
        if(GameManager.Instance.GameLevel==4)
        {

         SceneManager.Instance.ChangeScene(GameManager.Scene.Last, "last");

        }
        else
        {

         SceneManager.Instance.ChangeScene(GameManager.Scene.Rest, "Rest");
        }
     
        PlayManager.Instance.Point += 2;
        CardManager.Instance.AddaNewCardToGroup();
        //TODO
    }
    


    //返回主菜单
    private void OnBtnReturnToStartClick()
    {
        GameManager.Instance.UnPause();
        CardManager.Instance.HowManyCardCanUse = 5;
        SceneManager.Instance.ChangeScene(GameManager.Scene.Start, "Start");

        //TODO
    }
    public void UIUpdate()
    {

        HadusedCard.text = EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed.ToString();
        if (CardManager.Instance.HowManyCardCanUse >= 0)
        {
            CardCanUse.text = CardManager.Instance.HowManyCardCanUse.ToString() + '/' + CardManager.Instance.SumCardCanUse.ToString();
        }
        else
        {
            CardCanUse.text ="0/5";
        
        }
        RestCard.text = CardManager.Instance.CardToDrugList.Count.ToString();

    
    }

}
