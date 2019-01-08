using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : Singleton<PlayManager> {
    [HideInInspector]
    public int limit_HP;//最大生命值
    public int limit_MP;//最大MP
    public int persent_HP;//当前生命值
    public int persent_MP;//当前MP
    public int Attack;//攻击力
    public int Defend;//防御力
    public int Point;
    public int HPPoint;
    //职业
    public enum HeroCareer { Warrior,Master,Hunter};
    public HeroCareer IsInHeroCareer;

    private void Start()
    {
        limit_HP = 100;
        limit_MP = 0;
        persent_HP = 100;
        persent_MP = 0;
        Attack = 1;
        Defend = 1;
        Point = 2;
        HPPoint = 10;
    }

    private void Update()
    {
        limit_HP = HPPoint * 10;
    }




}
