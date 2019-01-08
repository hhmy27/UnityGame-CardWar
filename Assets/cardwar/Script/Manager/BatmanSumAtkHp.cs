using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatmanSumAtkHp : MonoBehaviour {

    public GameObject[] OurBatman;
    public Text SumAtkText;
    public Text SumHpText;
    private int SumAtk;
    private int SumHp;
    private void Start()
    {

        SumAtk = 0;
        SumHp = 0;
        Invoke("ResetSumInfor", 0.5f);

    }

    //重置属性提示
    public void ResetSumInfor()
    {
        SumHp = 0;
        SumAtk = 0;
        for (int i = 0; i < OurBatman.Length; i++)
        {
            if(OurBatman[i].GetComponent<BatMan>().CurHP>0)
            {
                SumAtk += OurBatman[i].GetComponent<BatMan>().Atk;
                SumHp += OurBatman[i].GetComponent<BatMan>().CurHP;
            }
        }

       // Debug.Log("SumATk:" + SumAtk);
        //Debug.Log("SumHp:" + SumHp);
        SumAtkText.text = SumAtk.ToString();
        SumHpText.text = SumHp.ToString();

    }





}
