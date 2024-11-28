using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Charly.Graph
{
    public class Dijkstra : MonoBehaviour
    {
        #region RuntimeVariables

        [SerializeField] protected int sizeX = 10;
        [SerializeField] protected int sizeZ = 10;
        [SerializeField] float cellSize = 1.0f;
        [SerializeField] protected GameObject prefabNodeTest;
        //protected GameObject nodesContainer;

        #endregion

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
        [SerializeField] protected List<Node> nodesContainer;
        

        #endregion

        #region RuntimeVariables

        protected Route initialRoute;
        [SerializeField] protected List<Route> allRoutes;
            //succesfullRoutes
            //truncatedRoutes
            //failledRoutes

        #endregion

        #region GUILayoutButton

        public void CalculateAllRoutes()
        {
            initialRoute = new Route();
            initialRoute.AddNode(initialNode, 0);

            allRoutes = new List<Route>();
            allRoutes.Add(initialRoute);
            //Recursive Method
            ExploreBranchTree(initialRoute, initialNode);
        }

        #endregion

        #region LocalMethods

        //Recursive Method
        protected void ExploreBranchTree(Route previousRoute, Node actualNodeToExplore)
        {
            //Are we in the destiny node
            if (actualNodeToExplore == finalNode)
            {

                //Break point for recursivity at this level Rutas nodo final
                return;
            }
            else
            {
                //validate the connections of the actual node
                foreach (Connection connectionOfTheActualNode in actualNodeToExplore.GetConnections)
                {
                    Node nextNode = connectionOfTheActualNode.RetreiveOtherNodeThan(actualNodeToExplore);

                    if (!previousRoute.ContainsNodeInRoute(nextNode)) 
                    {
                        //1) Furthe exploration in a branch of the tree
                        //Invocation to itself
                        Route newRoute = new Route(
                            previousRoute.nodes,
                            previousRoute.sumDistance
                            );
                        newRoute.AddNode(
                            nextNode,
                            connectionOfTheActualNode.ditanceBetweenNodes
                            );
                        allRoutes.Add(newRoute); //truncated route
                                                 //Invocation to itself to continue recursivity
                        ExploreBranchTree(newRoute, nextNode);
                    }
                    else
                    {
                        //2) Connection to a previously explored node in the route
                        //Break point for recursivity
                    }
                }           
            }
            //Cut the recursivity
        }

        #endregion

        #region RuntimeMethods

        public void ProbeNodes()
        {
            Vector3 startPosition = transform.position -
                                new Vector3(sizeX * cellSize, 0, sizeZ * cellSize);

            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 nodePosition = startPosition + new Vector3(x * cellSize, .5f, z * cellSize);                  
                    GameObject nodesInstance =  Instantiate(prefabNodeTest, nodePosition, Quaternion.identity);
                    nodesInstance.name = $"Node {x} {z}";
                    nodesInstance.GetComponent<Node>().ValidNode();
                    nodesInstance.transform.SetParent(this.transform);
                    if (nodesInstance.GetComponent<Node>().isNodeConnectable) {
                    graph.Add(nodesInstance.GetComponent<Node>());
                    }
                    nodesContainer.Add(nodesInstance.GetComponent<Node>());
                }
            }
        }

        public void ConnectionNodes()
        {
  
            foreach (Node node in graph) 
            {
                node.RayCastAndDistanceForAllNodes(); 
            }

        }

        public void ClearAll()
        {
            foreach (Node node in nodesContainer)
            {
                DestroyImmediate(node.gameObject);
            }
            graph.Clear();
            nodesContainer.Clear();
            allRoutes.Clear();
        }
        #endregion

        #region UnityMethods

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Vector3 startPosition = transform.position -
                                    new Vector3(sizeX * cellSize, 0, sizeZ * cellSize);

            for (int x = 0; x < sizeX; x++) {
                Vector3 start = startPosition + new Vector3(x * cellSize, 0, 0);
                Vector3 end = start + new Vector3(0, 0, sizeZ * cellSize);
                Gizmos.DrawLine(start, end);
            }

            for (int z = 0; z < sizeZ; z++) {
                Vector3 start = startPosition + new Vector3(0, 0, z * cellSize);
                Vector3 end = start + new Vector3(sizeX * cellSize + 1, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }

        #endregion

        #region GetterSetters

        public List<Node> GetListOfNodes 
        {
            get { return graph; }
        }

        public float GetCellSize 
        {
            get { return cellSize; }
        }
        //getter of the list

        #endregion
    }
}
