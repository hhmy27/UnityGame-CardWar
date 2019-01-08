using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBloodSlider : MonoBehaviour
{
    public GameObject Hero;
    public Text ATKText;
    public Text DEFText;
    public Text BloodText;
    public Text MpText;
    private int Hp;
    private int Present_HP;
    public GameObject MPslier;


    private void Update()
    {
        this.GetComponent<Slider>().maxValue = Hero.GetComponent<Hero>().HP;
        this.GetComponent<Slider>().value = Hero.GetComponent<Hero>().Present_HP;
        Hp = Hero.GetComponent<Hero>().HP;
        Present_HP = Hero.GetComponent<Hero>().Present_HP;
        ATKText.text = "" + Hero.GetComponent<Hero>().ATK.ToString();
        DEFText.text = "" + Hero.GetComponent<Hero>().DEF.ToString();
        BloodText.text = Present_HP.ToString() + "/" + Hp.ToString();
        MPslier.GetComponent<Slider>().value = Hero.GetComponent<Hero>().Present_MP;
        MpText.text = Hero.GetComponent<Hero>().Present_MP.ToString();
    }



}
