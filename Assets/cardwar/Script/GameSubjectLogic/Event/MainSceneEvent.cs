using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 控制主场景的所有事件
/// </summary>
public class MainSceneEvent : MonoBehaviour
{
    public int Round = 0;
    //1是敌方赢 0是我方赢
    private int WhoWin = -1;
    public GameObject EnemyCore;
    public GameObject OwnCore;
    public GameObject EnemyBtGroup;
    public GameObject OwnBtGroup;
    private BatMan[] OwnBatMen;
    private BatMan[] EnemyBatMen;
    public GameObject EnemyHero;
    public GameObject OurHero;

    //小兵的攻击和血量信息
    public GameObject[] BatmanSumAtkHp;

    //当前选中的牌
    public Card NowChooseCard;

    public GameObject ChooseCard;
    public int HowManyCardHadUsed = 0;
    //已经被抽取的牌
    public bool aCardhadbeenChoose = false;
    /// <summary>
    /// 回合结束计算
    /// </summary>
    ///
    public void Start()
    {
        WhoWin = -1;
        CardManager.Instance.CreatCardPrefeb();

    }

    /// <summary>
    /// 当有小兵血量减少时 小兵脚本发送给此脚本更新信息
    /// </summary>
    public void BatmanDemage()
    {
        foreach (var i in BatmanSumAtkHp)
        {
            i.GetComponent<BatmanSumAtkHp>().ResetSumInfor();
        }
    }



    public void OnEndCalculate()
    {



        //TODO回合结束计算
        //小兵清算
        BtmanAtk();
        //战斗动画
        //一方小兵进入，另一方小兵退出
        
        //敌方赢
        if (WhoWin == 1)
        {
            foreach(var i in EnemyBatMen)
            {
                i.BatmanAtkCore();
            }
        }
        else if (WhoWin==0)
        {
            //我方赢
            foreach (var i in   OwnBatMen)
            {
                i.BatmanAtkCore();
            }
        }
        //Debug.Log("whowin:" + WhoWin);
        this.GetComponent<OnEndGroupAnimation>().EndRound(WhoWin);
        //小兵攻击动画的播放  只要是改变动画机的状态 


        //防御塔秒杀小兵
        //获胜方退场 
        //发送给核心，进行加减血的运算
        Invoke("SendMessageToCore", 1.5f);

    }
    
