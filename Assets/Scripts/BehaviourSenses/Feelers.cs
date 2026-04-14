using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Westhouse.GPG221.AI.Agent
{
    [RequireComponent(typeof(Vehicle))]
    public class Feelers : MonoBehaviour
    {
        public float feelerLength = 2;
        public Transform feelerOrigin;
        public float maxFeelerAngle = 30;
        public int feelerCount = 2;
        public LayerMask avoidMask;

        [Min(0.0001f)]
        public float senseRate = 20f;

        public List<Vector3> hits;


        private Vehicle vehicle;

        void Start()
        {
            vehicle = GetComponent<Vehicle>();
        }


        private float lastSenseTime = 0;
        void FixedUpdate()
        {
            Sense();
        }

        private Vector2 feelOrigin;
        private void Sense()
        {
            if(feelerOrigin == null)
                feelOrigin = transform.position;
            else
                feelOrigin = feelerOrigin.position;

            hits = new();

            for(int i = 0; i < feelerCount; i++)
            {
                float x = vehicle.forward.x;
                float z = vehicle.forward.z;
                float scale = (feelerCount - 1) * 0.5f;
                float theta = (i / scale - 1) * maxFeelerAngle;

                Vector3 dir = Quaternion.AngleAxis(theta, transform.up) * vehicle.forward;

                if (Physics.Raycast(feelerOrigin.position, dir,  out RaycastHit hit, feelerLength, avoidMask))
                {
                    hits.Add(hit.point);
                }
            }
            

        }

        private void OnDrawGizmosSelected() 
        {
            if(vehicle == null)
                vehicle = GetComponent<Vehicle>();
            for(int i = 0; i < feelerCount; i++)
            {
                float x = vehicle.forward.x;
                float z = vehicle.forward.z;
                float scale = (feelerCount - 1) * 0.5f;
                float theta = (i / scale - 1) * maxFeelerAngle;
                Vector3 dir = Quaternion.AngleAxis(theta, transform.up) * vehicle.forward;
                
                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(feelerOrigin.position, dir * feelerLength);

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