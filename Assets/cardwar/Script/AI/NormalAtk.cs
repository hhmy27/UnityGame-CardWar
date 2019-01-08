
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class NormalAtk : Action{
    public GameObject  player;
    public GameObject core;
    public GameObject[] Batmen;
    private bool isAllBatmandead=false;
    public GameObject AtkEffect;
    public GameObject TestUImanager;
    public override void OnStart()
    {
        base.OnStart();
        player = GameObject.FindGameObjectWithTag("Hero");
        TestUImanager = GameObject.Find("UIManager");
    }
    public override TaskStatus OnUpdate()
    {

        int i = 100;
        Batmen = GameObject.FindGameObjectsWithTag("OurBatman");
        while (!ReSetlist())
        {
            i--;
            if (i < 0)
            {
        isAllBatmandead = true;

                break;

            }

        }
        if (isAllBatmandead)
        {
            player.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
            player.gameObject.GetComponent<Hero>().Present_HP -= 5 - player.gameObject.GetComponent<Hero>().DEF + this.GetComponent<Hero>().ATK;
            GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)false);
        }
        /*
        foreach (var i in Batmen)
        {
            i.GetComponent<BatMan>().CurHP= i.GetComponent<BatMan>().CurHP - this.GetComponent<Hero>().ATK - 5;

        }*/

        return TaskStatus.Success;
    }


    public bool ReSetlist()
    {
        var c=(SharedInt)GlobalVariables.Instance.GetVariable("playcard");
        //小兵数量
        int i = Random.Range(0, 4);
        if (Batmen[i].GetComponent<BatMan>().CurHP > 0)
        {
            isAllBatmandead = false;
            if (GameManager.Instance.GameLevel != 1)
            {
                c.Value = c.Value + 1;
            }
            // GameObject effect = GameObject.Instantiate(AtkEffect, Batmen[i].gameObject.transform.position, Quaternion.identity);
            TestUImanager.GetComponent<AudioSource>().Play();
            
            Batmen[i].GetComponent<BatMan>().BatmanReduceHP(this.GetComponent<Hero>().ATK + 5);
            this.GetComponent<Hero>().ReduceMP(0);
           // Batmen[i].gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
            GlobalVariables.Instance.SetVariable("playcard", (SharedInt)c.Value);
            GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)false);
            return true;
        }
        return false;
    }

}
