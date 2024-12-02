using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Charly.Graph
{
    public class Node : MonoBehaviour
    {
        #region Directions

        protected Vector3[] listaDeDirecciones = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left, 
            new Vector3(.5f, 0,.5f), new Vector3(-.5f, 0, -.5f), new Vector3(-.5f, 0, .5f), new Vector3(.5f, 0, -.5f)};

        #endregion

        #region Refeerences

        //Saves up to 0 to any connection
        [SerializeField] protected List<Connection> connections;
        protected Dijkstra _dijkstra;

        #endregion

        #region Variables

        [SerializeField] public bool isNodeConnectable = true;

        [SerializeField] protected Material defaultNodeMaterial;
        [SerializeField] protected Material invalidNodeMaterial;

        [SerializeField, HideInInspector] protected MeshRenderer _meshRenderer;

        #endregion

        #region UnityMethods

        private void Awake()
        {

        }

        private void OnDrawGizmos()
        {
            if (connections == null)
            {
                return;
            }
            foreach (Connection connection in connections)
            {
                if (connection.nodeA == null || connection.nodeB == null)
                {
                    return;
                }
                Debug.DrawLine(
                        connection.nodeA.transform.position,
                        connection.nodeB.transform.position,
                        Color.green, 0.01666666f);
            }
        }

        #endregion

        #region PublicMethods

        public void RayCastAndDistanceForAllNodes()
        {
            RaycastHit hit;
            Vector3 direction = new Vector3();
           
            for (int i = 0; i < listaDeDirecciones.Length; i++) {
                direction = listaDeDirecciones[i];
                if (Physics.Raycast(transform.position, direction, out hit, 100)) 
                {
                    if (hit.collider.gameObject.CompareTag("Node") &&
                        hit.transform.gameObject.GetComponent<Node>().isNodeConnectable == true) 
                    {
                        RaycastHit confirmHit;
                        if (Physics.Raycast(hit.transform.position, this.transform.position - hit.transform.position, out confirmHit, 100))
                        {
                            if (confirmHit.transform.gameObject.CompareTag("Node"))
                            {
                                Connection newConecction = new Connection();
                                connections.Add(newConecction);
                                newConecction.nodeA = this;
                                newConecction.nodeB = hit.transform.gameObject.GetComponent<Node>();
                                newConecction.ditanceBetweenNodes =
                                    Vector3.Distance(this.transform.position, hit.transform.position);
                            }
                        }
                    }
                } 
            }
        }

        public void ValidNode() 
        {
            Collider[] colliderHit = Physics.OverlapSphere(this.transform.position, this.transform.localScale.x /2);
            foreach (Collider collider in colliderHit) 
            {
                if (collider.gameObject.CompareTag("Object")) 
                {
                    isNodeConnectable = false;
                    _meshRenderer = GetComponent<MeshRenderer>();
                    _meshRenderer.material = invalidNodeMaterial;
                }
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