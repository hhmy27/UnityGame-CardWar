using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReallyOver : MonoBehaviour {
    public AudioSource paly;
    public void Playit()
    {

        paly.Play();
    }
    public void  Gamesover()
    {

        Application.Quit();
    
    }
}
