using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoseCardGroupToSee : MonoBehaviour {
    [SerializeField]
    private GameObject CardToSeePrefab;
    private int CardGroupLength = 0;
    public GameObject Lose_CardPanel;


    /// <summary>
    /// 生成预制卡片，设置信息
    /// </summary>
    public void CreatLoseCardToSee()
    {
        DestroyLoseCardToSee();
        //获得弃牌List长度
        CardGroupLength = CardManager.Instance.CardToLoseList.Count;
        if(CardGroupLength>0)
        {
            for (int i = 0; i < CardGroupLength; i++)
            {
                //实例化生成一个物体
                //GameObject go = GameObject.Instantiate(CardToSeePrefab, Vector3.zero, Quaternion.identity);
                //go.GetComponent<CardToSeeInstance>().card = CardManager.Instance.CardGroup[CardManager.Instance.CardToLoseList[i]];
                //go.GetComponent<CardToSeeInstance>().SetAllInfomation();
                //go.transform.SetParent(GameObject.Find("Lose_CardPanel").GetComponent<Transform>());

                GameObject go = GameObject.Instantiate(CardToSeePrefab, Vector3.zero, Quaternion.identity);
                go.GetComponent<CardToSeeInstance>().card = CardManager.Instance.CardToLoseList[i];
                go.transform.GetChild(1).transform.position = new Vector3(0, -45, 0);

                go.GetComponent<CardToSeeInstance>().SetAllInfomation();
                go.transform.SetParent(Lose_CardPanel.GetComponent<Transform>());
            }
        }
        
    }


    /// <summary>
    /// 清空已有的卡组信息
    /// </summary>
    public void DestroyLoseCardToSee()
    {
        GameObject go = Lose_CardPanel;
       
        for(int i = 0; i < go.transform.childCount; i++)
        {
            Destroy(go.transform.GetChild(i).gameObject);
        }
    }
    private void Start()
    {
        //CreatCardToSee();

    }
}
