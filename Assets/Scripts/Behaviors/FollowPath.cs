using System;
using Westhouse.GPG221.AI.Navigation;
using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{


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
            if(path == null || path.points == null) return;
            // at target point
            if(Vector3.Distance(path.points[targetPoint], transform.position) < pointPassThreshold)
            {
                if(targetPoint >= path.points.Length - 1) //the last point
                {
                    pathCompleteCallback?.Invoke();
                    pathCompleteCallback = null;  // prevents callback being spammed
                }else
                    targetPoint++; //target next point
            }

            //seek the target point
            Vector3 desiredVel = (path.points[targetPoint]- transform.position).normalized * vehicle.maxSpeed;
            Vector3 steerForce = desiredVel  - vehicle.velocity;
            Steer(steerForce);
        }

        void OnDrawGizmosSelected()
        {
            if(path == null || path.status != PathStatus.SOLVED) 
                return;
            Gizmos.color = Color.blue;
            for(int i = 0; i < path.points.Length - 1; i++)
            {
                Gizmos.DrawLine(path.points[i], path.points[i+1]);
            }
            Gizmos.DrawSphere(path.points[targetPoint], 0.2f);
        }

    }
}