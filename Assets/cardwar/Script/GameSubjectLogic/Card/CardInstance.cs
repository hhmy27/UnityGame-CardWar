using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于实例化卡牌Prefab的脚本，对每个小兵来说这个脚本控制了所有的信息
/// </summary>
public class CardInstance : MonoBehaviour {

    public Image image;//获取图片组件，设置图片
    public Sprite[] img;//获取Prefab上绑定过的指定的图片
    public Sprite[] smallImg;
    public int ImageIndex;//图片索引
    public int Demage;//伤害
    public Card.CardType WhichCard;//卡牌类型
    public Card card;//确认这张牌
    bool IsGroupAtk;//

    public Text Title;

    public Text CardInfomation;

    public Image SmallImage;
    //public Text CardIntro;//卡牌介绍
    //设计事件牌属性
    //用一个ID来设计卡牌的功能（抽象事件牌技能）
    public int CardID;
    
 
    public void Start()
    {
        //设置卡牌文字信息
        Invoke("DelaySetInfor", 0.5f);
    }

    private void DelaySetInfor()
    {
        Title.text = card.CardTitle;
        CardInfomation.text = card.CardIntro;
    }

    /// <summary>
    /// 设置卡牌图片 
    /// </summary>
    /// <param name="index"></param>
    public void SetImage(int index)
    {

        ImageIndex = index;
        image.sprite =img[index];
        //Debug.Log(card.CardID);

        switch (card.CardID)
        {
            case 0:
                SmallImage.sprite = smallImg[0];
                break;
            case 1:
                SmallImage.sprite = smallImg[1];
                break;
            case 2:
                SmallImage.sprite = smallImg[2];
                break;
            case 3:
                SmallImage.sprite = smallImg[3];
                break;
            case 4:
                SmallImage.sprite = smallImg[4];
                break;
            case 5:
                SmallImage.sprite = smallImg[5];
                break;
            case 6:
                SmallImage.sprite = smallImg[6];
                break;
            case 7:
                SmallImage.sprite = smallImg[7];
                break;
            case 9:
                SmallImage.sprite = smallImg[8];
                break;
            case 14:
                SmallImage.sprite = smallImg[9];
                break;
            case 15:
                SmallImage.sprite = smallImg[10];
                break;
            case 16:
                SmallImage.sprite = smallImg[11];
                break;
            case 17:
                SmallImage.sprite = smallImg[12];
                break;
        }
    }




    public void SetCurChooseCardToEvent()
    {

        GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard = card;



    }
    public void SetCard(Card card)
    {



        this.card=card;


    }
    public void SetAtk(int num)
    {
        Demage = num;
      
    }
   

	public void SetIntro(string Intro)
    {
    // CardIntro.text=Intro;
    }

    //以后补充各种set
    //...

    //小兵承受伤害

    public void DoEffect()
    { 
        
    
    }
    public void DoDemage()
    { 
    
    
    
    }

}
