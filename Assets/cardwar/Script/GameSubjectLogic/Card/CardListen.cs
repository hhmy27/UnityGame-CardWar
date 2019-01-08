using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardListen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{

    // 延迟时间
    private float delay = 0.2f;
    // 按钮是否是按下状态
    private bool isDown = false;
    // 按钮最后一次是被按住状态时候的时间
    private float lastIsDownTime;

    //检测鼠标是否在按钮中
    private bool IsInButton = false;


    void Start()
    {
        
    }


    void Update()
    {
        // 如果按钮是被按下状态
        /* if (isDown)
         {
            /* Transform position = Player.GetComponent<Transform>();
             position.Translate(Vector2.right * movespeed * Time.deltaTime);*/
        // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒
        /*  if (Time.time - lastIsDownTime > delay)
          {
              // 触发长按方法
              // 记录按钮最后一次被按下的时间
              lastIsDownTime = Time.time;
          }
         }*/


    }

    // 当按钮被按下后系统自动调用此方法
    //选中卡牌后
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
        this.GetComponent<CardInstance>().SetCurChooseCardToEvent();
        GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().aCardhadbeenChoose = true;
        GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().ChooseCard = this.gameObject;



    }
    //public void OnMouseDrag()
    //{
    //    Debug.Log("OnMouseDrag");
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().aCardhadbeenChoose = false;

    //    }
    //}
    // 当按钮抬起的时候自动调用此方法
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        this.GetComponent<Transform>().SetSiblingIndex(0);

        this.GetComponent<Transform>().DOScale(new Vector3(1, 1, 1), 0.3f);

    }

    // 当鼠标从按钮上离开的时候自动调用此方法
    public void OnPointerExit(PointerEventData eventData)
    {
        //this.GetComponent<Transform>().SetSiblingIndex(0);

        IsInButton = false;
        if (!isDown)
        {
            this.GetComponent<Transform>().DOScale(new Vector3(1, 1, 1), 0.3f);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Transform>().SetSiblingIndex(18);

        IsInButton = true;
        this.GetComponent<Transform>().DOScale(new Vector3(1.5f, 1.5f, 1), 0.3f);


    }

}
