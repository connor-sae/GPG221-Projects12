using System.Drawing;
using UnityEngine;

public class NavPath
{
    public PathStatus status {get; private set;}
    public Vector3[] points;

    public void SetPathPoints(Vector3[] pathPoints)
    {
        points = pathPoints;
    }

    public void SetStatus(PathStatus pathStatus)
    {
        status = pathStatus;
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