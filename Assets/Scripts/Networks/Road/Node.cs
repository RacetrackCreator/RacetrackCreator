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
        
        public int[] ConnectedSegments => _connectedSegments.ToArray();

        public Node(Vector3 leftEnd, Vector3 rightEnd)
        {
            this.LeftEnd = leftEnd;
            this.RightEnd = rightEnd;
            this._connectedSegments = new List<int>();
        }

        public void ConnectSegment(int s)
        {
            _connectedSegments.Add(s);
        }

        public void RemoveSegment(int s)
        {
            _connectedSegments.Remove(s);
        }
    }
}