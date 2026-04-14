using UnityEngine;

public class GoToFood : VehicleState
{
    [SerializeField] private string foodTag;
    [SerializeField] private float foodSaturation = 0.5f;
    [SerializeField] private float foodMaturation = 0.2f;
    private ViewConeSense viewCone;
    private Transform targetFood;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        viewCone = vehicle.GetComponent<ViewConeSense>();
    }
    public override void Enter()
    {
        base.Enter();

        Collider[] visibleFood = viewCone.GetByTag(foodTag);

        if(visibleFood.Length <= 0)
        {
            Debug.LogWarning("Lost food in transition");
            Finish();
        }
        else
        {
            targetFood = visibleFood[0].transform;
            GoTo(targetFood.position);
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == foodTag)
    //     {
    //         if(vehicle is SurvivalAgent)
    //         {
    //             (vehicle as SurvivalAgent).hunger += foodSaturation;
    //             (vehicle as SurvivalAgent).maturity += foodMaturation;
    //         }
    //         else
    //             Debug.LogError("Vehicle is not Survival agent!!!");
            
    //         other.GetComponent<FoodScatter>().Consume();
    //         Finish();
    //     }

    // }

    protected override void OnTargetReached()
    {
        base.OnTargetReached();
        if(Vector3.Distance(targetFood.position , transform.position) > 1.5f) // food has been moved 
        {
            Debug.Log("My Food was stolen!!");
            targetFood = null;

            // Collider[] visibleFood = viewCone.GetByTag("Food");

            // if(visibleFood.Length > 0)
            // {
            //     Debug.LogWarning("other food nearby!");
            //     targetFood = visibleFood[0].transform;
            //     GoTo(targetFood.position);
            // }else
        }
        else
        {
            if(vehicle is SurvivalAgent)
            {
                (vehicle as SurvivalAgent).hunger += foodSaturation;
                (vehicle as SurvivalAgent).maturity += foodMaturation;
            }
            else
                Debug.LogError("Vehicle is not Survival agent!!!");
            
            targetFood.GetComponent<FoodScatter>().Consume();
            //Finish();
        }

        Finish();
    }
}
