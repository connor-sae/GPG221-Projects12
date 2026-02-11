using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavSolver))]
public class NavSolverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NavSolver solver = (NavSolver)target;

        bool isActive = NavUtil.activeSolver == (NavSolver)target;
        

        if(GUILayout.Button(isActive ? "Solver is Active" : "Set As Active Solver"))
        {
            NavUtil.SetActiveSolver(solver);
            solver.ReRegisterAllNavNodes();
        }

        if(GUILayout.Button("Re-Register All Nodes with solver"))
        {
            solver.ReRegisterAllNavNodes();
        }
    }
}

[CustomEditor(typeof(AStarSolver))]
public class AStarSolverEditor : NavSolverEditor {}
