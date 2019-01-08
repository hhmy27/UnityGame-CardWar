using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardManager : Singleton<CardManager>
{
    [SerializeField]
    private GameObject CardPrefeb;//持有卡牌Prefab


    //判断卡组中已有卡牌的信息

    // 总共有多少张卡牌
    public int HowManyCard = 14;
    //总共抽多少张卡牌
    public int HowManytoDrug = 7;
    //还剩多少张卡牌
    public int HowManyCardRemain;
    //三种类型的牌
    private int HowManyAtkCard;
    //
    private int HowManySkillCard;
    //
    private int HowManyEnventCard;

    public Card[] preSkillType;

    public Card[] preEnventType;
    //所有卡牌存放的地方
    public Card[] CardGroup;
    //还能用多少张
    public int HowManyCardCanUse = 5;
    //总共能用多少张
    public int SumCardCanUse = 5;
    //用来抽牌的列表
    public List<int> CardToDrugList;

    public List<Card> CardToLoseList;
    public AudioSource DrawCardAuido;
    
    public GameObject CardToSeePrefab;
    //用来存储卡牌序号
    List<int> listNewCard ;
    //TODO  
    //添加新卡牌的方法
    public bool isInitNewCard=false;
    public bool isMouseAleardyDown=false;
    void Start()
    {
        listNewCard = new List<int>(20);
        CardToLoseList = new List<Card>(100);

        ReStartCardList();
        //InitlistNewCard();
        isInitNewCard = false;
        isMouseAleardyDown = false;
    }

public void SkillandEnventCardInformation()
    {

        preEnventType = new Card[1000];
        preSkillType = new Card[1000];
        
        preSkillType[0] = new Card(Card.CardType.SkillCard, "挥斩", "消耗<color=yellow>15</color>点怒气,对全部敌人（不包括城堡）造成<color=red>4(+ATK)</color>点伤害", 4, 1);

        preSkillType[1] = new Card(Card.CardType.SkillCard, "搏命", "消耗<color=yellow>9</color>点怒气,对敌方英雄造成<color=red>3(+ATK)</color>点伤害,我方英雄每损失5点生命则造成伤害+<color=red>2</color>", 3, 2);

        preSkillType[2] = new Card(Card.CardType.SkillCard, "双杀", "消耗<color=yellow>10</color>点怒气，对敌方英雄造成<color=red>5(+ATK)</color>的伤害,对任意一个小兵造成<color=red>2(+ATK)</color>点伤害", 5, 3);

        //BOSS
        preSkillType[3] = new Card(Card.CardType.SkillCard, "拔刀", "消耗<color=yellow>8</color>点怒气,随机攻击3个敌人，对每个敌人造成<color=red>5(+ATK)</color>点伤害", 5, 4);

        preSkillType[4] = new Card(Card.CardType.SkillCard, "穿刺", "消耗<color=yellow>10</color>点怒气,造成英雄最大生命值<color=red>10％</color>的伤害(*对城堡无效)", 0, 5);

        preSkillType[5] = new Card(Card.CardType.SkillCard, "斩杀", "消耗<color=yellow>10</color>点怒气,造成<color=red>3点(+ATK)</color>伤害。若敌人生命值<15％，直接杀死对方(*对城堡无效)", 3, 6);

        preSkillType[6] = new Card(Card.CardType.SkillCard, "攻城", "消耗<color=yellow>8</color>点怒气,对敌方城堡造成<color=red>15(+ATK)</color>点伤害", 3, 7);

        preSkillType[7] = new Card(Card.CardType.SkillCard, "包扎", "消耗<color=yellow>6</color>点怒气,恢复<color=red>20</color>点生命值", 0, 9);

      
        //preSkillType[8] = new Card(Card.CardType.SkillCard, "破血", "消耗6点怒气,每回合造成3伤害，持续2回合", 3, 10);
//      preEnventType[0] = new Card(Card.CardType.EventCard, "陷阱", "在敌方行军路上放置陷阱，若敌人进入，则持续掉血", 1, 11);
//      preEnventType[1] = new Card(Card.CardType.EventCard, "迷途", "改变天气，敌方暂停一回合", 0, 12);
//      preEnventType[2] = new Card(Card.CardType.EventCard, "急速", "恢复3点行动力", 0, 13);


        preEnventType[0] = new Card(Card.CardType.EventCard, "神眷", "恢复<color=red>50％</color>已损失生命值", 0, 14);
        preEnventType[1] = new Card(Card.CardType.EventCard, "重整", "获得你当前<color=red>防御力</color>值的恢复", 0, 15);
        preEnventType[2] = new Card(Card.CardType.EventCard, "嗜血", "随机杀死一名小兵，获得其生命值的恢复", 0, 16);
        preEnventType[3] = new Card(Card.CardType.EventCard, "出击", "对敌方英雄造成我方小兵攻击力<color=red>总和</color>的伤害", 0, 17);

//      preEnventType[4] = new Card(Card.CardType.EventCard, "静养", "下一回合恢复最大生命值，并无法行动", 0, 15);


    }

    public void CreatOriginalCardGroup(PlayManager.HeroCareer heroCareer)
    {
        //TODO
        //根据传入的不同职业生成不同的初始卡组

        CardGroup = new Card[200];
        if (heroCareer == PlayManager.HeroCareer.Warrior)
        {
            for (int i = 0; i < 8; i++)
            {
                CardGroup[i] = new Card(Card.CardType.AtkCard, "打击", "对目标造成<color=red>3(+ATK)</color>点伤害,获得<color=yellow>3</color>点怒气", 3, 0);
            }
            for (int i = 0; i < 5; i++)
            {
                CardGroup[i + 8] = preSkillType[i];
            }
            CardGroup[13] = preEnventType[2];
        }
        else if (heroCareer == PlayManager.HeroCareer.Master)
        {



        }
        else if (heroCareer == PlayManager.HeroCareer.Warrior)
        {



        }


    }
    public void InitlistNewCard()
    {
        for (int j = 5; j < 8; j++)
        {
            listNewCard.Add(j);
        }
    }


  
    /// <summary>
    /// 
    /// </summary>
    public void AddaNewCardToGroup()
    {
        InitlistNewCard();
        int i= Random.Range(0,listNewCard.Count);

        ReStartCardList();

        CardToDrugList.Add(HowManyCard++);
        Debug.Log("preSkillType[listNewCard[i]]:"+listNewCard[i]);
        CardGroup[HowManyCard-1] = preSkillType[listNewCard[i]];
        listNewCard.Remove(listNewCard[i]);
    }

    public void CreatCardPrefeb()
    {
        //抽牌
        if (HowManyCardRemain >= HowManytoDrug)
        {

            DrugCard();
        }
        else
        {
            //洗牌
            ReStartCardList();
           // HowManyCardRemain = HowManyCard;
            ReStartLoseCardList();
            DrugCard();
        }
    }

    /// <summary>
    /// 满足条件时抽上限牌数
    /// </summary>
    private void DrugCard()
    {
        for (int i = 0; i < HowManytoDrug; i++)
        {
            //随机数j
            int j = Random.Range(0, CardToDrugList.Count);
            //List中的第j+1个元素对应的值 (j+1)
            CreatCard(i, CardToDrugList[j]);
            //剩余卡牌数-1
            HowManyCardRemain--;
            //从抽牌List中移除掉这张牌
            CardToDrugList.Remove(CardToDrugList[j]);



        }
        DrawCardAuido.Play();
    }

    public void CreatCard(int i, int Rand)
    {
        //实例化Prefab,实例化时CardInstance调用Start()函数生成文字信息，SetImage生成图片信息
        GameObject Card = GameObject.Instantiate(CardPrefeb, Vector3.zero, Quaternion.identity);
        //SendMessage给预物体的脚本，完成更换贴图以及具化卡牌的功能  
        Card.GetComponent<CardInstance>().SetCard((CardGroup[Rand]));
        //设置卡牌底图
        Card.GetComponent<CardInstance>().SetImage(CardGroup[Rand].ImageIndex);
        //Card.GetComponent<CardInstance>().SetAtk(CardGroup[Rand].Demage);
        
        Card.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        Card.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(-340 + i * 180, -240, 0), 3f);
        Card.GetComponent<Transform>().SetSiblingIndex(0);
    }

    /// <summary>
    /// 洗牌后重置列表用于抽牌
    /// </summary>
    private void ReStartCardList()
    {
        HowManyCardRemain = HowManyCard;
        //清空已有的LIST
        CardToDrugList.Clear();

        for (int i = 0; i < HowManyCard; i++)
        {
            CardToDrugList.Add(i);
        }

        if (GameManager.Instance.WhichScene == GameManager.Scene.Test)
        {
            GameObject.Find("Event").GetComponent<MainSceneEvent>().HowManyCardHadUsed = 0;
        }
        if (GameManager.Instance.WhichScene == GameManager.Scene.Test1)
        {
            GameObject.Find("Event").GetComponent<MainSceneEvent>().HowManyCardHadUsed = 0;
        }
        if (GameManager.Instance.WhichScene == GameManager.Scene.Test2)
        {
            GameObject.Find("Event").GetComponent<MainSceneEvent>().HowManyCardHadUsed = 0;
        }
    }


    /// <summary>
    ///只有打出牌和弃牌的时候 添加到弃牌List中 
    /// </summary>
    public void AddCardToLoseCardList(Card loseCard)
    {
        CardToLoseList.Add(loseCard);
    }

    private void ReStartLoseCardList()
    {
        if(CardToLoseList.Count>0)
        {
            CardToLoseList.Clear();
        }
        
    }



}

