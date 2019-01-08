using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;
/// <summary>
/// 战斗场景中的Hero
/// 
/// </summary>
public class Hero : MonoBehaviour
{
    //每次进入战斗场景通过这个临时类来管理战斗逻辑

    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int Present_HP;
    public int Present_MP;
    public GameObject Event;
    public GameObject Test_UIManager;
    private GameObject CardChoose;
    public GameObject EnemyHero;
    public GameObject[] OurBatman;
    public GameObject[] EnemyBatman;
    public GameObject HpDemageText;
    private Vector3 RealDemageTextPosition;
    //1是己方英雄，0是敌方英雄
    public int own;
    private Color nowcolor;
    private float AlphaValue = 1;
    private bool AladyDie = false;
    private Animator Animator;


    private void Start()
    {
        CreatHero();
        nowcolor = GameObject.FindGameObjectWithTag("Hero").GetComponent<SpriteRenderer>().color;
        AladyDie = false;
        RealDemageTextPosition=HpDemageText.transform.position;
        Animator = GetComponent<Animator>();
    }




    public void CreatHero()
    {
        if (this.gameObject.tag == "Hero")
        {
            HP = PlayManager.Instance.limit_HP;
            MP = PlayManager.Instance.limit_MP;
            ATK = PlayManager.Instance.Attack;
            DEF = PlayManager.Instance.Defend;
            Present_HP = HP;
            Present_MP = 0;
        }
        if (this.gameObject.tag == "EnemyHero")
        {
            HP = 100 + (GameManager.Instance.GameLevel ) * 25;
            Present_HP = HP;
            ATK = GameManager.Instance.GameLevel ;
            DEF = GameManager.Instance.GameLevel;
        }
    }

    private void Update()
    {
        if (Present_HP < 0)
        {
            if (this.tag == "Hero")
            {
                if (AladyDie == false)
                {
                    Test_UIManager.GetComponent<Test_UI_Manger>().HeroFade();
                    foreach (var i in OurBatman)
                    {
                        i.GetComponent<BatMan>().CurHP = -1;
                    }

                    Invoke( "ReduceReward_half",2.5f);
                    //TODO
                    //数值惩罚
                    //敌方加强
                    //播放死亡动画
                    EnemyHero.GetComponent<Hero>().ATK += 5;
                    //回合强制结束
                    Test_UIManager.GetComponent<Test_UI_Manger>().OnBtnOverClick();
                    Invoke("DelayHeroRestart", 2.5f);
                    AladyDie = true;
                }
                ControlMessageBox.Instance.SetMessage("玩家死亡");
            }
            if (this.tag == "EnemyHero")
            {
                if (AladyDie == false)
                {

                    Test_UIManager.GetComponent<Test_UI_Manger>().EnemyHeroFade();
                    foreach (var i in OurBatman)
                    {
                        i.GetComponent<BatMan>().CurHP = -1;
                    }

                    Invoke("ReduceReward_half", 3f);
                    //TODO
                    //数值惩罚
                    //敌方加强
                    //播放死亡动画
                    EnemyHero.GetComponent<Hero>().ATK += 5;
                    //回合强制结束
                    Test_UIManager.GetComponent<Test_UI_Manger>().OnBtnOverClick();
                    Invoke("DelayHeroRestart", 2f);
                    AladyDie = true;
                }
                ControlMessageBox.Instance.SetMessage("敌人死亡");
            }

        }
    }


    //让英雄满血
    private void DelayHeroRestart()
    {
        Present_HP = HP;
        AladyDie = false;
    }

    /// <summary>
    /// 如果英雄死亡 不会获得小兵属性加成
    /// </summary>
    public void ReduceReward()
    {
        foreach (var i in EnemyBatman)
        {
            if (i.GetComponent<BatMan>().CurHP > 0)
            {
                switch (i.GetComponent<BatMan>().Addwhich)
                {
                    case 0:
                        this.AddHP(-i.GetComponent<BatMan>().AddHp);
                        break;
                    case 1:
                        this.AddATK(-i.GetComponent<BatMan>().AddAtk);
                        break;
                    case 2:
                        this.AddDEF(-i.GetComponent<BatMan>().AddDef);
                        break;

                }
            }
        }
    }
    public void ReduceReward_half()
    {
        foreach (var i in EnemyBatman)
        {
            if (i.GetComponent<BatMan>().CurHP > 0)
            {
                switch (i.GetComponent<BatMan>().Addwhich)
                {
                    case 0:
                        this.AddHP(-(i.GetComponent<BatMan>().AddHp) / 2);
                        break;
                    case 1:
                        this.AddATK(-(i.GetComponent<BatMan>().AddAtk) / 2);
                        break;
                    case 2:
                        this.AddDEF(-(i.GetComponent<BatMan>().AddDef) / 2);
                        break;

                }
            }
        }
    }

