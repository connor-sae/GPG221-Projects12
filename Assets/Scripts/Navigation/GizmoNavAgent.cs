using Westhouse.GPG221.AI.Navigation;
using UnityEngine;


public class GizmoNavAgent : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private NavPath navPath;
    public void Generate()
    {
        NavUtil.activeSolver.GeneratePath(startPoint.position, endPoint.position, out navPath);
        //Debug.Log(navPath.status);
    }

    void FixedUpdate()
    {
        Generate();
    }



    void OnDrawGizmos()
    {
        if(navPath != null && navPath.status == PathStatus.SOLVED)
        {
            Gizmos.color = Color.yellow;
            Vector3 lastPos = startPoint.position;
            foreach(Vector3 pos in navPath.points)
            {
                Gizmos.DrawLine(lastPos, pos);
                lastPos = pos;
            }
        }
    }
}
