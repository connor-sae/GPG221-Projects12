using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Vehicle))]
public class Behavior : MonoBehaviour
{
    protected Vehicle vehicle;
    public Vector3 steer {get; private set;}

    protected virtual void Awake()
    {
        vehicle = GetComponent<Vehicle>();
    }

    protected virtual void Steer(Vector3 steerForce)
    {
        steer = steerForce;
    }

    public void ClearSteer()
    {
        steer = Vector3.zero;
    }
}
