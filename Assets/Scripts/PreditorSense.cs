using UnityEngine;
using Anthill.AI;

[RequireComponent(typeof(SurvivalAgent))]
public class PreditorSense : MonoBehaviour, ISense
{
    private ViewConeSense viewCone;
    private SurvivalAgent agent;
    void Awake()
    {
        viewCone = GetComponent<ViewConeSense>();
        agent = GetComponent<SurvivalAgent>();
    }
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.Set(PreditorSenseEnum.bred, false);
        aWorldState.Set(PreditorSenseEnum.hungry, IsHungry());
        aWorldState.Set(PreditorSenseEnum.mature, IsMature());
        aWorldState.Set(PreditorSenseEnum.nearMate, NearMate());
        aWorldState.Set(PreditorSenseEnum.seeMate, SeeMate());
        aWorldState.Set(PreditorSenseEnum.seePrey, SeePrey());
    }

    #region Conditions

    bool IsHungry()
    {   
        return agent.IsHungry();
    }

    bool IsMature()
    {
        return agent.IsMature();
    }

    bool SeeMate()
    {
        return agent.targetMate != null;

    }

    bool NearMate()
    {
        
        return SeeMate() && Vector3.Distance(agent.position, agent.targetMate.position) <= agent.matingDistance;
    }

    bool SeePrey()
    {
        return viewCone.GetByTag("Prey").Length > 0;
    }

    #endregion

    public enum PreditorSenseEnum
{
	hungry = 0,
	mature = 1,
	seeMate = 2,
	nearMate = 3,
	seePrey = 4,
	bred = 5
}
    
}

