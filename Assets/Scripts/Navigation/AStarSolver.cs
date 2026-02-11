using UnityEngine;

[CreateAssetMenu(fileName = "new A* Solver", menuName = "Navigation/A* Solver")]
public class AStarSolver : NavSolver
{
    [Header("A*")]
    public HueristicType hueristicSolver;

    public override void GeneratePath(Vector3 Origin, Vector3 Destination, out NavPath navPath)
    {
        throw new System.NotImplementedException();
    }
}

public enum HueristicType
{
    MANHATTAN,
}
