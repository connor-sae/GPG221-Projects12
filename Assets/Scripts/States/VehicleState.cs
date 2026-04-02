using UnityEngine;
using Anthill.AI;
using UnityEngine.Animations;

public class VehicleState : AntAIState 
{
    StateBehaviour[] stateBehaviours;
    VehicleStator stator;
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        if(!aGameObject.TryGetComponent(out stator))
            Debug.LogError("No Vehicle Stator found on AntGameObject: " + aGameObject.name);
    }

    public override void Enter()
    {
        base.Enter();
        stator.SetStateBehaviours(stateBehaviours);
    }

    public override void Exit()
    {
        base.Exit();
        stator.SetStateBehaviours(new StateBehaviour[0]);
    }
}
