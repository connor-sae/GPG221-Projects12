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

    public void SetPath(NavPath path)
    {
        this.path = path;
        targetPoint = 0;
    }

    private void FixedUpdate() 
    {
        // at target point
        if(Vector3.Distance(path.points[targetPoint], transform.position) < pointPassThreshold)
        {
            if(targetPoint < path.points.Length - 1) //not the last point
                targetPoint++; //target next point
        }

        //seek the target point
        Vector3 desiredVel = (path.points[targetPoint]- transform.position).normalized * vehicle.maxSpeed;
        Vector3 steerForce = desiredVel  - vehicle.velocity;
        Steer(steerForce);
    }

}
