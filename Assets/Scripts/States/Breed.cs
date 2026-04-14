using UnityEngine;

namespace Westhouse.GPG221.AI.Strategy
{
        public class Breed : VehicleState
    {
        public float breedHungerCost = 0.8f;
        public GameObject childPrefab;
        public override void Enter()
        {
            base.Enter();

            Instantiate(childPrefab, transform.position, transform.rotation);

            if(vehicle is SurvivalAgent)
                (vehicle as SurvivalAgent).hunger -= breedHungerCost;
            else
                Debug.LogError("Vehicle is not Survival agent!!!");
            
            Finish();
        }
    }
}