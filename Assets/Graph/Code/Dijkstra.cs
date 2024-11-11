using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charly.Graph
{
    public class Dijkstra : MonoBehaviour
    {
        #region References

        //The collection of all the nodes
        //Wich every node contains multiple connection
        //defines the graph 
        [SerializeField] protected List<Node> graph;

        #endregion
    }
}
