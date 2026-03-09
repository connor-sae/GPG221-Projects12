using UnityEngine;
using GPG221.AI;
using System;

[CreateAssetMenu(fileName = "PathSO", menuName = "Scriptable Objects/PathSO")]
public class PathSO : ScriptableObject
{
    private NavPath path;
    public void Set(NavPath path)
    {
        this.path = path;
        onPathUpdate();
    }

    public NavPath Get()
    {
        return path;
    }

    public Vector3 GetPoint(int index)
    {
        return path.points[index];
    }

    public Action onPathUpdate;
}
