using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Vehicle))]
public class ViewConeSense : MonoBehaviour
{

    public float senseRadius = 3f;
    public float senseAngle = 180f;
    [SerializeField] private int gizmoResolution = 10;
    private Vehicle vehicle;
    [SerializeField] private Transform viewOrigin;
    [SerializeField] private bool useLOS = true;

    private Collider[] visibleColliders;

    void Awake()
    {
        vehicle = GetComponent<Vehicle>();
    }

    void FixedUpdate()
    {
        visibleColliders = GetVisible();
    }

    [SerializeField] private LayerMask visibleMask;
    [SerializeField] private LayerMask obstructionMask;
    private Collider[] GetVisible()
    {
        Collider[] nearbyCols = Physics.OverlapSphere(viewOrigin.position, senseRadius, visibleMask);
        List<Collider> inViewCols = new();

        foreach(Collider col in nearbyCols) 
        {
            if(col.gameObject == gameObject)
                continue; //ignore if self
            
            Vector3 dir = col.transform.position - viewOrigin.position;

            if(Vector3.Angle(vehicle.forward, dir) > senseAngle) // if collider is outside sense angle from forward
                continue;

            
            float dist = Vector3.Distance(col.transform.position, viewOrigin.position);
            
            if(useLOS)
                if(Physics.Raycast(viewOrigin.position, dir, out RaycastHit hit, dist, obstructionMask))
                {
                    if(hit.collider != col) // skip if hit somthing that is not target col
                        continue; // obstructed
                }

            inViewCols.Add(col);   
        }

        return inViewCols.ToArray();
    }

    public T[] GetByType<T>() where T : Component
    {
        if(visibleColliders == null) return new T[0];

        List<T> components = new();
        foreach(Collider col in visibleColliders)
        {
            if(col.TryGetComponent(out T component))
            {
                components.Add(component);
            }
        }
        return components.ToArray();
    }

    public Collider[] GetByTag(string tag)
    {
        if(visibleColliders == null) return new Collider[0];
        
        List<Collider> colliders = new();
        foreach(Collider col in visibleColliders)
        {
            if(col.tag == tag)
            {
                colliders.Add(col);
            }
        }
        return colliders.ToArray();
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;

        //Gizmos.DrawWireSphere(transform.position, senseRadius);

        Vector3 forward = GetComponent<Vehicle>().forward;

        Vector3 lastPos = transform.position;

        for(int i = - gizmoResolution; i <= gizmoResolution ; i++)
        {
            float angle = (float)i / gizmoResolution * senseAngle;
            Vector3 pos = transform.position + AngleToVec(angle, forward) * senseRadius;
            Gizmos.DrawLine(lastPos, pos);
            lastPos = pos;
        }

        Gizmos.DrawLine(lastPos, transform.position);

    
        Gizmos.color = Color.green;

        if(visibleColliders != null)
            foreach(Collider v in visibleColliders)
            {
                Gizmos.DrawLine(viewOrigin.position, v.transform.position);
            }
        
    }

    private Vector3 AngleToVec(float angle, Vector3 forward)
    {
        float theta = angle * Mathf.Deg2Rad;
        float x = forward.x * Mathf.Cos(theta) - forward.z * Mathf.Sin(theta);
        float z = forward.x * Mathf.Sin(theta) + forward.z * Mathf.Cos(theta);
        return new Vector3(x, 0, z);
    }
}
