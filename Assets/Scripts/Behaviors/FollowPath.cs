using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPath : Behavior
{
    [SerializeField] private PathSO path;
    [SerializeField] private float pointPassThreshold = 0.5f;

    void OnEnable()
    {
        path.onPathUpdate += OnPathUpdate;
    }

    void OnDisable()
    {
        path.onPathUpdate -= OnPathUpdate;
    }

    int targetPoint = 0;

    private void FixedUpdate() 
    {
        // at target point
        if(Vector3.Distance(path.GetPoint(targetPoint), transform.position) < pointPassThreshold)
        {
            if(targetPoint < path.Get().points.Length - 1) //not the last point
                targetPoint++; //target next point
        }

        //seek the target point
        Vector3 desiredVel = (path.GetPoint(targetPoint) - transform.position).normalized * vehicle.maxSpeed;
        Vector3 steerForce = desiredVel  - vehicle.velocity;
        Steer(steerForce);
    }

    private void OnPathUpdate()
    {
        targetPoint = 0;
    }
}
