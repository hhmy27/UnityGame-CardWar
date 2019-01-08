
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class AIPlayCard : Conditional
{

    public SharedInt Count = 1;

    public override TaskStatus OnUpdate()
    {
        if (Count.Value != 6)
        {



            return TaskStatus.Failure;
        }
        else
        {




            return TaskStatus.Success;


        }

    }
}
