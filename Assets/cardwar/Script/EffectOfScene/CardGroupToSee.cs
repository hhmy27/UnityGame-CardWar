using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 设置卡牌信息
/// </summary>
public class CardGroupToSee : MonoBehaviour
{
    [SerializeField]
    private GameObject CardToSeePrefab;
    private int CardGroupLength = 0;
    //public GameObject TextTitle;
    public GameObject CardPanel;//是DragCardPanelBackground下的CardPanel
    /// <summary>
    /// 生成预制卡片，设置信息
    /// </summary>
    public void CreatCardToSee()
    {
        DestroyCardToSee();
        //获得待抽牌长度
        CardGroupLength = CardManager.Instance.CardToDrugList.Count;
        for (int i = 0; i < CardGroupLength; i++)
        {
            //实例化生成一个物体
            GameObject go = GameObject.Instantiate(CardToSeePrefab, Vector3.zero, Quaternion.identity);
            go.GetComponent<CardToSeeInstance>().card = CardManager.Instance.CardGroup[CardManager.Instance.CardToDrugList[i]];
            go.transform.GetChild(1).transform.position = new Vector3(0, -45, 0);
            //TextTitle.transform.position = new Vector3(0, -45, 0);
            go.GetComponent<CardToSeeInstance>().SetAllInfomation();
            //设置到CardGroupPanel上
            go.transform.SetParent(CardPanel.GetComponent<Transform>());
            // CardManager.Instance.CardGroup[CardManager.Instance.CardToDrugList[i]];
        }
    }


    /// <summary>
    /// 清空已有的卡组信息
    /// </summary>
    public void DestroyCardToSee()
    {
        GameObject go = CardPanel;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Destroy(go.transform.GetChild(i).gameObject);
        }
    }
    private void Start()
    {
        DestroyCardToSee();
        CreatCardToSee();
    }



}
