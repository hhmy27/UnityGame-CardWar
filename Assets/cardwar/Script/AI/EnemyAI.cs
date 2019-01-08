using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class EnemyAI : Conditional {

    public SharedBool ISturnAI =false;

    public override TaskStatus OnUpdate()
    {
        if (ISturnAI.Value == false)
        {



            return TaskStatus.Failure;
        }
        else
        {




            return TaskStatus.Success;


        }

    }


}
