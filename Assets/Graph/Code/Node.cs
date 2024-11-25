using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Charly.Graph
{
    public class Node : MonoBehaviour
    {
        #region Refeerences

        //Saves up to 0 to any connection
        [SerializeField] protected List<Connection> connections;
        [SerializeField] protected Dijkstra _dijkstra;

        #endregion

        #region Variables

        [SerializeField] private int nodesSeen;
        protected bool isNodeConnectacle;

        #endregion

        #region UnityMethods

        private void Awake()
        {

        }

        private void OnDrawGizmos()
        {

            if (connections == null)
                return;
            foreach (Connection connection in connections)
            {
                if (connection.nodeA == null || connection.nodeB == null)
                    return;
                Debug.DrawLine(
                        connection.nodeA.transform.position,
                        connection.nodeB.transform.position,
                        Color.gray,
                        0.01666666f
                    );
            }
        }

        #endregion


        #region PublicMethods

        public void RayCastForAllNodes()
        {
            nodesSeen = 0;
            _dijkstra = GameObject.Find("Dijkstra(Graph)").GetComponent<Dijkstra>();
            foreach (Node node in _dijkstra.GetListOfNodes)        
            {
                RaycastHit hit;
                Vector3 direction = node.gameObject.transform.position - transform.position;
                if (Physics.Raycast(transform.position, direction, out hit, 10))
                {
                    if (hit.collider.gameObject.CompareTag("Node") /*&& isNodeConnectacle==true*/)
                    {
                        connections.Add(new Connection());
                        connections[nodesSeen].nodeA = gameObject.GetComponent<Node>();
                        connections[nodesSeen].nodeB = hit.collider.gameObject.GetComponent<Node>();
                        ++nodesSeen;
                    }
                }
            }
        }

        #endregion

        #region privateMethods

        

        #endregion

        #region SetterAndGetters

        public List<Connection> GetConnections
        {
            get { return connections; }
        }

        #endregion
    }
}