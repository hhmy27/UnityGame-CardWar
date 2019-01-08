using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using System;
using UnityEngine.UI;

public class BatMan : MonoBehaviour
{
    [SerializeField]
    private Sprite[] Image;
    [SerializeField]
    private GameObject EnemyHero;
    public GameObject OurHero;
    public GameObject EnemyCore;
    public GameObject OurCore;
    [SerializeField]
    private GameObject EventGameObject;
    [SerializeField]
    private GameObject Test_UIManager;
    public GameObject AtkEffect;
    public GameObject AudioManager;
    //状态提升的UI提示
    //public GameObject OurHeroUpATKImage;
    //public GameObject OurHeroUpDEFImage;
    //public GameObject OurHeroUpHPImage;

    public GameObject EnemyHeroUpDEFImage;
    public GameObject EnemyHeroUpATKImage;
    public GameObject EnemyHeroUpHPImage;
    public GameObject DemageText;

    //血条
    public GameObject BatmanBloodSlider;
    //血量文本
    public GameObject BatmanBloodText;
    public GameObject ATKImageText;
    public GameObject RewardImageText;

    public int CurHP;
    public int Atk;
    public int AddHp = 0;
    public int AddAtk = 0;
    public int AddDef = 0;
    private Color nowcolr;
    //如果击杀小兵还有别的收益类型在这里补充
    public int Addwhich;
    private float AlphaValue = 1;
    private int LimitHP;
    private bool isAleardyAdd = false;
    //存放临时卡牌
    private GameObject CardChoose;
    //1是己方，0是敌方
    public int IsOwnBatman;
    private Vector3 RealDemageTextPosition;
    public Animator Animator;
    void Start()
    {
        //随机HP属性
        int random;
        random = UnityEngine.Random.Range(-1, 2);

        BatmanBloodSlider.SetActive(true);

        CurHP = 15 + random;
        Atk = 3;
        nowcolr = this.GetComponent<SpriteRenderer>().color;//获得color属性
        GenerateProperties();
        RealDemageTextPosition = DemageText.transform.position;
        BatmanBloodSlider.GetComponent<Slider>().maxValue = CurHP;
        BatmanBloodSlider.GetComponent<Slider>().value = CurHP;
        BatmanBloodText.GetComponent<Text>().text = CurHP.ToString();
        ATKImageText.GetComponent<Text>().text = Atk.ToString();
        Animator = GetComponent<Animator>();
    }


    private void Update()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(nowcolr.r, nowcolr.g, nowcolr.b, AlphaValue);




