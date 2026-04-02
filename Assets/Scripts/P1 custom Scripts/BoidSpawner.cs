using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boidBundlex10;
    [SerializeField] private GameObject boidBundlex20;

    public void Spawn10()
    {
        Instantiate(boidBundlex10);
    }

    public void Spawn20()
    {
        Instantiate(boidBundlex20);
    }
} 
