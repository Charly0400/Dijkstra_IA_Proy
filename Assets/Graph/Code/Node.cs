using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Charly.Graph
{
    public class Node : MonoBehaviour
    {
        //Saves up to 0 to any connection
        [SerializeField] protected List<Connection> connections;

        #region UnityMethods

        private void OnDrawGizmos()
        {
            foreach (Connection connection in connections)
            {
                Debug.DrawLine(connection.nodeA.transform.position,
                                connection.nodeB.transform.position,
                                Color.magenta, 0.016666f
                                );
            }
        }

        #endregion

        #region SetterAndGetters

        public List<Connection> GetConnections
        {
            get { return connections; }
        }

        #endregion
    }
}