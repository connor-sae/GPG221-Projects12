using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Vehicle
{

    [SerializeField] protected Transform visual;

    protected virtual void Update()
    {
        float desiredRot = Mathf.Atan2(forward.x, forward.z);
        visual.rotation = Quaternion.AngleAxis(desiredRot * Mathf.Rad2Deg, Vector3.up);
    }
}
