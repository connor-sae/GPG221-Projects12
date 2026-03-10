using System;
using GPG221.AI;
using UnityEngine;

public class FollowPath : Behavior, IPather
{
    [SerializeField] private float pointPassThreshold = 0.5f;
    private NavPath path;


    int targetPoint = 0;

    public NavPath GetPath()
    {
        return path;
    }

    Action pathCompleteCallback;
    public void SetPath(NavPath path, Action pathCompleteCallback)
    {
        this.path = path;
        targetPoint = 0;
        this.pathCompleteCallback = pathCompleteCallback;
    }



    private void FixedUpdate() 
    {
        if(path == null) return;
        // at target point
        if(Vector3.Distance(path.points[targetPoint], transform.position) < pointPassThreshold)
        {
            if(targetPoint >= path.points.Length - 1) //the last point
            {
                pathCompleteCallback();
            }else
                targetPoint++; //target next point
        }

        //seek the target point
        Vector3 desiredVel = (path.points[targetPoint]- transform.position).normalized * vehicle.maxSpeed;
        Vector3 steerForce = desiredVel  - vehicle.velocity;
        Steer(steerForce);
    }

}
