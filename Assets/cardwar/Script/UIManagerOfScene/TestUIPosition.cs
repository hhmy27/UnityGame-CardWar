using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIPosition : MonoBehaviour {
    public Transform OurBatmanGroup;
    public Transform EnemyBatmanGroup;
    public Transform[] OurBatman;
    public Transform[] EnemyBatman;
    public RectTransform[] OurBatmanInformation;
    public RectTransform[] EnemyBatmanInformation;
    public RectTransform[] OurBatmanDemageText;
    public RectTransform[] EnemyBatmanDemageText;
    public RectTransform[] OurBatmanBloodSlider;
    public RectTransform[] EnemyBatmanBloodSlider;


    public Transform OurCore;
    public Transform EnemyCore;
    public RectTransform OurCoreInformation;
    public RectTransform EnemyCoreInformation;
    public RectTransform EnemyHeroDemageText;
    public RectTransform OurHeroDemageText;
    public RectTransform OurCoreDemageText;
    public RectTransform EnemyCoreDemageText;
    public RectTransform EnemyBatmanSumAtkHP;
    public RectTransform OurBatmanSumAtkHP;


    public Transform OurHero;
    public Transform EnemyHero;
    public RectTransform OurHeroInformation;
    public RectTransform EnemyHeroInformation;
    public RectTransform OurCoreUIposition;
    public RectTransform EnemyCoreUIposion;
    public RectTransform OurHeroUIposition;
    public RectTransform EnemyHeroUIposion;
    public RectTransform OurBatmanGroupPosition;
    public RectTransform EnemyBatmanGroupPositon;
    public RectTransform OurSumATKPosition;
    public RectTransform EnemySumATKPosion;

    private void Update()
    {
        for(int i = 0; i < 4; i++)
        {
            OurBatmanInformation[i].position= Camera.main.WorldToScreenPoint(new Vector3(OurBatman[i].position.x, OurBatman[i].position.y-0.05f, OurBatman[i].position.z));
            EnemyBatmanInformation[i].position = Camera.main.WorldToScreenPoint(new Vector3(EnemyBatman[i].position.x, EnemyBatman[i].position.y - 0.05f, EnemyBatman[i].position.z));
        }
        for (int i = 0; i < 4; i++)
        {
            OurBatmanBloodSlider[i].position = new Vector3(OurBatmanInformation[i].position.x, OurBatmanInformation[i].position.y +70f, OurBatmanInformation[i].position.z);
            EnemyBatmanBloodSlider[i].position = new Vector3(EnemyBatmanInformation[i].position.x, EnemyBatmanInformation[i].position.y +70f, EnemyBatmanInformation[i].position.z);
        }



    }
    private void Start()
    {

      OurBatmanGroup.position = Camera.main.ScreenToWorldPoint(new Vector3(OurBatmanGroupPosition.position.x, OurBatmanGroupPosition.position.y, 5));
      EnemyBatmanGroup.position = Camera.main.ScreenToWorldPoint(new Vector3(EnemyBatmanGroupPositon.position.x, EnemyBatmanGroupPositon.position.y, 5));

        OurCore.position = Camera.main.ScreenToWorldPoint(new Vector3( OurCoreUIposition.position.x,OurCoreUIposition.position.y,5));
        EnemyCore.position = Camera.main.ScreenToWorldPoint(new Vector3(EnemyCoreUIposion.position.x,EnemyCoreUIposion.position.y,5));

        OurHero.position = Camera.main.ScreenToWorldPoint(new Vector3(OurHeroUIposition.position.x, OurHeroUIposition.position.y, 5));
        EnemyHero.position = Camera.main.ScreenToWorldPoint(new Vector3(EnemyHeroUIposion.position.x, EnemyHeroUIposion.position.y, 5));

        OurCoreInformation.position = Camera.main.WorldToScreenPoint(new Vector3(OurCore.position.x + 0.3f, OurCore.position.y + 3f, OurCore.position.z));
        EnemyCoreInformation.position = Camera.main.WorldToScreenPoint(new Vector3(EnemyCore.position.x + 0.2f, EnemyCore.position.y + 3f, EnemyCore.position.z));

        OurHeroInformation.position = Camera.main.WorldToScreenPoint(new Vector3(OurHero.position.x, OurHero.position.y +3f, OurHero.position.z));
        EnemyHeroInformation.position = Camera.main.WorldToScreenPoint(new Vector3(EnemyHero.position.x , EnemyHero.position.y + 3f, EnemyHero.position.z));

        EnemyHeroDemageText.position = new Vector3(EnemyHeroInformation.position.x, EnemyHeroInformation.position.y + 50f, 0);
        OurHeroDemageText.position = new Vector3(OurHeroDemageText.position.x, OurHeroDemageText.position.y + 50f, 0);
        OurCoreDemageText.position = new Vector3(OurHeroInformation.position.x, OurHeroInformation.position.y + 50,0);
        EnemyCoreDemageText.position = new Vector3(EnemyCoreInformation.position.x, EnemyCoreInformation.position.y + 50, 0);

        for (int i = 0; i < 4; i++)
        {
            OurBatmanDemageText[i].position = new Vector3(OurBatmanInformation[i].position.x-50, OurBatmanInformation[i].position.y + 50f, OurBatmanInformation[i].position.z);
            EnemyBatmanDemageText[i].position = new Vector3(EnemyBatmanInformation[i].position.x-50, EnemyBatmanInformation[i].position.y + 50f, EnemyBatmanInformation[i].position.z);
        }


        OurBatmanSumAtkHP.position = new Vector3(OurSumATKPosition.position.x, OurSumATKPosition.position.y+10f , 0);
        EnemyBatmanSumAtkHP.position = new Vector3(EnemySumATKPosion.position.x, EnemySumATKPosion.position.y+10f , 0);


    }
}

