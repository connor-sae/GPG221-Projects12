using UnityEngine;

public static class Navigation
{

    public static void StitchNodes(NavNode navNode1, NavNode navNode2, bool ignoreObstructed = true)
    {
        if (ignoreObstructed && (navNode1.obstructed || navNode2.obstructed))
            return;

        navNode1.ConnectToNode(navNode2);
        navNode2.ConnectToNode(navNode1);
    }

    public static void StitchNodes(bool ignoreObstructed = true, params NavNode[] nodes)
    {
        if (nodes.Length <= 1)
            return;

        for (int i = 0; i < nodes.Length; i++) 
        {
            for (int j = i+1; j < nodes.Length; j++)
            {
                if (ignoreObstructed && (nodes[i].obstructed || nodes[j].obstructed))
                    continue;

                nodes[i].ConnectToNode(nodes[j]);
                nodes[j].ConnectToNode(nodes[i]);
            }
        }
    }
}
