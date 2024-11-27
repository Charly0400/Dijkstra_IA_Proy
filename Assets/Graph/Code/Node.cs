using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
        [SerializeField] public bool isNodeInstanciable = true;

        [SerializeField] protected Material defaultNodeMaterial;
        [SerializeField] protected Material invalidNodeMaterial;

        [SerializeField, HideInInspector] protected MeshRenderer _meshRenderer;
        protected Vector3[] listaDeDirecciones = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

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
            RaycastHit hit;
            Vector3 direction = new Vector3();
           
            for (int i = 0; i < listaDeDirecciones.Length; i++) {
                direction = listaDeDirecciones[i];
                if (Physics.Raycast(this.transform.position, direction, out hit, 10)) {
                    Debug.Log(this + " " +  this.transform.gameObject.name);
                    if (hit.collider.gameObject.CompareTag("Node") & hit.transform.gameObject.GetComponent<Node>().isNodeInstanciable == true) {
                        Connection newConecction = new Connection();
                        newConecction.nodeA = hit.transform.gameObject.GetComponent<Node>();
                        newConecction.nodeB = this;
                        //connections[nodesSeen].nodeA = gameObject.GetComponent<Node>();
                        //connections[nodesSeen].nodeB = hit.collider.gameObject.GetComponent<Node>();
                        //++nodesSeen;
                    }
                }
            }
            //foreach (Node node in _dijkstra.GetListOfNodes)        
            //{
            //    RaycastHit hit;
            //    Vector3 direction = node.gameObject.transform.position - transform.position;
            //    if (Physics.Raycast(transform.position, direction, out hit, 10))
            //    {
            //        Debug.Log(hit.transform.gameObject.GetComponent<Node>().isNodeInstanciable + hit.transform.gameObject.name);
            //        if (hit.collider.gameObject.CompareTag("Node") & isNodeInstanciable == true)
            //        {
            //            connections.Add(new Connection());
            //            connections[nodesSeen].nodeA = gameObject.GetComponent<Node>();
            //            connections[nodesSeen].nodeB = hit.collider.gameObject.GetComponent<Node>();
            //            ++nodesSeen;
            //        }
            //    }
            //}
        }

        public void ValidNode() 
        {
            Collider[] colliderHit = Physics.OverlapSphere(this.transform.position, this.transform.localScale.x /2);
            foreach (Collider collider in colliderHit) 
            {
                if (collider.gameObject.CompareTag("Object")) 
                {
                    isNodeInstanciable = false;
                    _meshRenderer = GetComponent<MeshRenderer>();
                    _meshRenderer.material = invalidNodeMaterial;
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