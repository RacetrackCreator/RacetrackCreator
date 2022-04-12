using System.Linq;
using Managers;
using UnityEngine;
using Utils;

namespace Networks.Road
{
    
    public class Segment
    {
        private const int BezierPoints = 10;
        public readonly int StartId;
        public readonly int EndId;
        
        private Node Start => NodeManager.Instance.Get(StartId);
        private Node End => NodeManager.Instance.Get(EndId);
        public Vector3 Pos => Start.Pos;

        public readonly Vector3 Control;

        private GameObject _segmentObject = null;

        public GameObject SegmentObject => _segmentObject;

        public Segment(int startId, int endId, Vector3 control)
        {
            this.StartId = startId;
            this.EndId = endId;
            this.Control = control;
        }

        public Mesh BuildMesh()
        {
            Mesh m = new Mesh();
            Vector3 startLeftOff = Start.LeftEnd - Start.Pos;
            Vector3 endLeftOff = End.LeftEnd - End.Pos;
            Vector3 leftOff = (startLeftOff + endLeftOff) / 2;
            Bezier left = new Bezier(Start.LeftEnd - Pos, leftOff + Control - Pos, End.LeftEnd - Pos);
            
            Vector3 startRightOff = Start.RightEnd - Start.Pos;
            Vector3 endRightOff = End.RightEnd - End.Pos;
            Vector3 rightOff = (startRightOff + endRightOff) / 2;
            Bezier right = new Bezier(Start.RightEnd - Pos, rightOff + Control - Pos, End.RightEnd - Pos);

            int[] triangles = new int[(BezierPoints - 1) * 6];
            Vector3[] vertices = new Vector3[BezierPoints * 2];
            
            for (int i = 0, trianglePos=0; i < BezierPoints; i++)
            {
                float t = i / ((float) BezierPoints - 1);
                int ii = i + BezierPoints;
                vertices[i] = left.GetIntermediate(t);
                vertices[ii] = right.GetIntermediate(t);
                if (i != 0)
                {
                    triangles[trianglePos] = i - 1;
                    trianglePos++;
                    triangles[trianglePos] = ii;
                    trianglePos++;
                    triangles[trianglePos] = i;
                    trianglePos++;
                }
                if (i != BezierPoints - 1)
                {
                    triangles[trianglePos] = i;
                    trianglePos++;
                    triangles[trianglePos] = ii;
                    trianglePos++;
                    triangles[trianglePos] = ii+1;
                    trianglePos++;
                }
                
            }
            m.vertices = vertices;
            m.triangles = triangles;
            return m;
        }


        public void CreateGameObject(Material m)
        {
            GameObject g = new GameObject();
            MeshRenderer renderer = g.AddComponent<MeshRenderer>();
            renderer.material = m;
            g.AddComponent<MeshFilter>();
            _segmentObject = g;
            UpdateGameObject();
        }

        public void UpdateGameObject()
        {
            _segmentObject.GetComponent<MeshFilter>().mesh = BuildMesh();
            _segmentObject.transform.Translate(Pos, Space.World);
        }

        public void DestroyGameObject()
        {
            Object.Destroy(_segmentObject);
            _segmentObject = null;
        }
    }
}