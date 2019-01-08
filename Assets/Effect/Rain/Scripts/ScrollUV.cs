using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour
{

    public float scrollSpeed_X = 0.5f;
    public float scrollSpeed_Y = 0.5f;


    public void Main()
    {
    }

    public void Update()
    {
        float x = Time.time * this.scrollSpeed_X;
        float y = Time.time * this.scrollSpeed_Y;
        this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, y);
    }
}

 


