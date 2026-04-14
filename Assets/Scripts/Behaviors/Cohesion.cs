using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{


    [RequireComponent(typeof(SenseVehicles))]
    public class Cohesion : Behavior
    {
        SenseVehicles sense;
        protected override void Awake()
        {
            base.Awake();
            sense = GetComponent<SenseVehicles>();
        }

        Vector3 targetPos;
        void FixedUpdate()
        {
            targetPos = Vector3.zero;

            foreach(Vehicle other in sense.visibleVehicles)
            {
                targetPos += other.position;
            }

            if(sense.visibleVehicles.Length > 0)
            {
                targetPos /= sense.visibleVehicles.Length; // average forward direction of neighbors
                Vector3 desiredVel = (targetPos - vehicle.position) * vehicle.maxSpeed;
                Vector3 steerForce = desiredVel - vehicle.velocity; //desired velocity - current velocity
                Steer(steerForce);
            }

        }

        void OnDrawGizmosSelected()
        {
            if(sense != null && sense.visibleVehicles.Length > 0)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(targetPos, 0.4f);
            }
        }
    }
}