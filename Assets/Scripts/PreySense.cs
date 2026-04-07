using UnityEngine;
using Anthill.AI;

[RequireComponent(typeof(SurvivalAgent))]
public class PreySense : MonoBehaviour, ISense
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
        aWorldState.Set(PreySensesEnum.bred, false);
        aWorldState.Set(PreySensesEnum.hungry, IsHungry());
        aWorldState.Set(PreySensesEnum.mature, IsMature());
        aWorldState.Set(PreySensesEnum.nearMate, NearMate());
        aWorldState.Set(PreySensesEnum.seeFood, SeeFood());
        aWorldState.Set(PreySensesEnum.seeMate, SeeMate());
        aWorldState.Set(PreySensesEnum.seePreditor, SeePreditor());
        aWorldState.Set(PreySensesEnum.starving, IsStarving());
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
    
    bool SeePreditor()
    {
        return viewCone.GetByTag("Preditor").Length > 0; // TODO: insert preditor only script
    }

    bool SeeFood()
    {
        return viewCone.GetByTag("Food").Length > 0;
    }

    bool IsStarving()
    {
        return agent.IsStarving();
    }

    #endregion

    public enum PreySensesEnum
{
	hungry = 0,
	mature = 1,
	seeMate = 2,
	nearMate = 3,
	seePreditor = 4,
	bred = 5,
	seeFood = 6,
	starving = 7
}
    
}

