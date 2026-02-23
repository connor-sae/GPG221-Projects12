using UnityEngine;

[System.Serializable]
public class NavPath
{
    public PathStatus status {get; private set;}
    public Vector3[] points;

    public NavPath()
    {
        status = PathStatus.UNSOLVED;
    }

    public void SetPathPoints(Vector3[] pathPoints)
    {
        status = PathStatus.SOLVED;
        points = pathPoints;
    }

    public void Fail(string failReason)
    {
        status = PathStatus.FAILED;
        Debug.Log("Path generation failed: ");
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