    public void AddMaxHp(int num)
    {
        HP += num;
        Present_HP += num;
        if (Present_HP > HP)
        {
            Present_HP = HP;
        }
        HpDemageText.transform.position = RealDemageTextPosition;
        HpDemageText.GetComponent<Text>().text = "+" + (num).ToString();
        HpDemageText.SetActive(true);
        // DemageText.transform.position = new Vector3(DemageText.transform.position.x, DemageText.transform.position.y+30, 0);
        HpDemageText.transform.DOMoveY(HpDemageText.transform.position.y + 30, 1f).OnComplete(() => HpDemageText.SetActive(false));
    }

    public void AddHP(int num)
    {
        //HP += num;
        Present_HP += num;
        if (Present_HP > HP)
        {
            Present_HP = HP;
        }
        HpDemageText.transform.position = RealDemageTextPosition;
        HpDemageText.GetComponent<Text>().text = "+"+(num).ToString();
        HpDemageText.SetActive(true);
        // DemageText.transform.position = new Vector3(DemageText.transform.position.x, DemageText.transform.position.y+30, 0);
        HpDemageText.transform.DOMoveY(HpDemageText.transform.position.y + 30, 1f).OnComplete(() => HpDemageText.SetActive(false));
    }
 
    public void AddATK(int num)
    {
        ATK += num;
    }
    public void AddDEF(int num)
    {
        DEF += num;
    }



    /// <summary>
    /// 减少血量时调用
    /// </summary>
    /// <param name="num"></param>
    public void ReduceHP(int num)
    {
        if (num != 0)
        {
            //只有传进来的伤害大于DEF才会扣血
            if (num > DEF)
            {
                Animator.SetTrigger("Demage");
                
                //如果传进来的数字大于防御力，当前血量就减去数字减防御力

                Present_HP -= (num-DEF);
              
                //TODO 扣血的显示
                HpDemageText.transform.position = RealDemageTextPosition;
                HpDemageText.GetComponent<Text>().text = "-"+((num-DEF)).ToString();
                HpDemageText.SetActive(true);
                // DemageText.transform.position = new Vector3(DemageText.transform.position.x, DemageText.transform.position.y+30, 0);
                HpDemageText.transform.DOMoveY(HpDemageText.transform.position.y + 30, 1f).OnComplete(() => HpDemageText.SetActive(false));
            }
            else
            {
                //如果num没有超过防御力，则扣一滴血
                Animator.SetTrigger("Demage");

                Present_HP -= 1;

                HpDemageText.GetComponent<Text>().text = (-1).ToString();
                HpDemageText.transform.position = RealDemageTextPosition;
                HpDemageText.SetActive(true);
                HpDemageText.transform.DOMoveY(HpDemageText.transform.position.y + 30, 1f).OnComplete(() => HpDemageText.SetActive(false));

            }

        }
        

    }


    /// <summary>
    /// 减少怒气时调用
    /// </summary>
    /// <param name="num"></param>
    public void ReduceMP(int num)
    {
        Animator.SetTrigger("Atk");
        Present_MP -= num;
    }

    /// <summary>
    /// 打出普通攻击 增加怒气
    /// </summary>
    /// <param name="num"></param>
    public void AddMP(int num)
    {
        Animator.SetTrigger("Atk");
        Present_MP += num;
    }

