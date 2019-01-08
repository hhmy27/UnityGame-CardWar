using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 回合结束时，小兵Group的动画控制
/// </summary>

public class OnEndGroupAnimation : MonoBehaviour {
    //己方小兵
    [SerializeField]
    private GameObject OwnBatmanGroup;
    //敌方小兵
    [SerializeField]
    private GameObject EnemyBatmanGroup;

    //回合结束，传入谁赢的值。0是己方赢
    public void EndRound(int whowin)
    {

        if (whowin == 0)
        {
            OwnBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(4, 0, 5), 1f);//己方获胜方去敌方基地
            EnemyBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(14, 0, 5), 1f);//敌方进入下回合等待

        }
        else if (whowin == 1)
        {

            EnemyBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(-4, 0, 5), 1f);
            OwnBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(-10, 0, 5), 1f);
        }
        else if (whowin == -1)
        {

        }


    }


    public void StartNewRound()
    {
        OwnBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(-0.8f, 0, 5), 1f);
        EnemyBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(0, 0, 5), 1f);
    }

    //双方小兵都退出到场景外
    public void ExitScene()
    {
        EnemyBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(14, 0, 5), 0.5f);//敌方进入下回合等待
        OwnBatmanGroup.GetComponent<Transform>().DOMove(new Vector3(-10, 0, 5), 1f);
    }
}
