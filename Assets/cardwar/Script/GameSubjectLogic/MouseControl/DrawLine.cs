using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DrawLine : MonoBehaviour
{

    /// <summary>
    /// 直线渲染器
    /// </summary>
    [SerializeField]
    private LineRenderer lineRenderer;
     
    [SerializeField]
    private AudioSource audioSource;

    LayerMask batmanGroupMask = 1 << 8;

    /// <summary>
    /// 是否第一次鼠标按下
    /// </summary>
    private bool firstMouseDown = false;

    /// <summary>
    /// 是否鼠标一直按下
    /// </summary>
    private bool mouseDown = false;
    private bool firstMouseUp = false;
    private void Start()
    {
        isCancelCardShow = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;
            firstMouseUp = false;
            //播放声音
            //audioSource.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            firstMouseUp = true;
        }

        onDrawLine();

        firstMouseDown = false;

    }


    /// <summary>
    /// 保存的所有坐标
    /// </summary>
    private Vector3[] positions = new Vector3[30];

    /// <summary>
    /// 当前保存的坐标数量
    /// </summary>
    private int posCount = 0;

    /// <summary>
    /// 代表这一帧鼠标的位置 就 头的坐标
    /// </summary>
    private Vector3 head;

    /// <summary>
    /// 代表上一帧鼠标的位置
    /// </summary>
    private Vector3 last;

    public GameObject CancelCard;
    private bool isCancelCardShow;
    /// <summary>
    /// 画线
    /// </summary>
    private void onDrawLine()
    {
        if (firstMouseDown)
        {
            //先把计数器设为0
            posCount = 0;

            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            last = head;
           
        }

        if (mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //onRayCast(Input.mousePosition);
            //Debug.Log("mouseDown");
            if (Vector3.Distance(head, last) > 0.05f)
            {
                savePosition(head);
                posCount++;


            }
            last = head;
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().aCardhadbeenChoose == true && isCancelCardShow==false)
            {
                CancelCard.GetComponent<DOTweenAnimation>().DOPlayForward();
                isCancelCardShow = true;
            }
        }
        else
        {
            if (firstMouseUp)
            {
                //Debug.Log("firstMouseUp ");
                //发射一条射线  只在鼠标提起的瞬间发射
                //如果选中了一张卡牌
                if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().aCardhadbeenChoose == true)
                {

                    //射线检测
                    onRayCast(Input.mousePosition);
                    //取消选中，清空信息
                    GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().aCardhadbeenChoose = false;
                    if(isCancelCardShow == true)
                    {
                        CancelCard.GetComponent<DOTweenAnimation>().DOPlayBackwards();

                    }
                }

                firstMouseUp = false;
                isCancelCardShow = false;
            }
            positions = new Vector3[30];
        }

        changePositions(positions);
    }

    #region
    //private void testRay(Vector3 worldPos)
    //{
    //    //if (Input.GetTouch(0).phase == TouchPhase.Began)
    //    //{
    //    //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
    //    //    RaycastHit rh = new RaycastHit();
    //    //    if (Physics.Raycast(ray, out rh, Mathf.Infinity, batmanGroupMask.value))
    //    //    {
    //    //        Debug.Log("碰撞体的名字：" + rh.collider.name);
    //    //    }
    //    //}
    //    Debug.Log("testRay");
    //    //worldPos=Camera.main.WorldToScreenPoint(worldPos);
    //    Debug.Log(worldPos);
    //    Ray ray = Camera.main.ScreenPointToRay(worldPos);
    //    RaycastHit rh = new RaycastHit();
    //    if (Physics.Raycast(ray, out rh, batmanGroupMask.value))
    //    {
    //        Debug.Log("碰撞体的名字：" + rh.collider.name);
    //    }
    //}
    #endregion

    /// <summary>
    /// 发射射线
    /// </summary>
    /// <param name="pos">Position.</param>
    private void onRayCast(Vector3 worldPos)
    {
        //Debug.Log("worldPos:"+worldPos);
        Ray ray = Camera.main.ScreenPointToRay(worldPos);
        //检测到第一个物体  Raycastall则是所有
        RaycastHit[] hits = Physics.RaycastAll(ray);

        //Debug.Log("hits.Length:"+hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log((hits[i].collider.name));

            switch (hits[i].collider.gameObject.tag)
            {

                case "EnemyBatman":
                    if (hits[i].collider.gameObject.GetComponent<BatMan>().IsOwnBatman == 0)
                    {
                      //  ControlMessageBox.Instance.SetMessage("敌人出牌");
                        //以后要增加AOE的话就增加条件判断
                        if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 1
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 2
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 3
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 4
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 5
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 6
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 7
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 9
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 14
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 15
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 16
                            &&
                            GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID != 17
                            )
                        {
                            
                            hits[i].collider.gameObject.GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);
                        }
                    }
                    break;
                case "EnemyCore":
                  //  ControlMessageBox.Instance.SetMessage("敌人出牌");
                    if (hits[i].collider.gameObject.GetComponent<Core>().own == 0)
                    {
                        hits[i].collider.gameObject.GetComponent<Core>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);
                    }
                    break;
                case "EnemyHero":
                 //   ControlMessageBox.Instance.SetMessage("敌人出牌");
                    if (hits[i].collider.gameObject.GetComponent<Hero>().own == 0)
                    {
                        hits[i].collider.gameObject.GetComponent<Hero>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

                    }
                    break;
                
            }


            //TODO 
            //hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);
            //这里写打击到的牌的事件功能
            //2018年7月17日13:35:41

        }

        //拖到空处 判断是否是AOE
        //选中挥斩卡牌
        //TODO
        //目前AOE一旦被选中就会触发，要添加取消出牌的功能
        if(Input.mousePosition.y>220)
        {
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 1)
            {

                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 2)
            {

                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 3)
            {

                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }

            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 4)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 5)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 6)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 7)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 9)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 14)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 15)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 16)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
            if (GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard.CardID == 17)
            {
                GameObject.FindGameObjectWithTag("EnemyBatman").GetComponent<BatMan>().CardOnthis(GameObject.FindGameObjectWithTag("Event").GetComponent<MainSceneEvent>().NowChooseCard);

            }
        }

       
    }





    /// <summary>  
    /// 保存坐标点
    /// </summary>
    /// <param name="pos">Position.</param>
    private void savePosition(Vector3 pos)
    {
        pos.z = 0;

        if (posCount <= 29)
        {
            for (int i = posCount; i <= 29; i++)
            {
                positions[i] = pos;
            }
        }
        else
        {
            for (int i = 0; i < 29; i++)
                positions[i] = positions[i + 1];

            positions[29] = pos;
        }
    }


    /// <summary>
    /// 修改直线渲染器的坐标
    /// </summary>
    /// <param name="postions">Postions.</param>
    private void changePositions(Vector3[] postions)
    {
        lineRenderer.SetPositions(postions);
    }
}
