using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class StartImgChangeEffect : MonoBehaviour, IPointerDownHandler
{
    //计数器
    public CanvasGroup canvasGroup;
    float value = 0;
    private int index = 0;
    private Image image;
    public Sprite[] img;
    //public int number = 3;
    private float _time;
    private float _endTime;
    private int num;
    public Text Textshow;
    public string[] textshow;
    private int  i=0;
    public Image ban;
    public AudioSource clk;
    private void Start()
    // Use this for initialization

    {
        Textshow = this.GetComponent<Text>();
        textshow = new string[3];
        textshow[0] = "光明王国是大陆上最强的国家之一，这得力于王国的圣子。\n" +
            "(点击翻页...)";
        textshow[1] = "王国每一百年会举行一次圣子选拔，成为圣子的人将会在接下来的百年守护着王国，并在百年之后将自己的力量赋予给下一位圣子，王国凭着圣子的力量得以兴盛。\n" +
            "(点击翻页...)";
        textshow[2] = "物极必衰，光明王国受到了一股不明力量的入侵，至此，以前俯首臣称的国家也开始不断挑衅光明王国的权威……圣子大人，能否带领我们重回光明？\n" +
            "(点击翻页...) ";
        image = GameObject.Find("Image1").GetComponent<Image>();
        image.sprite = img[0];
        Invoke("ShowImg1", 2.5f);


        //开始时刻的时间
        /* _time = Time.time;*/

        /* Invoke("ShowImg", 3f);*/
    }
    void ShowImg()
    {
        if (i <  textshow.Length-1)
        {
            clk.PlayDelayed(2.5f);
            Textshow.text = "";
            DOTween.To(() => value, x => value = x, 0, 1f).SetLoops(2, LoopType.Yoyo);
            Textshow.DOText(textshow[++i], 4f).SetDelay(3f).OnComplete(() => ban.gameObject.SetActive(false));
        }
        else
        {

            SceneManager.Instance.ChangeScene(GameManager.Scene.Start, "Start");

        }
    }

    void ShowImg1()
    {
        clk.Play();
        DOTween.To(() => value, x => value = x, 1, 1.5f);
        Textshow.DOText(textshow[0], 4f).OnComplete(() => ban.gameObject.SetActive(false)) ;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
         ban.gameObject.SetActive(true);
        Textshow.text = "";
        ShowImg();
        

    }
    private void init()
    {
     
        /* Invoke("ChangetoStart", 3f * num + 2);*/
        
    }
    public void ChangetoStart()
    {
        SceneManager.Instance.ChangeScene(GameManager.Scene.Start, "Start");
    }




    // Update is called once per frame
    void Update()
    {


        canvasGroup.alpha = value;

        /*
        //Debug.Log(Time.time);
        //现在的时间
        _endTime = Time.time;
        //开始时刻 到现在的时间差值
        float timeOffset = _endTime - _time;

        if (timeOffset > 3)
        {
            if (index < img.Length)
            {
                image.sprite = img[index];
                index += 1;

            }
            else
            {
                index = 0;
                image.sprite = img[index];
            }
            //记下换好下一张图片的时间
            _time = Time.time;

        }

    }*/
    }
}