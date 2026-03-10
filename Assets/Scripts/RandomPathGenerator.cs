using GPG221.AI;
using UnityEngine;

public class RandomPathGenerator : MonoBehaviour
{

    [SerializeField] public FollowPath pather;
    [SerializeField] private Transform startPoint;

    void Start()
    {
        GeneratePath();
    }

    public void GeneratePath()
    {
        NavNode randomNode = null;

        while(randomNode == null || randomNode.obstructed)
            randomNode = NavUtil.activeSolver.navNodes[Random.Range(0, NavUtil.activeSolver.navNodes.Length)];

        NavUtil.activeSolver.GeneratePath(startPoint.position, randomNode.transform.position, out NavPath path);

        pather.SetPath(path);
    }
}
