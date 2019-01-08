using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class AIAoeAtk : Action
{

    public GameObject player;
    public GameObject core;
    public GameObject[] Batmen;
    public GameObject TestUImanager;

    public override void OnStart()
    {
        TestUImanager = GameObject.Find("UIManager");
        base.OnStart();
    }
    public override TaskStatus OnUpdate()
    {

        if (GameManager.Instance.GameLevel == 1)
        {






        }
        else if (GameManager.Instance.GameLevel == 2)
        {

            this.GetComponent<Animator>().SetTrigger("Atk");
            Batmen = GameObject.FindGameObjectsWithTag("OurBatman");
            player = GameObject.FindGameObjectWithTag("Hero");
            int num = 0;
            foreach (var i in Batmen)
            {
                
                i.GetComponent<BatMan>().BatmanReduceHP(this.GetComponent<Hero>().ATK + 5);
                i.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
                if (++num > 2)
                {

                    break;

                }
            }
            //播放AOE攻击音效
            TestUImanager.GetComponents<AudioSource>()[1].Play();

            //  player.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);

            player.gameObject.GetComponent<Hero>().ReduceHP(4 + this.GetComponent<Hero>().ATK);

            //player.gameObject.GetComponent<Hero>().Present_HP -= 4 - player.gameObject.GetComponent<Hero>().DEF + this.GetComponent<Hero>().ATK;

            GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)false);
            var c = (SharedInt)GlobalVariables.Instance.GetVariable("playcard");
            c.Value = 1;
            GlobalVariables.Instance.SetVariable("playcard", (SharedInt)c.Value);


            return TaskStatus.Success;


        }
        else if (GameManager.Instance.GameLevel == 3)
        {
            this.GetComponent<Animator>().SetTrigger("AoeATK");
            Batmen = GameObject.FindGameObjectsWithTag("OurBatman");
            player = GameObject.FindGameObjectWithTag("Hero");
            foreach (var i in Batmen)
            {
                i.GetComponent<BatMan>().BatmanReduceHP(this.GetComponent<Hero>().ATK + 5);
                i.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
            }
            TestUImanager.GetComponents<AudioSource>()[1].Play();

            // player.gameObject.GetComponent<Transform>().DOShakePosition(1, new Vector3(2, 1, 0), 25);
            player.gameObject.GetComponent<Hero>().ReduceHP(4  + this.GetComponent<Hero>().ATK);

            //  player.gameObject.GetComponent<Hero>().Present_HP -= 4 - player.gameObject.GetComponent<Hero>().DEF + this.GetComponent<Hero>().ATK;

            GlobalVariables.Instance.SetVariable("IstrunToAI", (SharedBool)false);


            var c = (SharedInt)GlobalVariables.Instance.GetVariable("playcard");
            c.Value = 1;
            GlobalVariables.Instance.SetVariable("playcard", (SharedInt)c.Value);


            return TaskStatus.Success;




        }
        return TaskStatus.Success;
    }



}
