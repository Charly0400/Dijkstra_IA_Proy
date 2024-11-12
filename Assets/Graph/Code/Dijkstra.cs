using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Charly.Graph
{
    public class Dijkstra : MonoBehaviour
    {
        #region RuntimeVariables

        [SerializeField] protected int SizeX = 10;
        [SerializeField] protected int SizeZ = 10;

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

        #region RuntimeMethods

        public void CreatNodes()
        {
            for (int x = 0; x < SizeX; x++)
            {
                for (int z = 0; z < SizeZ; z++)
                {
                    Vector3 nodePosition = new Vector3(x * SizeX, 0, z * SizeZ);
                }
            }

        }

        #endregion

        #region UnityMethodas

        private void OnDrawGizmos()
        {
        

        }

        #endregion
    }
}
