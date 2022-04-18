using System.Collections.Generic;
using UnityEngine;

namespace Networks.Road
{

    public class Node
    {
        public Vector3 LeftEnd;
        public Vector3 RightEnd;
        public Vector3 Pos => (LeftEnd + RightEnd) / 2;
        private readonly List<int> _connectedSegments;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        public int[] ConnectedSegments => _connectedSegments.ToArray();

        public Node(Vector3 leftEnd, Vector3 rightEnd)
        {
            this.LeftEnd = leftEnd;
            this.RightEnd = rightEnd;
            this._connectedSegments = new List<int>();
            
            // Show ends
            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            right.transform.localScale *= 0.5f;
            left.transform.localScale *= 0.5f;
            right.transform.position = RightEnd;
            left.transform.position = LeftEnd;
            right.GetComponent<MeshRenderer>().material.SetColor(Color1, Color.red);
            left.GetComponent<MeshRenderer>().material.SetColor(Color1, Color.green);
        }

        public void ConnectSegment(int s)
        {
            _connectedSegments.Add(s);
        }

        public void RemoveSegment(int s)
        {
            _connectedSegments.Remove(s);
        }

        public bool IsFull()
        {
            return _connectedSegments.Count > 1;
        }
    }
}