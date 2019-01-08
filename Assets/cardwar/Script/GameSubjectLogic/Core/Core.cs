using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BehaviorDesigner.Runtime;
public class Core : MonoBehaviour
{
    //[HideInInspector]
    public int HP;
    [HideInInspector]
    public int DEF;
    //public int ATK;
    //1是己方，0是敌方
    [HideInInspector]
    public int own;
    public GameObject EnemyBtGroup;
    public GameObject OwnBtGroup;
    public GameObject EventAimator;
    private BatMan[] batmen;
    private GameObject CardChoose;
    [SerializeField]
    private GameObject Test_UIManager;
    [SerializeField]
    private Slider OwnBloodSlider;
    [SerializeField]
    private Text BloodText;
    //[SerializeField]
    //private Slider EnemyBloodSlider;
    //private DOTweenAnimation EndGameBG;
    public GameObject OurHero;
    public GameObject EnemyHero;
    private Vector3 RealDemageTextPosition;
    public GameObject HpDemageText;

    /// <summary>
    /// 防御塔掉血
    /// </summary>
    /// <param name="num"></param>
    public void CoreReduceHp(int num)
    {
        if (num != 0)
        {
            HP -= num;
            //TODO 扣血的显示
            HpDemageText.transform.position = RealDemageTextPosition;
            HpDemageText.GetComponent<Text>().text = (-num).ToString();
            HpDemageText.SetActive(true);
            // DemageText.transform.position = new Vector3(DemageText.transform.position.x, DemageText.transform.position.y+30, 0);
            HpDemageText.transform.DOMoveY(HpDemageText.transform.position.y + 30, 1f).OnComplete(() => HpDemageText.SetActive(false));
        }
      
    }

    private void Start()
    {
        HP = 300;
        DEF = 0;
        RealDemageTextPosition = HpDemageText.transform.position;

    }
    private void Update()
    {
        if (HP <= 0 && own == 1)
        {

            //TODO
            //游戏失败
            Test_UIManager.GetComponent<Test_UI_Manger>().LoseBG.GetComponent<DOTweenAnimation>().DOPlayForward();
            Invoke("GameOverPause", 1.5f);
        }
        if (HP <= 0 && own == 0)
        {

            //TODO
            //游戏胜利
            Test_UIManager.GetComponent<Test_UI_Manger>().WinBG.GetComponent<DOTweenAnimation>().DOPlayForward();
            Invoke("GameOverPause", 1.5f);
        }

    }

    private void CoreUIUpdate()
    {
        OwnBloodSlider.value = HP;
        BloodText.text = HP + "/300";
    }

    public void GameOverPause()
    {
        GameManager.Instance.Pause();
    }


    //敌方基地被攻击
    public void OnEnemyCoreEndRound()
    {
        ReduceReward(0);
        //播放动画
        //hp受损   消灭敌方小兵
        int ReduceHp = 0;
        //获取小兵Group下面的所有小兵组件
        batmen = OwnBtGroup.GetComponentsInChildren<BatMan>();
        //判断是否有小兵存活，如果有就加上小兵攻击力
        //for (int i = 0; i < batmen.Length; i++)
        //{
        //    if (batmen[i].CurHP > 0)
        //    {
        //        batmen[i].CurHP = -1;
        //        ReduceHp += batmen[i].Atk;
        //    }
        //}
        foreach(var i in batmen)
        {
            if (i.CurHP > 0)
            {
                //i.GetComponent<BatMan>().BatmanAtkCore();
                i.GetComponent<BatMan>().Animator.SetTrigger("Demage");
                i.CurHP = -1;
                ReduceHp += i.Atk;
            }
        }
        CoreReduceHp(ReduceHp);
       // HP = HP - ReduceHp + DEF;
        CoreUIUpdate();
        
        //以及敌方小兵退场
        Invoke("SendmessaToBtnmanAnimaion", 1f);

    }