        //this.GetComponent<SpriteRenderer>().color.a = AlphaValue;
        if (CurHP <= 0)
        {
            BatmanBloodSlider.SetActive(false);

            if (isAleardyAdd == false)
            {
                isAleardyAdd = true;

                //TODO
                switch (Addwhich)
                {
                    case 0:

                        EnemyHero.GetComponent<Hero>().AddMaxHp(AddHp);
                        EnemyHeroUpHPImage.transform.GetChild(0).GetComponent<Text>().text = "HP";
                        EnemyHeroUpHPImage.SetActive(true);
                        Invoke("DelayFadeAllUpImage", 1f);


                        break;
                    case 1:
                        EnemyHero.GetComponent<Hero>().AddATK(AddAtk);

                        EnemyHeroUpATKImage.transform.GetChild(0).GetComponent<Text>().text = "ATK";
                        EnemyHeroUpATKImage.SetActive(true);
                        Invoke("DelayFadeAllUpImage", 1f);

                        break;
                    case 2:
                        EnemyHero.GetComponent<Hero>().AddDEF(AddDef);
                        EnemyHeroUpDEFImage.transform.GetChild(0).GetComponent<Text>().text = "DEF";
                        EnemyHeroUpDEFImage.SetActive(true);
                        Invoke("DelayFadeAllUpImage", 1f);

                        break;
                }

            }
            //播放死亡特效，隐藏小兵，下一回合再显示
            DOTween.To(() => AlphaValue, x => AlphaValue = x, 0, 1f);

        }
        else
        {
            BatmanBloodSlider.SetActive(true);
            if (AlphaValue != 1)
            {
                DOTween.To(() => AlphaValue, x => AlphaValue = x, 1, 0.5f);
            }
            //AlphaValue = 1;
            isAleardyAdd = false;
        }
    }
    /// <summary>
    /// 小兵属性随着回合增加而增加
    /// </summary>
    public void GrowBatman()
    {
        //随机HP点数
        int random;
        random = UnityEngine.Random.Range(-2, 3);
        //HP会被打成负的，所以要清零
        CurHP = 15;
        Atk = 3;
        CurHP += EventGameObject.GetComponent<MainSceneEvent>().Round + random;
        LimitHP = CurHP;
        Atk += EventGameObject.GetComponent<MainSceneEvent>().Round;
        ATKImageText.GetComponent<Text>().text = Atk.ToString();
        BatmanBloodSlider.GetComponent<Slider>().maxValue = CurHP;
        BatmanBloodSlider.GetComponent<Slider>().value = CurHP;
        BatmanBloodText.GetComponent<Text>().text = CurHP.ToString();
        GenerateProperties();
    }

    /// <summary>
    /// 给敌方英雄增加奖励属性
    /// </summary>
    public void GenerateProperties()
    {
        //实例化时随机给予收益属性

        Addwhich = UnityEngine.Random.Range(0, 3);
        switch (Addwhich)
        {
            case 0:
                AddHp = 2;
                RewardImageText.GetComponent<Text>().text = "HP";
                break;
            case 1:
                AddAtk = 2;
                RewardImageText.GetComponent<Text>().text = "ATK";

                break;
            case 2:
                AddDef = 2;
                RewardImageText.GetComponent<Text>().text = "DEF";

                break;
            default:
                break;
        }
    }

    public void CardOnthis(Card card)
    {
        Sequence sequence = DOTween.Sequence();
        bool isDoEffect = false;
        //如果行动力为0，就不能出牌
        if (CardManager.Instance.HowManyCardCanUse > 0)
        {
            CardChoose = EventGameObject.GetComponent<MainSceneEvent>().ChooseCard;
            if (CurHP > 0)
            {
                if (card.CardID == 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_0_OnAll_one();
                    isDoEffect = true;
                }
            }
            if (card.CardID == 1)
            {
                //怒气消耗
                if (EnemyHero.GetComponent<Hero>().Present_MP - 15 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_1_OnBatman_OnHero_AOE();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }

            //搏命
            if (card.CardID == 2)
            {
                //怒气消耗
                if (EnemyHero.GetComponent<Hero>().Present_MP - 9 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_2_OnBatman_OnHero_One();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }
            //双杀
            if (card.CardID == 3)
            {
                //怒气消耗
                if (EnemyHero.GetComponent<Hero>().Present_MP - 10 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_3_OnBatman_OnHero_double();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }

            if (card.CardID == 4)
            {
                if (EnemyHero.GetComponent<Hero>().Present_MP - 8 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_4_OnBatman_OnHero_One();
                    isDoEffect = true;

                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }
            if (card.CardID == 5)
            {
                if (EnemyHero.GetComponent<Hero>().Present_MP - 10 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_5_OnHero_one();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }
            if (card.CardID == 6)
            {
                if (EnemyHero.GetComponent<Hero>().Present_MP - 10 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_6_OnHero_one();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }

            if (card.CardID == 7)
            {
                if (EnemyHero.GetComponent<Hero>().Present_MP - 8 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_7_OnCore_one();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }

            if (card.CardID == 9)
            {
                if (EnemyHero.GetComponent<Hero>().Present_MP - 6 >= 0)
                {
                    CardManager.Instance.AddCardToLoseCardList(card);
                    CardID_9_OnOurHero_one();
                    isDoEffect = true;
                }
                else
                {

                    ControlMessageBox.Instance.SetMessage("怒气值不足");

                }
            }
            if (card.CardID == 14)
            {
                CardID_14_OnOurHero_one();
                isDoEffect = true;

            }
            if (card.CardID == 16)
            {
                CardID_16_OnHero_one();
                isDoEffect = true;
            }
            if (card.CardID == 15)
            {
                CardID_15_OnHero_one();
                isDoEffect = true;
            }

            if (card.CardID == 17)
            {
                CardID_17_OnHero_one();
                isDoEffect = true;
            }
            //更新弃牌
            Test_UIManager.GetComponent<Test_UI_Manger>().UIUpdate();
            if (isDoEffect)
            {
                ControlMessageBox.Instance.SetMessage("敌人出牌");
                if (EventGameObject.GetComponent<MainSceneEvent>().ChooseCard)
                {

                    sequence.Append(EventGameObject.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(80, 150, 0), 0.5f));
                    sequence.Append(EventGameObject.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(1000, -250, 0), 0.3f)).OnComplete(() =>
                    {



                        Destroy(CardChoose);
                    });
                    //打出一张牌后，能用的牌减-1

                    GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)true);
                    CardManager.Instance.HowManyCardCanUse--;
                }
            }



            //行动力TEXT更新
            Test_UIManager.GetComponent<Test_UI_Manger>().UIUpdate();
        }
        if (CardManager.Instance.HowManyCardCanUse <= 0)
        {
            if (CardManager.Instance.HowManyCardCanUse-- <= -1)
            {

                ControlMessageBox.Instance.SetMessage("行动力不足");

            }
        }

    }

    /// <summary>
    /// 小兵受到攻击减少血量
    /// </summary>
    /// <param name="num"></param>
    public void BatmanReduceHP(int num)
    {
        Animator.SetTrigger("Demage");

        CurHP -= num;
        DemageText.transform.position = RealDemageTextPosition;
        DemageText.GetComponent<Text>().text = "-" + num;

        DemageText.SetActive(true);
        DemageText.transform.position = new Vector3(DemageText.transform.position.x, DemageText.transform.position.y + 30, 0);
        DemageText.transform.DOMoveY(DemageText.transform.position.y + 30, 1f).OnComplete(() => DemageText.SetActive(false));

        BatmanBloodSlider.GetComponent<Slider>().value = CurHP;
        BatmanBloodText.GetComponent<Text>().text = CurHP.ToString();
        //发送给主场景事件脚本 提示有小兵血量减少


        EventGameObject.GetComponent<MainSceneEvent>().BatmanDemage();

    }
    /// <summary>
    /// 播放攻击动画
    /// </summary>
    public void BatmanAtkCore()
    {
        Animator.SetTrigger("Atk");
    }

    /// <summary>
    /// 普通攻击
    /// </summary>
    private void CardID_0_OnAll_one()
    {
        //检索这张卡片  并造成效果
        //CurHP -= 5 + EnemyHero.GetComponent<Hero>().ATK;

        BatmanReduceHP(3 + EnemyHero.GetComponent<Hero>().ATK);

        //抖动
        this.GetComponent<AudioSource>().Play();

        //this.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
        GameObject effect = GameObject.Instantiate(AtkEffect, this.gameObject.transform.position, Quaternion.identity);
        if (EnemyHero.GetComponent<Hero>().Present_MP + 3 <= 60)
        {
            EnemyHero.GetComponent<Hero>().ReduceMP(-3);

        }
        else if (EnemyHero.GetComponent<Hero>().Present_MP + 3 > 60)
        {
            EnemyHero.GetComponent<Hero>().Present_MP = 60;
        }


        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }
    /// <summary>
    /// 挥斩
    /// </summary>
    public void CardID_1_OnBatman_OnHero_AOE()
    {
        AudioManager.GetComponents<AudioSource>()[0].Play();
        Transform EnemyBatmanGroup = GameObject.Find("EnemyBatmanGroup").transform;
        //敌方英雄
        // GameObject EnemyHero = GameObject.FindGameObjectWithTag("EnemyHero");
          EnemyBatmanGroup.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);

        //敌人小兵
        GameObject[] EnemyBatman = GameObject.FindGameObjectsWithTag("EnemyBatman");
        for (int i = 0; i < EnemyBatman.Length; i++)
        {
            EnemyBatman[i].GetComponent<BatMan>().BatmanReduceHP(4 + EnemyHero.GetComponent<Hero>().ATK);
            GameObject effect = GameObject.Instantiate(AtkEffect, EnemyBatman[i].gameObject.transform.position, Quaternion.identity);
        }

        OurHero.GetComponent<Hero>().ReduceHP(4 + EnemyHero.GetComponent<Hero>().ATK);

        //OurHero.GetComponent<Hero>().ReduceHP(4 + EnemyHero.GetComponent<Hero>().ATK - OurHero.GetComponent<Hero>().DEF);
        //OurHero.GetComponent<Hero>().Present_HP -= 4 + EnemyHero.GetComponent<Hero>().ATK - OurHero.GetComponent<Hero>().DEF;
        OurHero.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);

        //EnemyHero.GetComponent<Hero>().Present_MP -= 15;
        EnemyHero.GetComponent<Hero>().ReduceMP(15);
        //使用卡牌+1
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }
    ///// <summary>
    ///// 搏命
    ///// </summary>
    private void CardID_2_OnBatman_OnHero_One()
    {
        AudioManager.GetComponents<AudioSource>()[7].Play();

        GameObject EnemyHero = GameObject.FindGameObjectWithTag("EnemyHero");
        GameObject OurHero = GameObject.FindGameObjectWithTag("Hero");
        int i = 0;

        i = (OurHero.GetComponent<Hero>().HP - OurHero.GetComponent<Hero>().Present_HP) / 5;

        EnemyHero.GetComponent<Hero>().ReduceHP(3 + OurHero.GetComponent<Hero>().ATK + i * 2);
        //this.Present_HP -= (3 + EnemyHero.GetComponent<Hero>().ATK + i * 2);

        EnemyHero.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
        //AttackHeroPunish();

        OurHero.GetComponent<Hero>().ReduceMP(9);

        //EnemyHero.GetComponent<Hero>().Present_MP -= 9;
        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }

    /// <summary>
    /// 双杀, 消耗10点怒气，对敌方英雄造成5(+ATK)的伤害,对任意一个小兵造成2(+ATK)点伤害
    /// </summary>
    private void CardID_3_OnBatman_OnHero_double()
    {
        AudioManager.GetComponents<AudioSource>()[4].Play();

        //对敌方英雄造成5(+ATK)的伤害
        OurHero.GetComponent<Hero>().ReduceHP(5 + EnemyHero.GetComponent<Hero>().ATK);
        GameObject[] EnemyBatman = new GameObject[4];
        EnemyBatman = GameObject.FindGameObjectsWithTag("EnemyBatman");
        int j = 0;

        //遍历数组 找到一个血量大于零的小兵进行攻击
        for (j = 0; j < EnemyBatman.Length; j++)
        {
            if (EnemyBatman[j].GetComponent<BatMan>().CurHP > 0)
            {
                break;
            }
        }
        if (j == EnemyBatman.Length)
        {

        }
        else
        {

            EnemyBatman[j].GetComponent<BatMan>().BatmanReduceHP(2 + EnemyHero.GetComponent<Hero>().ATK);

        }
        //EnemyHero.GetComponent<Hero>().Present_MP -= 9;
        EnemyHero.GetComponent<Hero>().ReduceMP(9);
        OurHero.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }


    /// <summary>
    /// 拔刀
    /// </summary>
    private void CardID_4_OnBatman_OnHero_One()
    {
        AudioManager.GetComponents<AudioSource>()[8].Play();

        //敌人小兵
        GameObject[] EnemyBatman = new GameObject[10];
        EnemyBatman = GameObject.FindGameObjectsWithTag("EnemyBatman");
        //for (int i = 0; i < EnemyBatman.Length; i++)
        //{
        //    EnemyBatman[i].GetComponent<BatMan>().CurHP -= 4 + Hero.GetComponent<Hero>().ATK;
        //}

        //敌方英雄
        GameObject EnemyHero = GameObject.FindGameObjectWithTag("EnemyHero");
        GameObject OurHero = GameObject.FindGameObjectWithTag("Hero");

        //初始化攻击目标列表
        List<int> AtkTragetList = new List<int>(5);
        for (int i = 0; i < 5; i++)
        {
            AtkTragetList.Add(i);
        }
        int AtkTraget = 0;
        //解除挥剑死循环的bug
        int num = 0;
        while (AtkTraget != 3)
        {

            int j = UnityEngine.Random.Range(0, AtkTragetList.Count);
            if (AtkTragetList[j] == 4)
            {
                //敌方英雄受到攻击
                EnemyHero.GetComponent<Hero>().ReduceHP(5 + EnemyHero.GetComponent<Hero>().ATK);
                //  EnemyHero.GetComponent<Hero>().Present_HP -= 5 + EnemyHero.GetComponent<Hero>().ATK;
                EnemyHero.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
                AtkTraget++;
                AtkTragetList.Remove(AtkTragetList[j]);
            }
            else
            {
                if (EnemyBatman[AtkTragetList[j]].GetComponent<BatMan>().CurHP > 0)
                {
                    EnemyBatman[AtkTragetList[j]].GetComponent<BatMan>().BatmanReduceHP(5 + EnemyHero.GetComponent<Hero>().ATK);
                    GameObject effect = GameObject.Instantiate(AtkEffect, EnemyBatman[AtkTragetList[j]].gameObject.transform.position, Quaternion.identity);

                     EnemyBatman[AtkTragetList[j]].GetComponent<BatMan>().transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);

                    AtkTraget++;
                    AtkTragetList.Remove(AtkTragetList[j]);
                }
            }

            if (num++ > 200)
            {
                break;
            }

        }
        //OurHero.GetComponent<Hero>().Present_MP -= 8;
        OurHero.GetComponent<Hero>().ReduceMP(8);
        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }
    /// <summary>
    /// 穿刺，对敌方英雄造成最大生命值10％的伤害
    /// </summary>
    private void CardID_5_OnHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[6].Play();

        OurHero.GetComponent<Hero>().ReduceHP((int)(OurHero.GetComponent<Hero>().HP * 0.1));
        // OurHero.GetComponent<Hero>().Present_HP -= (int)(OurHero.GetComponent<Hero>().HP * 0.1);
        OurHero.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
        //EnemyHero.GetComponent<Hero>().Present_MP -= 10;
        EnemyHero.GetComponent<Hero>().ReduceMP(10);
    }

    /// <summary>
    /// 斩杀
    /// </summary>
    private void CardID_6_OnHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[5].Play();

        if (OurHero.GetComponent<Hero>().Present_HP < OurHero.GetComponent<Hero>().HP * 0.15)
        {
            OurHero.GetComponent<Hero>().ReduceHP(-999);
            //血量变为-1 斩杀
            //OurHero.GetComponent<Hero>().Present_HP = -1;
            //Debug.Log("敌方英雄血量:"+OurHero.GetComponent<Hero>().Present_HP);
            OurHero.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
            EnemyHero.GetComponent<Hero>().ReduceMP(10);
           // EnemyHero.GetComponent<Hero>().Present_MP -= 10;

            EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;

        }
        else
        {
            OurHero.GetComponent<Hero>().ReduceHP(3 + EnemyHero.GetComponent<Hero>().ATK);
            //OurHero.GetComponent<Hero>().Present_HP -= 3+EnemyHero.GetComponent<Hero>().ATK;
            OurHero.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
            EnemyHero.GetComponent<Hero>().ReduceMP(10);
           // EnemyHero.GetComponent<Hero>().Present_MP -= 10;
            EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;

        }
        //else
    }

    /// <summary>
    /// 攻城 只作用于城堡
    /// </summary>
    private void CardID_7_OnCore_one()
    {
        AudioManager.GetComponents<AudioSource>()[2].Play();

        OurCore.GetComponent<Core>().CoreReduceHp(EnemyHero.GetComponent<Hero>().ATK + 15);
        //OurCore.GetComponent<Core>().HP -= EnemyHero.GetComponent<Hero>().ATK + 15;
        OurCore.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
        EnemyHero.GetComponent<Hero>().ReduceMP(8);
       // EnemyHero.GetComponent<Hero>().Present_MP -= 8;
        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }

    /// <summary>
    /// 包扎，消耗6点怒气,恢复20点生命值
    /// </summary>
    private void CardID_9_OnOurHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[1].Play();

        EnemyHero.GetComponent<Hero>().AddHP(20);
        if (EnemyHero.GetComponent<Hero>().Present_HP > EnemyHero.GetComponent<Hero>().HP)
        {
            EnemyHero.GetComponent<Hero>().Present_HP = EnemyHero.GetComponent<Hero>().HP;
        }

        EnemyHero.GetComponent<Hero>().ReduceMP(6);

        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;

    }
    /// <summary>
    /// 神眷 恢复50％已损失生命值
    /// </summary>
    private void CardID_14_OnOurHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[1].Play();

        //当前HP恢复已损失的HP的50%
        EnemyHero.GetComponent<Hero>().AddHP((int)((EnemyHero.GetComponent<Hero>().HP - EnemyHero.GetComponent<Hero>().Present_HP) * 0.5));
        // EnemyHero.GetComponent<Hero>().Present_HP += (int)((EnemyHero.GetComponent<Hero>().HP- EnemyHero.GetComponent<Hero>().Present_HP) * 0.5);
        if (EnemyHero.GetComponent<Hero>().Present_HP > EnemyHero.GetComponent<Hero>().HP)
        {
            EnemyHero.GetComponent<Hero>().Present_HP = EnemyHero.GetComponent<Hero>().HP;
        }
        //使用卡牌+1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }

    /// <summary>
    /// 出击 敌方英雄造成我方小兵攻击力总和的伤害,随机损失一名小兵
    /// </summary>
    private void CardID_17_OnHero_one()
    {
        EnemyHero.GetComponent<Hero>().ReduceMP(0);

        GameObject[] OurBatman = GameObject.FindGameObjectsWithTag("OurBatman");
        int sum = 0;
        for (int i = 0; i < OurBatman.Length; i++)
        {
            sum += OurBatman[i].GetComponent<BatMan>().Atk;
        }
        OurHero.GetComponent<Hero>().ReduceHP(sum);


        int j = 0;
        for (j = 0; j < OurBatman.Length; j++)
        {
            if (OurBatman[j].GetComponent<BatMan>().CurHP > 0)
            {
                break;
            }
        }
        OurBatman[j].GetComponent<BatMan>().CurHP = -1;
        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }
    /// <summary>
    /// 重整 获得防御力的恢复
    /// </summary>
    private void CardID_15_OnHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[4].Play();

        int DEF = EnemyHero.GetComponent<Hero>().DEF;
        EnemyHero.GetComponent<Hero>().AddHP(DEF);

        EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }

    /// <summary>
    /// 嗜血 随机击杀一名小兵
    /// </summary>
    private void CardID_16_OnHero_one()
    {
        AudioManager.GetComponents<AudioSource>()[3].Play();

        EnemyHero.GetComponent<Hero>().ReduceMP(0);

        GameObject[] OurBatman = GameObject.FindGameObjectsWithTag("OurBatman");
        GameObject[] EnemyBatman = GameObject.FindGameObjectsWithTag("EnemyBatman");
        GameObject[] Batman = new GameObject[OurBatman.Length + EnemyBatman.Length];
        OurBatman.CopyTo(Batman, 0);
        EnemyBatman.CopyTo(Batman, OurBatman.Length);
        int i = 0;
        int j = 0;
        int EnemyHpSum = 0;
        int OurHpSum = 0;


        foreach(var x in OurBatman)
        {
            if (x.GetComponent<BatMan>().CurHP > 0)
            {
                OurHpSum += x.GetComponent<BatMan>().CurHP;
            }
        }
        foreach (var y in EnemyBatman)
        {
            if (y.GetComponent<BatMan>().CurHP > 0)
            {
                EnemyHpSum += y.GetComponent<BatMan>().CurHP;
            }
        }

        if (OurHpSum > EnemyHpSum)
        {
            for (i = 0; i < OurBatman.Length; i++)
            {
                if (OurBatman[i].GetComponent<BatMan>().CurHP > 0)
                {
                    break;
                }
            }

            if (i != 4)
            {
                int HP = OurBatman[i].GetComponent<BatMan>().CurHP;

                OurBatman[i].GetComponent<BatMan>().CurHP = -1;
                EnemyHero.GetComponent<Hero>().AddHP(HP);

            }
            EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
        }
        else
        {
            for (j = 0; j < EnemyBatman.Length; j++)
            {
                if (EnemyBatman[j].GetComponent<BatMan>().CurHP > 0)
                {
                    break;
                }
            }

            if (j != 4)
            {
                int HP = EnemyBatman[j].GetComponent<BatMan>().CurHP;

                EnemyBatman[j].GetComponent<BatMan>().CurHP = -1;
                EnemyHero.GetComponent<Hero>().AddHP(HP);

            }


            EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
        }



        //for (i = 0; i < Batman.Length; i++)
        //{
        //    if (Batman[i].GetComponent<BatMan>().CurHP > 0)
        //    {
        //        break;
        //    }
        //}
        ////int HP = Batman[i].GetComponent<BatMan>().CurHP;

        //if (i != 4)
        //{
        //    Batman[i].GetComponent<BatMan>().CurHP = -1;

        //}
        //EnemyHero.GetComponent<Hero>().Present_HP+= HP;
        //EventGameObject.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;

    }




    /// <summary>
    /// 隐藏所有的UP提示
    /// </summary>
    private void DelayFadeAllUpImage()
    {
        EnemyHeroUpHPImage.SetActive(false);
        EnemyHeroUpATKImage.SetActive(false);
        EnemyHeroUpDEFImage.SetActive(false);

    }



}
