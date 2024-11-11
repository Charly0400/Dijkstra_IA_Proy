using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charly.Graph
{
    [System.Serializable]
    public class Connection 
    {
        //Alway two nodes
        [SerializeField] public Node nodeA;
        [SerializeField] public Node nodeB;
    }

    [System.Serializable]
    public class Route
    {
        [SerializeField] public List<Node> nodes;
    }
}

