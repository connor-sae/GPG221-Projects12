using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{

    [RequireComponent(typeof(ViewConeSense))]
    public class FleeNearbyPreditors : Behavior
    {   
        private ViewConeSense viewCone;

        protected override void Awake()
        {
            base.Awake();
            viewCone = GetComponent<ViewConeSense>();
        }
        void FixedUpdate()
        {
            Collider[] nearbyPreditors = viewCone.GetByTag("Preditor");
            if(nearbyPreditors.Length <= 0)
                return;
            

            Vector3 desiredVel = Vector3.zero;
            foreach(Collider col in nearbyPreditors)
            {
                Vector3 dir = col.transform.position - vehicle.position;
                desiredVel -= dir.normalized;
            }
            desiredVel *= vehicle.maxSpeed / nearbyPreditors.Length;
            Steer(desiredVel - vehicle.velocity);
        }
    }
}