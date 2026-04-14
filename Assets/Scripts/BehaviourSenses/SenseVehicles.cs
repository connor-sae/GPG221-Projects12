using System.Collections.Generic;
using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
    {
    [RequireComponent(typeof(Vehicle))]
    public class SenseVehicles : MonoBehaviour
    {

        private Vehicle vehicle;
        public float senseRadius = 3f;
        public float senseAngle = 180f;
        [SerializeField] private int gizmoResolution = 10;

        void Awake()
        {
            vehicle = GetComponent<Vehicle>();
        }

        public Vehicle[] visibleVehicles = new Vehicle[0];

        void FixedUpdate()
        {
            visibleVehicles = GetVisible();
        }

        [SerializeField] private LayerMask vehicleMask;
        private Vehicle[] GetVisible()
        {
            Collider[] nearbyVehicleCols = Physics.OverlapSphere(transform.position, senseRadius, vehicleMask);
            List<Vehicle> nearbyVehicles = new();

            foreach(Collider col in nearbyVehicleCols) // if vehicle is within sens angle from forward
            {
                if(col.gameObject == gameObject)
                    continue; //ignore if self
                
                Vehicle other = col.GetComponent<Vehicle>();
                Vector2 dir = other.position - vehicle.position;

                if(Vector2.Angle(vehicle.forward, dir) <= senseAngle) 
                {
                    nearbyVehicles.Add(other);   
                }
            }

            return nearbyVehicles.ToArray();
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
            foreach(Vehicle v in visibleVehicles)
            {
                Gizmos.DrawLine(transform.position, v.position);
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
}