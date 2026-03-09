using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GPG221.AI
{
    
    [System.Serializable]
    public class NavPath
    {
        public PathStatus status {get; private set;}
        public Vector3[] points;
        public NavNode[] nodes;

        public NavPath()
        {
            status = PathStatus.UNSOLVED;
        }

        public void SetPathPoints(NavNode[] pathNodes, Vector3[] pathPoints)
        {
            status = PathStatus.SOLVED;
            nodes = pathNodes;
            points = pathPoints;
        }

        public void Fail(string failReason)
        {
            status = PathStatus.FAILED;
            Debug.Log("Path generation failed: " + failReason);
        }
    }
}


public enum PathStatus
{
    /// <summary>
    /// Path is not yet generated
    /// </summary>
    UNSOLVED,
    /// <summary>
    /// Path Failed to Generate
    /// </summary>
    FAILED,
    /// <summary>
    /// Path Generated Successfully
    /// </summary>
    SOLVED,
}