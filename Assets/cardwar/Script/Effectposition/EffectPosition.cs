using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPosition : MonoBehaviour {

    public Transform Effection1;
    public Transform Effection2;
    public Transform Effection3;
    public RectTransform po1;
    public RectTransform po2;
    public RectTransform po3;



    void Start() 
    {
        /*Effection1.position = Camera.main.ScreenToWorldPoint(new Vector3(po1.position.x+160f,po1.position.y-20f,po1.position.z));
        Effection2.position = Camera.main.ScreenToWorldPoint(new Vector3(po2.position.x+155f, po2.position.y - 25f, po2.position.z));
        Effection3.position = Camera.main.ScreenToWorldPoint(new Vector3(po2.position.x+900f , po2.position.y +50f, po2.position.z));
        */



        Effection1.position = Camera.main.ScreenToWorldPoint(po1.position);
        Effection2.position = Camera.main.ScreenToWorldPoint(new Vector3(po2.position.x , po2.position.y -5f, po1.position.z));
        Effection3.position = Camera.main.ScreenToWorldPoint(po3.position);
    }
    



}
