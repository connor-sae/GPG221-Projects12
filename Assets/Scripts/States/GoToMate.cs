using UnityEngine;

namespace Westhouse.GPG221.AI.Strategy
    {
    public class GoToMate : VehicleState
    {
        private SurvivalAgent sAgent;
        public override void Enter()
        {
            base.Enter();

            if(vehicle is SurvivalAgent)
                sAgent = vehicle as SurvivalAgent;
            else
                Debug.LogError("Vehicle is not Survival agent!!!");
            
            GoTo(sAgent.targetMate.transform);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

                //if lost target mate or within the mating distance next
            if(sAgent.targetMate == null || Vector3.Distance(sAgent.transform.position, transform.position) < sAgent.matingDistance)
                Finish();
            
        }
    }
}