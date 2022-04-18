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
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        public GameObject SegmentObject => _segmentObject;

        public Segment(int startId, int endId, Vector3 control)
        {
            this.StartId = startId;
            this.EndId = endId;
            this.Control = control;
        }

        private Vector3 calcX(Vector3 startToEnd, Vector3 endToEnd)
        {
            float[] alpha = new[]
            {
                Mathf.Deg2Rad * Vector2.Angle(new Vector2(startToEnd.y, startToEnd.z), new Vector2(endToEnd.y, endToEnd.z)),
                Mathf.Deg2Rad * Vector2.Angle(new Vector2(startToEnd.x, startToEnd.z), new Vector2(endToEnd.x, endToEnd.z)),
                Mathf.Deg2Rad * Vector2.Angle(new Vector2(startToEnd.x, startToEnd.y), new Vector2(endToEnd.x, endToEnd.y))
            };
            return new Vector3(startToEnd.x * Mathf.Tan(alpha[0] / 2),startToEnd.y * Mathf.Tan(alpha[1] / 2),startToEnd.z * Mathf.Tan(alpha[2] / 2));
        }

        public Mesh BuildMesh()
        {
            Mesh m = new Mesh();
            Vector3 leftStartToEnd = Start.LeftEnd - Start.Pos;
            Vector3 leftControl = Control + leftStartToEnd + calcX(leftStartToEnd, End.LeftEnd - End.Pos);
            
            Bezier left = new Bezier(Start.LeftEnd - Pos, leftControl - Pos, End.LeftEnd - Pos);
            
            
            Vector3 rightStartToEnd = Start.RightEnd - Start.Pos;
            Vector3 rightControl = Control + rightStartToEnd + calcX(rightStartToEnd, End.RightEnd - End.Pos);
            Bezier right = new Bezier(Start.RightEnd - Pos, rightControl - Pos, End.RightEnd - Pos);

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
            
            // Control ball
            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            right.transform.localScale *= 0.5f;
            right.transform.position = Control;
            right.GetComponent<MeshRenderer>().material.SetColor(Color1, Color.yellow);
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