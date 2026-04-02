using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Vehicle))]
public class VehicleStator : MonoBehaviour
{
    Vehicle vehicle;

    void Awake()
    {
        vehicle = GetComponent<Vehicle>();
    }

    public List<BehaviourReference> behaviourReferences;

    public void SetStateBehaviours(StateBehaviour[] stateBehaviours)
    {
        List<WeightedBehavior> newBehaviours = new();
        foreach(StateBehaviour SB in stateBehaviours)
        {
            BehaviourReference reference = null;
            foreach(BehaviourReference BR in behaviourReferences) // ensure it exists
            {
                if(BR.name == SB.name) // it Exists!!
                {
                    reference = BR;
                    break;
                }
            }

            if(reference == null)
            {
                Debug.LogError($"The Requested state Behaviour {SB.name} Does not exist in Behaviour references!");
                continue;
            }

            WeightedBehavior WB = new WeightedBehavior(reference.behaviour, SB.weight);
            newBehaviours.Add(WB);
        }
        vehicle.behaviors = newBehaviours.ToArray();
    }
}

[System.Serializable]
public class StateBehaviour
{
    public string name;
    [Range(0, 1)]
    public float weight;
}

[System.Serializable]
public class BehaviourReference
{
    public string name;
    public Behavior behaviour;
}
