using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : Singleton<UIManager>
{
    //用于管理主体公用UI管理
    Text messageText;
   
   
    /*public void init()
    {


            MessageBox = GameObject.Instantiate(MPre, Vector3.zero, Quaternion.identity); 
            messageText = MessageBox.GetComponent<Text>();
           // MessageBox.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            MessageBox.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0, 280, 0), 0.5f);
           
    }*/
    void Start()
    {  
    
    }
    public void ShowMessage(string str)
    {
        messageText.text = str;
    
    }
}
