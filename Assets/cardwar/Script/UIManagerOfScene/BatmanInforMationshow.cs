using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class BatmanInforMationshow :  MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public int CurHP;

    public int Atk;
  
    private int AddHp = 0;
  
    private int AddAtk = 0;
  
    private int AddDef = 0;



    public CanvasGroup canvasGroup;
    float value = 0;
    //  canvasGroup.alpha = value;

    //如果击杀小兵还有别的收益类型在这里补充
    private int Addwhich;
    [SerializeField]
    private GameObject Batman;
    [SerializeField]
    private Text textshow;
    string Information = "";


    public void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();



    }


    private void Update()
    {

     canvasGroup.alpha = value;
    }
   /* public void OnPointerDown(PointerEventData eventData)
    {
       
       
        


    }*/

 /*   // 当按钮抬起的时候自动调用此方法
    public void OnPointerUp(PointerEventData eventData)
    {
       

    }*/

    // 当鼠标从按钮上离开的时候自动调用此方法
    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.To(() => value, x => value = x, 0, 0.5f);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       /* this.CurHP = Batman.GetComponent<BatMan>().CurHP;
        this.Atk = Batman.GetComponent<BatMan>().Atk;
        this.Addwhich = Batman.GetComponent<BatMan>().Addwhich;
        this.AddAtk = Batman.GetComponent<BatMan>().AddAtk;
        this.AddDef = Batman.GetComponent<BatMan>().AddDef;
        this.AddHp = Batman.GetComponent<BatMan>().AddHp;

        switch (Addwhich)
        {
            case 0:
                Information = "HP:" + CurHP + "\n" + "ATK:" + Atk + "\n" + "奖励:\n" + "MaxHP" + AddHp;
                break;
            case 1:
                Information = "HP:" + CurHP + "\n" + "ATK:" + Atk + "\n" + "奖励:\n" + "Atk" + AddAtk;
                break;
            case 2:
                Information = "HP:" + CurHP + "\n" + "ATK:" + Atk + "\n" + "奖励:\n" + "Def" + AddDef;
                break;


        }*/
        this.CurHP = Batman.GetComponent<BatMan>().CurHP;
        textshow.text = Information;
        if (CurHP > 0)
        {
            DOTween.To(() => value, x => value = x, 1, 0.5f);
        }

    }

}
