using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{

    [RequireComponent(typeof(Vehicle))]
    public class Seek : Behavior
    {
        public Transform target;
        public float determination = 1;
        public float bias = 1;

        void Update()
        {

            Vector3 desiredVel = (target.position - transform.position).normalized * vehicle.maxSpeed;

            Vector3 steerForce = (desiredVel * bias - vehicle.velocity) * determination;

            Steer(steerForce);

        }

    }
}