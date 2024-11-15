using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Charly.Graph
{
    public class Dijkstra : MonoBehaviour
    {
        #region RuntimeVariables

        [SerializeField] protected int sizeX = 10;
        [SerializeField] protected int sizeZ = 10;
        [SerializeField] float cellSize = 1.0f;
        [SerializeField] protected GameObject prefabNodeTest;

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

        #endregion

        #region RuntimeVariables

        protected Route initialRoute;
        protected List<Route> allRoutes;
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
                //Break point for recursivity at this level
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
                        //2) Connection ti apreviously explored node in the route
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
                                new Vector3(sizeX * cellSize / 2, 0, sizeZ * cellSize / 2);

            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    Vector3 nodePosition = startPosition + new Vector3(x * cellSize, 0, z * cellSize);

                    CreateNode(nodePosition);
                }
            }

        }

        void CreateNode(Vector3 position)
        {
            if (prefabNodeTest != null)
            {
                Instantiate(prefabNodeTest, position, Quaternion.identity);
            }
        }

        #endregion

        #region UnityMethods

        private void OnDrawGizmos()
        {
            
            
        }

        #endregion
    }
}
