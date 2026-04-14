using UnityEngine;
using Westhouse.GPG221.AI.Agent;

namespace Westhouse.GPG221.AI.Strategy
{
    public class HuntPrey : VehicleState
    {
        [SerializeField] private string PreyTag = "Prey";
        [SerializeField] private float saturation = 0.5f;
        [SerializeField] private float consumeDistance = 1.5f;
        private ViewConeSense viewCone;
        private Transform targetPrey;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            viewCone = vehicle.GetComponent<ViewConeSense>();
        }
        public override void Enter()
        {
            base.Enter();

            Collider[] visiblePrey = viewCone.GetByTag(PreyTag);

            if(visiblePrey.Length <= 0)
            {
                Debug.LogWarning("Lost food in transition");
                Finish();
            }
            else
            {
                targetPrey = visiblePrey[0].transform;
                GoTo(targetPrey);
            }
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if(targetPrey == null) // lost prey
            {
                Finish();
                return;
            }

            if(Vector3.Distance(targetPrey.position , vehicle.position) < consumeDistance) // Can Eat
            {
                if(vehicle is SurvivalAgent)
                {
                    (vehicle as SurvivalAgent).hunger += saturation;
                }
                else
                    Debug.LogError("Vehicle is not Survival agent!!!");
                

                Object.Destroy(targetPrey.gameObject);
                Finish();
            }

        }

    }
}