    /// <summary>
    /// 如果英雄死亡 不会获得小兵属性加成
    /// </summary>
    public void ReduceReward(int whowin)
    {
        GameObject[] EnemyBatman = GameObject.FindGameObjectsWithTag("EnemyBatman");
        GameObject[] OurBatman = GameObject.FindGameObjectsWithTag("OurBatman");
        //如果敌方获胜，我方防御塔被攻击，我方获得属性减一半
        if (whowin == 1)
        {
            foreach (var i in EnemyBatman)
            {
                if (i.GetComponent<BatMan>().CurHP > 0)
                {
                    switch (i.GetComponent<BatMan>().Addwhich)
                    {
                        case 0:
                            OurHero.GetComponent<Hero>().AddMaxHp(-(i.GetComponent<BatMan>().AddHp / 2));

                            break;
                        case 1:
                            OurHero.GetComponent<Hero>().AddATK(-(i.GetComponent<BatMan>().AddAtk / 2));
                            break;
                        case 2:
                            OurHero.GetComponent<Hero>().AddDEF(-(i.GetComponent<BatMan>().AddDef / 2));
                            break;

                    }
                }
            }
        }
        //如果我方获胜，敌方防御塔被攻击，敌方获得属性减一半
        if (whowin == 0)
        {
            foreach (var i in OurBatman)
            {
                if (i.GetComponent<BatMan>().CurHP > 0)
                {
                    switch (i.GetComponent<BatMan>().Addwhich)
                    {
                        case 0:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().AddHP(-i.GetComponent<BatMan>().AddHp / 2);
                            break;
                        case 1:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().AddATK(-i.GetComponent<BatMan>().AddAtk / 2);
                            break;
                        case 2:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().AddDEF(-i.GetComponent<BatMan>().AddDef / 2);
                            break;

                    }
                }
            }
        }
    }
    //我方基地被攻击
    public void OnOwnCoreEndRound()
    {
        ReduceReward(1);
        //播放动画
        //基地hp受损   消灭我方小兵
        int ReduceHp = 0;
        batmen = EnemyBtGroup.GetComponentsInChildren<BatMan>();
        //for (int i = 0; i < batmen.Length; i++)
        //{
        //    if (batmen[i].CurHP > 0)
        //    {
        //        batmen[i].CurHP = -1;
        //      //  Debug.Log(batmen[i].CurHP);
        //        ReduceHp += batmen[i].Atk;
        //    }

        //}
        foreach (var i in batmen)
        {
            if (i.CurHP > 0)
            {
               // i.GetComponent<BatMan>().BatmanAtkCore();
                i.GetComponent<BatMan>().Animator.SetTrigger("Demage");

                i.CurHP = -1;
                ReduceHp += i.Atk;
            }
        }

        CoreReduceHp(ReduceHp);
        //HP = HP - ReduceHp + DEF;
        //我方小兵退场   
        CoreUIUpdate();

        Invoke("SendmessaToBtnmanAnimaion", 1f);
    }

    public void SendmessaToBtnmanAnimaion()
    {

        EventAimator.GetComponent<OnEndGroupAnimation>().ExitScene();

    }


    public void CardOnthis(Card card)
    {
        Sequence sequence = DOTween.Sequence();
        bool isDoEffect = false;
        //如果行动力为0，就不能出牌
        if (CardManager.Instance.HowManyCardCanUse > 0 && HP > 0)
        {
            CardChoose = EventAimator.GetComponent<MainSceneEvent>().ChooseCard;

            if (card.CardID == 0)
            {
                CardManager.Instance.AddCardToLoseCardList(card);
                CardID_0_OnAll_one();
                isDoEffect = true;

            }
            //更新弃牌
            Test_UIManager.GetComponent<Test_UI_Manger>().UIUpdate();
            if (isDoEffect)
            {
                ControlMessageBox.Instance.SetMessage("敌人出牌");
                if (EventAimator.GetComponent<MainSceneEvent>().ChooseCard)
                {

                    sequence.Append(EventAimator.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(80, 150, 0), 0.5f));
                    sequence.Append(EventAimator.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(1000, -250, 0), 0.3f)).OnComplete(() =>
                    {
                        Destroy(CardChoose);
                    });
                    //打出一张牌后，能用的牌减-1
                    CardManager.Instance.HowManyCardCanUse--;
                }
                GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)true);
            }

            //行动力TEXT更新
            Test_UIManager.GetComponent<Test_UI_Manger>().UIUpdate();
            CoreUIUpdate();

        }
    }

    /// <summary>
    /// 普通攻击
    /// </summary>
    private void CardID_0_OnAll_one()
    {
        AttackCorePunish();
        //检索这张卡片  并造成效果
        //CurHP -= 5;
        //抖动
        CoreReduceHp(3 + OurHero.GetComponent<Hero>().ATK);
      //  HP -= (5 + OurHero.GetComponent<Hero>().ATK - DEF);
        this.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);

        if(EnemyHero.GetComponent<Hero>().Present_MP + 3 <= 60)
        {
            EnemyHero.GetComponent<Hero>().AddMP(3);

        }
        else if (EnemyHero.GetComponent<Hero>().Present_MP + 3 > 60)
        {
            EnemyHero.GetComponent<Hero>().Present_MP = 60;
        }


        EventAimator.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }


    /// <summary>
    /// 攻击城堡受到小兵和英雄的惩罚
    /// </summary>
    private void AttackCorePunish()
    {
        //直接攻击核心会受到敌方的小兵的攻击伤害
        GameObject[] EnemyGroup = GameObject.FindGameObjectsWithTag("EnemyBatman");
        //敌方小兵攻击力总和
        int sum = 0;
        for (int i = 0; i < EnemyGroup.Length; i++)
        {
            if (EnemyGroup[i].GetComponent<BatMan>().CurHP > 0)
            {
                sum += EnemyGroup[i].GetComponent<BatMan>().Atk;
            }
        }
        sum += OurHero.GetComponent<Hero>().ATK;

        EnemyHero.GetComponent<Hero>().ReduceHP(sum);
    }
}
