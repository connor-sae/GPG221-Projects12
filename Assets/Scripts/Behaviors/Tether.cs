using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : Seek
{
    public float distanceBias;

    protected override void Steer(Vector3 force)
    {
        float d = Vector3.Distance(vehicle.position, target.position);
        base.Steer(force * (d / distanceBias));
    }
}
