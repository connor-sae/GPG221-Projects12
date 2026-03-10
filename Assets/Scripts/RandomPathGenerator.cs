using System.IO;
using GPG221.AI;
using UnityEngine;

public class RandomPathGenerator : MonoBehaviour
{

    [SerializeField] public FollowPath pather;
    [SerializeField] private Transform startPoint;
    [SerializeField] private NavNode[] nodeOptions;
    public bool autoGenerate;
    [SerializeField] private int maxGenerations = 10;

    void Start()
    {
        GeneratePath();
    }

    public void SetAutoGenerate(bool on)
    {
        autoGenerate = on;
    }

    public void GeneratePath()
    {
        if(!gameObject.activeSelf) return;
        int generationCall = 0;
        if(maxGenerations < 0 ) generationCall = int.MinValue;
        NavPath path = new();        
        while(path.status != PathStatus.SOLVED && generationCall < maxGenerations)
        {
            NavNode randomNode = null;

            while(randomNode == null || randomNode.obstructed)
                randomNode = nodeOptions[Random.Range(0, nodeOptions.Length)];

            NavUtil.activeSolver.GeneratePath(startPoint.position, randomNode.transform.position, out path);
            generationCall++;
        }

        pather.SetPath(path, OnPathComplete);
    }

    private void OnPathComplete()
    {
        if(autoGenerate)
            GeneratePath();
    }
}
