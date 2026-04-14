using Westhouse.GPG221.AI.Agent;
using Westhouse.GPG221.AI.Navigation;
using UnityEngine;

namespace Westhouse.GPG221.AI.Strategy
{
    public class SearchFor : VehicleState
    {
        public string searchTag;
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
            if(viewCone.GetByTag(searchTag).Length > 0)  // Found search target
                Finish();
        }
    }
}