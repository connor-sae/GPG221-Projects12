using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;

    public WeightedBehavior[] behaviors;

    public Vector3 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    public Vector3 velocity
    {
        get
        {
            return rb.linearVelocity;
        }
        set
        {
            rb.linearVelocity = value;
        }
    }
    public Vector3 acceleration { get; protected set; }

    private Vector3 fwd;
    public Vector3 forward
    {
        get
        {
            if(fwd == Vector3.zero)
                return transform.forward;

            return fwd;
        }
        private set
        {
            fwd = value;
        }
    }

    private Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        forward = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        forward.Normalize();
    }

    protected virtual void FixedUpdate()
    {

        acceleration = GetWeightedSteer();

        if(acceleration.magnitude > maxAccel) acceleration = acceleration.normalized * maxAccel; // remap to maxAccel

        //set forward before rb velocity modified
        if(velocity != Vector3.zero)
        {
            forward = velocity.normalized;
        }

        //Generalise mass to 1
        velocity += acceleration * Time.fixedDeltaTime;

        //clamp velocity to max speed
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        //translate position by velocity
        //position += velocity * Time.fixedDeltaTime;
        //handled by rigidbody

        


    }

    private Vector3 GetWeightedSteer()
    {
        Vector3 steer = Vector3.zero;
        foreach(WeightedBehavior wb in behaviors)
        {
            steer += wb.behavior.steer * wb.weight * maxAccel; // steer proporionate to maxAccel
            //reset behaviour steer to prevent ghost steering
            wb.behavior.ClearSteer();
        }
        return steer;
    }


}

[System.Serializable]
public class WeightedBehavior
{
    public Behavior behavior;
    [Range(0, 1f)]
    public float weight;

    public WeightedBehavior(Behavior behaviour, float weight)
    {
        this.behavior = behaviour;
        this.weight = weight;
    }
}
