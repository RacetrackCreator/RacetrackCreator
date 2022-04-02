using UnityEngine;

namespace Networks.Road
{

    public class Node
    {
        private Vector3 leftEnd;
        private Vector3 rightEnd;
        private int id;
        public Vector3 pos => (leftEnd + rightEnd) / 2;

        public Vector3 LeftEnd => leftEnd;

        public Vector3 RightEnd => rightEnd;

        public int ID => id;
    }
}