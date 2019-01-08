using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//本类用于抽象数据作为存储辅助
[System.Serializable]
public class Save
{
    //这里需要被外界访问所以用public
    public int limit_HP;//最大生命值
    public int limit_MP;//最大MP
    public int persent_HP;//当前生命值
    public int persent_MP;//当前MP
    public int Attack;//攻击力
    public int Defend;//防御力
}
