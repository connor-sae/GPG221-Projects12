using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class MoveForward : Behavior
{
    //public float speed = 1f;

    void Update()
    {
        Steer(vehicle.forward * vehicle.maxSpeed);
    }
}
