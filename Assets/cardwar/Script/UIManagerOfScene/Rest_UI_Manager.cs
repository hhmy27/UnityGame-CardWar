using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Rest_UI_Manager : MonoBehaviour
{

    [SerializeField]
    private Button BtnStart;
    [SerializeField]
    private GameObject ExitWindow;
    [SerializeField]
    private Button BtnConfirmReturnToMain;
    [SerializeField]
    private Button BtnCancelReturnToMain;
    [SerializeField]
    private Button BtnExit;
    [SerializeField]
    private DOTweenAnimation HeroProperty;
    [SerializeField]
    private DOTweenAnimation CardPanel;
    [SerializeField]
    private Button BtnShowCardGroup;
    [SerializeField]
    private Button BtnShowHeroProperty;
    [SerializeField]
    private Button BtnAddATK;
    [SerializeField]
    private Button BtnAddDEF;
    [SerializeField]
    private Button BtnAddHP;
    [SerializeField]
    private Button BtnConfirmAddPoint;
    [SerializeField]
    private Button BtnResetAddPoint;
    [SerializeField]
    private Text CardNum;
    [SerializeField]
    private Text ATKText;
    [SerializeField]
    private Text DEFText;
    [SerializeField]
    private Text HPText;
    [SerializeField]
    private Text PointText;
    [SerializeField]
    private AudioSource ButtonCilck;
    public GameObject EnemyHeroInfor;

    public AudioSource StartButtonCilck;


    private GameObject ShowNewCard;
    public Button BtnShowNewCard;
    public GameObject CardToSeePrefab;
    public GameObject CardGroupPage;


    public Button BtnReturnCardGroupPage;
    //加点
    private int Point;
    //
    private int TempHP = 0;
    private int TempATkPoint = 0;
    private int TempDEFPoint = 0;
    private int TempHPPoint = 0;
    private int TempPoint;

    private bool ShowHeroPanel = false;
    private bool ShowExitWindow = false;
    private bool ShowCardPanle = false;

    private void Start()
    {
        BtnStart.onClick.AddListener(OnStartClick);
        BtnConfirmReturnToMain.onClick.AddListener(BtnConfirmClick);
        BtnCancelReturnToMain.onClick.AddListener(BtnReturnClick);
        BtnExit.onClick.AddListener(BtnExitClick);
        BtnShowCardGroup.onClick.AddListener(BtnShowCardGroupClick);
        BtnShowHeroProperty.onClick.AddListener(BtnShowHeroPropertyClick);
        //增加攻击力
        BtnAddATK.onClick.AddListener(BtnAddATKClick);
        //增加防御力
        BtnAddDEF.onClick.AddListener(BtnAddDEFClick);
        BtnAddHP.onClick.AddListener(BtnAddHPClick);
        BtnResetAddPoint.onClick.AddListener(BtnResetAddPointClick);
        BtnConfirmAddPoint.onClick.AddListener(BtnConfirmAddPointClick);
        BtnShowNewCard.onClick.AddListener(BtnShowNewCardClick);
        BtnReturnCardGroupPage.onClick.AddListener(BtnReturnCardGroupPageClick);

        Point = PlayManager.Instance.Point;
        PointText.text = Point.ToString();

        HPText.text = "Phy: " + (PlayManager.Instance.HPPoint);
        ATKText.text = "ATK: " + (PlayManager.Instance.Attack);
        DEFText.text = "DEF: " + (PlayManager.Instance.Defend);

        DelayFindNewCard();
        TempPoint = Point;
        if (GameManager.Instance.GameLevel > 1)
        {

            NewCardAnimation();
        }

        //Invoke("DelayFindNewCard", 0.3f);
        switch (GameManager.Instance.GameLevel)
        {
            case 1:
                EnemyHeroInfor.GetComponent<Text>().text = "敌人的资料：\n<color=yellow> 哥布林之王 </color>\n常年掠夺周边的城镇，狡猾又善于作战\n技能：<color=red>打击：</color>对单体造成攻击力的伤害";
                break;
            case 2:
                EnemyHeroInfor.GetComponent<Text>().text = "敌人的资料：\n<color=yellow> 古堡幽灵 </color>\n被囚禁在古堡中的幽灵，最喜欢生人的灵魂\n技能：<color=red>打击：</color>对单体造成伤害\n <color=red>山崩：</color>对随机三个敌人造成伤害";
                break;
            case 3:
                EnemyHeroInfor.GetComponent<Text>().text = "敌人的资料：\n<color=yellow> 黑暗神 </color>\n策划整场战争的终极BOSS，光明王国的最大敌人\n技能：<color=red>打击：</color>对单体造成伤害\n<color=red>献祭：</color>对全体敌人造成伤害";

                break;
        }
    }

    private void DelayFindNewCard()
    {
        ShowNewCard = GameObject.FindGameObjectWithTag("newcard");
    }

    private void BtnReturnCardGroupPageClick()
    {
        ButtonCilck.Play();
        CardGroupPage.SetActive(false);

    }

    private void BtnShowNewCardClick()
    {
        ButtonCilck.Play();
        //ShowNewCard.SetActive(false);
        ShowNewCard.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void NewCardAnimation()
    {


        //ShowNewCard.SetActive(true);
        ShowNewCard.GetComponent<CanvasGroup>().alpha = 1;
        CardToSeePrefab.GetComponent<CardToSeeInstance>().card =CardManager.Instance. CardGroup[CardManager.Instance.HowManyCard - 1];
        CardToSeePrefab.GetComponent<CardToSeeInstance>().SetAllInfomation();
        CardToSeePrefab.GetComponent<Transform>().DORotate(new Vector3(0f, 180f, 0f), 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetAutoKill(false);

    }
    private void BtnConfirmAddPointClick()
    {
        ButtonCilck.Play();
        PlayManager.Instance.Attack += TempATkPoint;
        PlayManager.Instance.Defend += TempDEFPoint;
        PlayManager.Instance.HPPoint += TempHPPoint;
        PlayManager.Instance.Point = TempPoint;
        Point = TempPoint;
        //TempPoint = Point;
        TempATkPoint = 0;
        TempDEFPoint = 0;
        TempHPPoint = 0;
        //HeroManager.Instance.HP += TempHPPoint;
        //HeroManager.Instance.ATK += TempATkPoint;
        //HeroManager.Instance.DEF += TempDEFPoint;
        ControlMessageBox.Instance.SetMessage("加点成功");
        if(Point==0)
        {
            ControlMessageBox.Instance.SetMessage("已无可用点数");
        }

    }
    private void BtnResetAddPointClick()
    {
        ButtonCilck.Play();
        TempATkPoint = 0;
        TempDEFPoint = 0;
        TempHPPoint = 0;
        TempPoint = Point;
        HPText.text = "Phy: " + (PlayManager.Instance.HPPoint + TempHPPoint);
        ATKText.text = "ATK: " + (PlayManager.Instance.Attack + TempATkPoint);
        DEFText.text = "DEF: " + (PlayManager.Instance.Defend + TempDEFPoint);
        PointText.text = Point.ToString();
        if (Point != 0)
        {
            ControlMessageBox.Instance.SetMessage("取消加点");
        }
        if (Point == 0)
        {
            ControlMessageBox.Instance.SetMessage("已无可用点数");

        }

    }
    private void BtnAddATKClick()
    {
        ButtonCilck.Play();
        if (TempPoint > 0)
        {
            TempPoint--;
            PointText.text = TempPoint.ToString();
            TempATkPoint++;
            ATKText.text = "ATK: " + (PlayManager.Instance.Attack + TempATkPoint);

        }
        else
        {
            Debug.Log(-1);
        }

    }



    private void BtnAddHPClick()
    {
        ButtonCilck.Play();
        if (TempPoint > 0)
        {
            TempPoint--;
            PointText.text = TempPoint.ToString();
            TempHPPoint++;
            HPText.text = "Phy: " + (PlayManager.Instance.HPPoint + TempHPPoint);

        }
        else
        {
            Debug.Log(-1);
        }


    }


    private void BtnAddDEFClick()
    {
        ButtonCilck.Play();
        if (TempPoint > 0)
        {
            TempPoint--;
            PointText.text = TempPoint.ToString();
            TempDEFPoint++;
            DEFText.text = "DEF: " + (PlayManager.Instance.Defend + TempDEFPoint);

        }
        else
        {
            Debug.Log(-1);
        }

    }
    /// <summary>
    /// 显示英雄面板
    /// </summary>
    private void BtnShowHeroPropertyClick()
    {
        ButtonCilck.Play();
        ATKText.text = "ATK: " + (PlayManager.Instance.Attack );
        HPText.text = "Phy: " +( PlayManager.Instance.HPPoint);
        DEFText.text = "DEF: " +(PlayManager.Instance.Defend);
        TempDEFPoint = 0;
        TempHPPoint = 0;
        TempATkPoint = 0;
        TempPoint = Point;
        PointText.text = Point.ToString();

        if (ShowHeroPanel == false)
        {
            //显示出面板
            HeroProperty.DOPlayForward();
            //HeroProperty.DOPlayBackwards();

            ShowHeroPanel = true;
        }
        else
        {
            HeroProperty.DOPlayBackwards();
            //HeroProperty.DOPlayForward();

            ShowHeroPanel = false;
        }
    }
    /// <summary>
    /// 显示卡组
    /// </summary>
    private void BtnShowCardGroupClick()
    {

        ButtonCilck.Play();
        CardGroupPage.SetActive(true);

        if (ShowCardPanle == false)
        {
            //显示出面板
          //  CardPanel.DOPlayForward();
            ShowCardPanle = true;
        }
        else
        {
            //退出面板
        //    CardPanel.DOPlayBackwards();
            ShowCardPanle = false;
        }
    }

    private void BtnExitClick()
    {
        ButtonCilck.Play();
        ExitWindow.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && ShowExitWindow == false)
        {
            ExitWindow.SetActive(true);
            ShowExitWindow = true;
            //GameManager.Instance.Pause();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && ShowExitWindow)
        {
            ExitWindow.SetActive(false);
            ShowExitWindow = false;
            // GameManager.Instance.UnPause();
        }
    }
    private void BtnReturnClick()
    {
        ButtonCilck.Play();
        ShowExitWindow = false;
        ExitWindow.SetActive(false);
    }

    private void BtnConfirmClick()
    {

        Application.Quit();

    }

    private void OnDestroy()
    {
        BtnStart.onClick.RemoveListener(OnStartClick);
        BtnConfirmReturnToMain.onClick.RemoveListener(BtnConfirmClick);
        BtnCancelReturnToMain.onClick.RemoveListener(BtnReturnClick);
        BtnExit.onClick.RemoveListener(BtnExitClick);
        BtnShowCardGroup.onClick.RemoveListener(BtnShowCardGroupClick);
        BtnShowHeroProperty.onClick.RemoveListener(BtnShowHeroPropertyClick);
        BtnAddATK.onClick.RemoveListener(BtnAddATKClick);
        BtnAddDEF.onClick.RemoveListener(BtnAddDEFClick);
        BtnAddHP.onClick.RemoveListener(BtnAddHPClick);
        BtnResetAddPoint.onClick.RemoveListener(BtnResetAddPointClick);
        
    }
    private void OnStartClick()
    {
        ButtonCilck.Play();
        //StartButtonCilck.Play();
         switch(GameManager.Instance.GameLevel )
        {
            case 1:
            SceneManager.Instance.ChangeScene(GameManager.Scene.Test, "Test");
            break;
            //TODo
            case 2:
                SceneManager.Instance.ChangeScene(GameManager.Scene.Test1, "Test1");
                break;
            case 3:
                SceneManager.Instance.ChangeScene(GameManager.Scene.Test2, "Test2");
                break;
           
        }



    }
}