    /// <summary>
    /// 传入0，我方获得属性减一半，传入1，敌方获得属性减一半
    /// </summary>
    /// <param name="whoReduce"></param>
    public void ReduceReward(int whoReduce)
    {
        //我方获得属性减一半
        if (whoReduce == 1)
        {
            foreach (var i in EnemyBatMen)
            {
                if (i.GetComponent<BatMan>().CurHP > 0)
                {
                    switch (i.GetComponent<BatMan>().Addwhich)
                    {
                        case 0:
                            OurHero.GetComponent<Hero>().AddHP(-(i.GetComponent<BatMan>().AddHp / 2));
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
        //敌方获得属性减一半
        if (whoReduce == 0)
        {
            foreach (var i in OwnBatMen)
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

    //小兵互相攻击动画
    //在此修改WhoWin的值
    public int BtmanAtk()
    {
        int SumOwnHP = 0;
        int SumEnemyHP = 0;
        OwnBatMen = OwnBtGroup.GetComponentsInChildren<BatMan>();
        //判断是否有小兵存活，  如果有就加上小兵攻击力
        //for (int i = 0; i < OwnBatMen.Length; i++)
        //{
        //    if (OwnBatMen[i].CurHP > 0)
        //    {
        //        SumOwnHP += OwnBatMen[i].CurHP;
        //    }

        //}
        foreach(var i in OwnBatMen)
        {
            if (i.CurHP > 0)
            {
                SumOwnHP += i.CurHP;
            }
        }

        EnemyBatMen = EnemyBtGroup.GetComponentsInChildren<BatMan>();
        //for (int i = 0; i < EnemyBatMen.Length; i++)
        //{
        //    if (EnemyBatMen[i].CurHP > 0)
        //    {
        //        SumEnemyHP += EnemyBatMen[i].CurHP;
        //    }
        //}
        foreach (var i in EnemyBatMen)
        {
            if (i.CurHP > 0)
            {
                SumEnemyHP += i.CurHP;
            }
        }



        //比较双方HP大小，修改WhoWin
        if (SumOwnHP < SumEnemyHP)
        {
            WhoWin = 1;
        }
        else if (SumOwnHP > SumEnemyHP)
        {
            WhoWin = 0;
        }
        else if(SumOwnHP == SumEnemyHP)
        {
            ReduceReward(0);
            ReduceReward(1);

            foreach (var i in OwnBatMen)
            {
                i.CurHP = -1;
            }

            foreach (var i in EnemyBatMen)
            {
                i.CurHP = -1;
            }
           
           // WhoWin = Random.Range(0, 2);

        }


        //如果敌方赢
        if (WhoWin == 1)
        {
            for (int i = 0; i < OwnBatMen.Length; i++)
            {
                if (OwnBatMen[i].CurHP > 0)
                {
                    switch (OwnBatMen[i].GetComponent<BatMan>().Addwhich)
                    {
                        case 0:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().HP -= 1;
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().Present_HP -= 1;
                            break;
                        case 1:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().ATK -= 1;
                            break;
                        case 2:
                            GameObject.FindGameObjectWithTag("EnemyHero").GetComponent<Hero>().DEF -= 1;
                            break;
                    }
                }
                OwnBatMen[i].CurHP = -1;
            }
            //foreach(var i in EnemyBatMen)
            //{
            //    if (i.CurHP > 0)
            //    {
            //        i.BatmanAtkCore();
            //    }
            //}
        }
        //我方赢
        else
        {

            for (int i = 0; i < EnemyBatMen.Length; i++)
            {
                if (EnemyBatMen[i].CurHP > 0)
                {
                 
                    switch (EnemyBatMen[i].GetComponent<BatMan>().Addwhich)
                    {
                        case 0:
                            GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().HP -= 1;
                            GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().Present_HP -= 1;
                            break;
                        case 1:
                            GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ATK -= 1;
                            break;
                        case 2:
                            GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().DEF -= 1;
                            break;


                    }
                    
                }

                EnemyBatMen[i].CurHP=-1;
            }

            //foreach (var i in OwnBatMen)
            //{
            //    if (i.CurHP > 0)
            //    {
            //        i.BatmanAtkCore();
            //    }
            //}


        }

        return WhoWin;
    }

    public void SendMessageToCore()
    {
        if (WhoWin == 0)
        {
            EnemyCore.GetComponent<Core>().OnEnemyCoreEndRound();
        }
        else
        {
            OwnCore.GetComponent<Core>().OnOwnCoreEndRound();
        }

    }

    //开始一个新回合的控制
    public void NewRoundStart()
    {
        //回合数+1
        //Whowin修改
        WhoWin = -1;
        //重置攻击列表
        CardManager.Instance.HowManyCardCanUse = 5;

        //重新出兵，随机属性，随着回合数增强,重置数值
        BatMan[] OwnBatMen = OwnBtGroup.GetComponentsInChildren<BatMan>();


        for (int i = 0; i < OwnBatMen.Length; i++)
        {
            OwnBatMen[i].GetComponent<BatMan>().GrowBatman();
        }

        BatMan[] EnemyBatMen = EnemyBtGroup.GetComponentsInChildren<BatMan>();
        for (int i = 0; i < EnemyBatMen.Length; i++)
        {
            EnemyBatMen[i].GetComponent<BatMan>().GrowBatman();
        }

        this.GetComponent<OnEndGroupAnimation>().StartNewRound();



        //重新抽牌

        CardManager.Instance.CreatCardPrefeb();
        //更新卡组数
        GameObject.Find("UIManager").GetComponent<Test_UI_Manger>().UIUpdate();
        GameObject.Find("UIManager").GetComponent<Test_UI_Manger>().Game_BtnRoundOver.interactable = true;
        
        foreach(var i in BatmanSumAtkHp)
        {
            i.GetComponent<BatmanSumAtkHp>().ResetSumInfor(); 
        }

    }
}
