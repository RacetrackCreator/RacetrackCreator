using UnityEngine;

namespace Networks.Road
{

    public class Node
    {
        public Vector3 leftEnd;
        public Vector3 rightEnd;
        public Vector3 pos => (leftEnd + rightEnd) / 2;

        public Node(Vector3 leftEnd, Vector3 rightEnd)
        {
            this.leftEnd = leftEnd;
            this.rightEnd = rightEnd;
        }
    }
}