    public void CardOnthis(Card card)
    {
        Sequence sequence = DOTween.Sequence();
        bool isDoEffect = false;
        //如果行动力为0，就不能出牌
        if (CardManager.Instance.HowManyCardCanUse > 0 && HP > 0)
        {
            CardChoose = Event.GetComponent<MainSceneEvent>().ChooseCard;
            if (card.CardID == 0)

            {
                CardManager.Instance.AddCardToLoseCardList(card);
                CardID_0_OnAll_one();
                isDoEffect = true;
            }
            //if (card.CardID == 2)
            //{
            //    //怒气消耗
            //    if (EnemyHero.GetComponent<Hero>().Present_MP - 9 > 0)
            //    {
            //        CardManager.Instance.AddCardToLoseCardList(card);
            //        CardID_2_OnBatman_OnHero_One();
            //        isDoEffect = true;

            //    }
            //}
            //更新弃牌
            Test_UIManager.GetComponent<Test_UI_Manger>().UIUpdate();
            if (isDoEffect)
            {
                ControlMessageBox.Instance.SetMessage("敌人出牌");
                if (Event.GetComponent<MainSceneEvent>().ChooseCard)
                {

                    sequence.Append(Event.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(80, 150, 0), 0.5f));
                    sequence.Append(Event.GetComponent<MainSceneEvent>().ChooseCard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(1000, -250, 0), 0.3f)).OnComplete(() =>
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
        }
        if (CardManager.Instance.HowManyCardCanUse <= 0)
        {
            if (CardManager.Instance.HowManyCardCanUse-- <= -1)
            {

                ControlMessageBox.Instance.SetMessage("行动力不足");

            }
        }

    }
    ///// <summary>
    ///// 搏命
    ///// </summary>
    //private void CardID_2_OnBatman_OnHero_One()
    //{
    //    int i = 0;
    //    i = (EnemyHero.GetComponent<Hero>().HP - EnemyHero.GetComponent<Hero>().Present_HP) / 5;

    //    this.ReduceHP(3 + EnemyHero.GetComponent<Hero>().ATK + i * 2);
    //    //this.Present_HP -= (3 + EnemyHero.GetComponent<Hero>().ATK + i * 2);

    //    this.transform.DOShakePosition(1, new Vector3(2, 1, 0), 25);
    //    AttackHeroPunish();
    //    EnemyHero.GetComponent<Hero>().ReduceHP(9);
    //   //EnemyHero.GetComponent<Hero>().Present_MP -= 9;
    //    //使用卡牌+1;
    //    Event.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    //}

    /// <summary>
    /// 普通攻击
    /// </summary>
    private void CardID_0_OnAll_one()
    {
        //加怒气
        GameObject OurHero = GameObject.FindGameObjectWithTag("Hero");
        //60是怒气上限
        if(OurHero.GetComponent<Hero>().Present_MP+3<=60)
        {
            OurHero.GetComponent<Hero>().ReduceMP(-3);

            //OurHero.GetComponent<Hero>().Present_MP += 3;
        }
        else if (OurHero.GetComponent<Hero>().Present_MP + 3 > 60)
        {
            OurHero.GetComponent<Hero>().Present_MP = 60;
        }

        AttackHeroPunish();

        if (3 + EnemyHero.GetComponent<Hero>().ATK - this.DEF > 0)
        {
           // Debug.Log("3 + ATK - DEF: " +( 3 + EnemyHero.GetComponent<Hero>().ATK - this.DEF).ToString());
            ReduceHP((3 + EnemyHero.GetComponent<Hero>().ATK));
            //this.Present_HP -= (5 + EnemyHero.GetComponent<Hero>().ATK - this.DEF);
        }
        //this.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
        Event.GetComponent<MainSceneEvent>().HowManyCardHadUsed++;
    }



    /// <summary>
    /// 直接攻击英雄会受到敌方的小兵的攻击伤害
    /// </summary>
    private void AttackHeroPunish()
    {

        //敌方小兵攻击力总和
        int sum = 0;
        for (int i = 0; i < OurBatman.Length; i++)
        {
            if (OurBatman[i].GetComponent<BatMan>().CurHP > 0)
            {
                sum += OurBatman[i].GetComponent<BatMan>().Atk;
            }
        }

        //Animator.SetTrigger("Atk");
        Debug.Log("sum:"+sum);
        //被攻击的英雄调用此方法 让敌方英雄减血
        
        EnemyHero.GetComponent<Hero>().ReduceHP(sum);
        EnemyHero.GetComponent<Hero>().Animator.SetTrigger("Atk");
        //EnemyHero.GetComponent<Hero>().ReduceHP(sum);
    }
}
