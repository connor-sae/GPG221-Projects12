using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{


[RequireComponent(typeof(SenseVehicles))]
public class Avoidance : Behavior
{
    SenseVehicles sense;
    [SerializeField] private float hardAvoidanceRadius = 0.6f;
    [SerializeField] private float exp = 0.6f;
    protected override void Awake()
    {
        base.Awake();
        sense = GetComponent<SenseVehicles>();
    }

    void FixedUpdate()
    {
        Vector3 avoidance = Vector3.zero;

        if(sense.visibleVehicles.Length <= 0)
            return;
        
        foreach(Vehicle other in sense.visibleVehicles)
        {
            float d = Vector3.Distance(other.position, vehicle.position);
            if(d <= 0 ) continue;

            float xOffset =  exp - hardAvoidanceRadius;
            float force = vehicle.maxSpeed * exp * (1/(d + xOffset) - 1/(sense.senseRadius + xOffset)); //y = Me(1/x+e-r - 1/R+e-r)
            avoidance += (vehicle.position - other.position).normalized*force;
        }

        //avoidance /= sense.visibleVehicles.Length;
        
        Steer(avoidance);
    }
}
}