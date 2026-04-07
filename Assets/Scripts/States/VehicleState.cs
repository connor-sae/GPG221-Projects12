using UnityEngine;
using Anthill.AI;
using GPG221.AI;

public class VehicleState : AntAIState 
{
    [SerializeField] StateBehaviour[] stateBehaviours;
    protected Vehicle vehicle;
    VehicleStator stator;
    [SerializeField] private float pathUpdateInterval = 1.5f;
    private bool hasTarget;
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        if(!aGameObject.TryGetComponent(out vehicle))
            Debug.LogError("No Vehicle found on AntGameObject: " + aGameObject.name);

        if(!aGameObject.TryGetComponent(out stator))
            Debug.LogError("No Vehicle Stator found on AntGameObject: " + aGameObject.name);
    }

    public override void Enter()
    {
        base.Enter();
        timeToNextUpdate = pathUpdateInterval;
        stator.SetStateBehaviours(stateBehaviours);
    }

    float timeToNextUpdate;
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        if(hasTarget)
        {
            timeToNextUpdate -= aDeltaTime;
            if(timeToNextUpdate <= 0)
            {
                UpdatePath();
                timeToNextUpdate = pathUpdateInterval;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        stator.SetStateBehaviours(new StateBehaviour[0]);
        hasTarget = false;
    }

    IPather pather;
    Transform targetPoint;
    private Vector3 targetPos;
    protected void GoTo(Vector3 targetPos)
    {
        targetPoint = null;
        this.targetPos = targetPos;
        SetInternalTarget();
    }

    protected void GoTo(Transform targetPoint)
    {
        this.targetPoint = targetPoint;
        SetInternalTarget();
    }

    private void SetInternalTarget()
    {
        if(pather == null)
            if(!vehicle.TryGetComponent(out pather))
            {
                Debug.LogError("Cannot call GoTo, No IPather found on AntGameObject: " + vehicle.name);
                return;
            }
        
        if(targetPoint != null) // if using transform update target
            targetPos = targetPoint.position;
        hasTarget = true;
        UpdatePath();
    }


    private void UpdatePath()
    {
        if(targetPoint != null) // if using transform update target
            targetPos = targetPoint.position;

        if(pather != null)
        {
            NavUtil.activeSolver.GeneratePath(vehicle.position, targetPos, out NavPath newPath);
            pather.SetPath(newPath, OnTargetReached);
        }else
            Debug.LogWarning("cannot update pather, null reference");
    }

    protected virtual void OnTargetReached()
    {
        Debug.Log(vehicle.name + " reached target destination");
        hasTarget = false;
    }
}
