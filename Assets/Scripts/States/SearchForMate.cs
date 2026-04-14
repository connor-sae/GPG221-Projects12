using Westhouse.GPG221.AI.Agent;
using Westhouse.GPG221.AI.Navigation;
using UnityEngine;

namespace Westhouse.GPG221.AI.Strategy
{
    public class SearchForMate : VehicleState
    {
        public string mateTag;
        private ViewConeSense viewCone;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            viewCone = vehicle.GetComponent<ViewConeSense>();
            if(viewCone == null)
                Debug.LogError("No ViewCone on Vehicle Object, SearchFor requires it");
        }

        public override void Enter()
        {
            base.Enter();
            Wander();
        }

        void Wander()
        {
            Vector3 ranPos = NavUtil.GetRandomNode().transform.position;
            GoTo(ranPos);
        }

        protected override void OnTargetReached()
        {
            if(gameObject.activeSelf)
                Wander();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            Collider[] potentialMates = viewCone.GetByTag(mateTag);
            if(potentialMates.Length > 0)                          // Found search target
                foreach(Collider col in potentialMates)
                {
                    SurvivalAgent agent = col.GetComponent<SurvivalAgent>();
                    if(!agent.IsHungry() && agent.IsMature())              // only target if other mate is also not hungry and mature
                    {
                        (vehicle as SurvivalAgent).targetMate = agent;       // set as target mate
                        break;
                    }
                }
        }
    }
}