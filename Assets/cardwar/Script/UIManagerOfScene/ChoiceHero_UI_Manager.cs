using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ChoiceHero_UI_Manager : MonoBehaviour {

    [SerializeField]
    private Button BtnReturn;
    [SerializeField]
    private Button BtnConfirm;

    [SerializeField]
    private DOTweenAnimation CameraShake;
    [SerializeField]
    private  AudioSource Butnclick;


    /// <summary>
    /// 第一个职业的Toggle，Text与Image；
    /// </summary>
    [SerializeField]
    private Toggle Hero1_Toggle;
    [SerializeField]
    private DOTweenAnimation Hero1_Text_DOT;
    [SerializeField]
    private GameObject Hero1_Image;

    [SerializeField]
    private Toggle Hero2_Toggle;
    [SerializeField]
    private DOTweenAnimation Hero2_Text_DOT;
    [SerializeField]
    private GameObject Hero2_Image;

    [SerializeField]
    private Toggle Hero3_Toggle;
    [SerializeField]
    private DOTweenAnimation Hero3_Text_DOT;
    [SerializeField]
    private GameObject Hero3_Image;
    [SerializeField]
    private GameObject Hero1_FadeImage;
    [SerializeField]
    private GameObject Hero2_FadeImage;
    [SerializeField]
    private GameObject Hero3_FadeImage;


    /// <summary>
    /// 注册监听器
    /// </summary>
    private void Start()
    {
        BtnReturn.onClick.AddListener(OnReturnClick);
        BtnConfirm.onClick.AddListener(OnConfirmClick);
        Hero1_Toggle.onValueChanged.AddListener((bool isOn) => { OnToggleClick_Hero1(isOn); });
        Hero2_Toggle.onValueChanged.AddListener((bool isOn) => { OnToggleClick_Hero2(isOn); });
        Hero3_Toggle.onValueChanged.AddListener((bool isOn) => { OnToggleClick_Hero3(isOn); });
    }

    
    private void OnToggleClick_Hero1(bool isOn)
    {
        Butnclick.Play();
        if(isOn)
        {
            Hero1_Image.SetActive(true);
            Hero1_Text_DOT.DOPlayForward();
            CameraShake.DOPlayForward();
            Hero1_FadeImage.SetActive(true);
            isOn = false;
        }
        else
        {
            Hero1_Image.SetActive(false);
            Hero1_Text_DOT.DOPlayBackwards();
            CameraShake.DOPlayBackwards();
            Hero1_FadeImage.SetActive(false);
            isOn = true;
        }
    }

    private void OnToggleClick_Hero2(bool isOn)
    {
        Butnclick.Play();
        if (isOn)
        {

            Hero2_Image.SetActive(true);
            Hero2_Text_DOT.DOPlayForward();
            CameraShake.DOPlayForward();
            Hero2_FadeImage.SetActive(true);
            isOn = false;
        }
        else
        {
            Hero2_Image.SetActive(false);
            Hero2_Text_DOT.DOPlayBackwards();
            CameraShake.DOPlayBackwards();
            Hero2_FadeImage.SetActive(false);
            isOn = true;
        }
    }

    private void OnToggleClick_Hero3(bool isOn)
    {
        Butnclick.Play();
        if (isOn)
        {
            Hero3_Image.SetActive(true);
            Hero3_Text_DOT.DOPlayForward();
            CameraShake.DOPlayForward();
            Hero3_FadeImage.SetActive(true);
            isOn = false;
        }
        else
        {
            Hero3_Image.SetActive(false);
            Hero3_Text_DOT.DOPlayBackwards();
            CameraShake.DOPlayBackwards();
            Hero3_FadeImage.SetActive(false);
            isOn = true;
        }
    }
    //确认按钮
    private void OnConfirmClick()
    {
      //  Butnclick.Play();
        if(!Hero1_Toggle.isOn&&!Hero2_Toggle.isOn&&!Hero3_Toggle.isOn)
        {
            ControlMessageBox.Instance.SetMessage("请选择职业");
        }
        else
        {
            if(Hero1_Toggle.isOn)
            {
             //   Debug.Log("战士");
                PlayManager.Instance.IsInHeroCareer = PlayManager.HeroCareer.Warrior;
                SceneManager.Instance.ChangeScene(GameManager.Scene.Rest, "Rest");
            }
            if(Hero2_Toggle.isOn)
            {
                Debug.Log("法师");
                PlayManager.Instance.IsInHeroCareer = PlayManager.HeroCareer.Master;
                ControlMessageBox.Instance.SetMessage("制作中。。。敬请期待");
            }
            if (Hero3_Toggle.isOn)
            {
                Debug.Log("猎人");
                PlayManager.Instance.IsInHeroCareer = PlayManager.HeroCareer.Hunter;
                ControlMessageBox.Instance.SetMessage("制作中。。。敬请期待");
            }
            //根据不同职业生成不同的初始卡组
           // SceneManager.Instance.ChangeScene(GameManager.Scene.Rest, "Rest");
            CardManager.Instance.SkillandEnventCardInformation();
            CardManager.Instance.CreatOriginalCardGroup(PlayManager.Instance.IsInHeroCareer);
            
        }

    }
    //返回按钮
    private void OnReturnClick()
    {
      //  Butnclick.Play();
        SceneManager.Instance.ChangeScene(GameManager.Scene.ChioceMode, "ChoiceMode");
    }

    private void OnDestroy()
    {
        BtnReturn.onClick.RemoveListener(OnReturnClick);
        BtnConfirm.onClick.RemoveListener(OnConfirmClick);
    }
}
