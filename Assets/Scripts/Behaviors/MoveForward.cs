using UnityEngine;
namespace Westhouse.GPG221.AI.Agent
{

    [RequireComponent(typeof(Vehicle))]
    public class MoveForward : Behavior
    {
        //public float speed = 1f;

        void Update()
        {
            Steer(vehicle.forward * vehicle.maxSpeed);
        }
    }
}