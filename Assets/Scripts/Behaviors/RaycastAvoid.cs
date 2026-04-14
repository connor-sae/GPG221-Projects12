using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{

    [RequireComponent(typeof(Feelers))]
    public class RaycastAvoid : Behavior
    {
        Feelers feelers;
        void Start()
        {
            feelers = GetComponent<Feelers>();
        }

        void FixedUpdate()
        {
            Vector3 avoid = Vector3.zero;

            if(feelers.hits.Count <= 0)
                return;
            
            foreach(Vector3 point in feelers.hits)
            {
                avoid += point;
            }
            avoid /= feelers.hits.Count;
            Vector3 desiredVel = (vehicle.position - avoid).normalized * vehicle.maxSpeed;
            
            Steer(desiredVel - vehicle.velocity);
        }

        //adds a force perpindicular to velocity direction
        // public void Turn(float torque)
        // {
        //     float x = vehicle.velocity.x / vehicle.velocity.magnitude;
        //     float y = vehicle.velocity.y / vehicle.velocity.magnitude;
        //     float theta = 90;
        //     Vector3 right = new Vector2(x * Mathf.Cos(theta) - y * Mathf.Sin(theta),
        //                                 x * Mathf.Sin(theta) + y * Mathf.Cos(theta));

        //     Steer(right * torque);
        // }
    }
}