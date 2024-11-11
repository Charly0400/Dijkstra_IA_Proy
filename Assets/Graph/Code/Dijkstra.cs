using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Charly.Graph
{
    public class Dijkstra : MonoBehaviour
    {
        #region References

        //both will be obtained by calculating the leeser distance between:
            //Avatar VS all nodes
            //Goal VS all nodes
        [SerializeField] protected Node initialNode;
        [SerializeField] protected Node finalNode;

        //The collection of all the nodes
        //Wich every node contains multiple connection
        //defines the graph 
        [SerializeField] protected List<Node> graph;

        #endregion
    }
}
