
using UnityEngine;

public class Wander : Behavior
{
    //public float strength = 1;
    public float aggitation = 1;

    private float seedx;
    private float seedy;

    void Start()
    {
        seedx = Random.Range(-100f, 100f);
        seedy = Random.Range(-100f, 100);
    }

    // float bias = 0f;
    // int count = 1;
    void Update()
    {
        float xSteer = (Mathf.PerlinNoise1D(seedx + Time.time * aggitation) - 0.4618f) ;
        float ySteer = (Mathf.PerlinNoise1D(seedy + Time.time * aggitation) - 0.4618f) ;

        //float xSteer = Random.Range(-strength, strength);
        //float ySteer = Random.Range(-strength, strength);0.5
        Steer(new Vector3(xSteer, 0, ySteer).normalized * vehicle.maxSpeed);

        // bias = (bias * count + xSteer) / (count + 1);
        // count ++;
        // Debug.Log(bias);
    }
}
