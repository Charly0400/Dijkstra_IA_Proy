using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Charly.Graph
{
    [CustomEditor(typeof(Dijkstra))]
    public class Dijkstra_Editor : Editor
    {
        #region RuntimeVariables

        protected Dijkstra _dijkstra;

        #endregion

        #region unityMethods

        public override void OnInspectorGUI()
        {
            if (_dijkstra == null)
            {
                _dijkstra = (Dijkstra)target;
            }
            DrawDefaultInspector();   

            if (GUILayout.Button("1) Probe Nodes"))
            {
                _dijkstra.ProbeNodes();
            }
            if (GUILayout.Button("2) Creat Graph (by connecting the nodes)"))
            {
                _dijkstra.CalculateAllRoutes();
            }
            if (GUILayout.Button("3) Calculate all routes (and the best route to destiny"))
            {

            }
            if (GUILayout.Button("Calculate All Dijkstra steps"))
            {

            }
            if (GUILayout.Button("Clean all previuos calculations"))
            {

            }
        }

        #endregion

        public Dijkstra SetDijkstra
        {
            set { _dijkstra = value; }
        }

        public Dijkstra GetDijkstra
        {
            get { return _dijkstra; }
        }

    }
}
