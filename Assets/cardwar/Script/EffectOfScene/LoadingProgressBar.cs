using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour {
    [SerializeField]
    private Slider Scorll;//进度条
    [SerializeField]
    private Text Text;//比率
    [SerializeField]
    private Text Load;//Loading字体
    [SerializeField]
    private GameObject loading;
    private float time = 0;//两秒后加载完成
    //private bool IsFirstTime = false;
    private string text = "Loading";

    private void Start()
    {
        StartCoroutine(LoadTextChange());
        //IsFirstTime = true; 
    }

    private void Update()
    {
        if(Scorll.value<100)
        {
            Scorll.value += 0.65f;
            Text.text = Scorll.value.ToString("f0") + "%";
        }
        if(Scorll.value==100)
        {
            loading.SetActive(false);
        }
    }

    private  IEnumerator  LoadTextChange()
    {
        while(Scorll.value<100)
        {
            text = text + ".";
            if(text.Length>=11)
            {
                text = "Loading";
            }
            Load.text = text;

           
            yield return new WaitForSeconds(0.8f);
        }

    }

  
}
