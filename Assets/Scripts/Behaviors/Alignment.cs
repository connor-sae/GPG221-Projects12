using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{

[RequireComponent(typeof(SenseVehicles))]
public class Alignment : Behavior
{
    SenseVehicles sense;

    protected override void Awake()
    {
        sense = GetComponent<SenseVehicles>();
        base.Awake();
    }

    Vector3 alignment;

    void FixedUpdate()
    {
        alignment = Vector3.zero;

        foreach(Vehicle v in sense.visibleVehicles)
        {
            alignment += v.forward;
        }

        if(sense.visibleVehicles.Length > 0)
        {
            alignment /= sense.visibleVehicles.Length; // average forward direction of neighbors

            Vector3 steerForce = alignment * vehicle.maxSpeed - vehicle.velocity; //desired velocity - current velocity
            Steer(steerForce);
        }

    }

    void OnDrawGizmos()
    {
        if(vehicle == null)
            vehicle = GetComponent<Vehicle>();

        Gizmos.color = Color.white;
        Gizmos.DrawRay(vehicle.position, vehicle.forward*5);
        Gizmos.color = Color.blue;
        if(alignment != null)
            Gizmos.DrawRay(vehicle.position, alignment*5);
    }
}
}