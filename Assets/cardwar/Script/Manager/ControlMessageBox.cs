using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ControlMessageBox : Singleton<ControlMessageBox>
{
    private GameObject MessageGameObject;
    private DOTweenAnimation MessageBoxAnimation;

    /// <summary>
    /// 每次转场的时候实例化
    /// </summary>
    public void InitMessageBox()
    {
        MessageGameObject = GameObject.Find("MessageBox");
        MessageBoxAnimation = MessageGameObject.GetComponent<DOTweenAnimation>();
    }

    public void SetMessage(string str)
    {
        MessageBoxAnimation.DOPlayForward();
        MessageGameObject.GetComponent<Text>().text = str;
        Invoke("DelayMessageBoxReturn", 0.6f);
    }
    private void DelayMessageBoxReturn()
    {
        MessageBoxAnimation.DOPlayBackwards();
    }
}
