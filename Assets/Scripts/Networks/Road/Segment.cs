using System;
using System.Linq;
using Managers;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

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

        public readonly Vector3 ControlLeft;
        public readonly Vector3 ControlRight;
        
        public Vector3 Control => (ControlLeft + ControlRight)/2;

        private GameObject _segmentObject = null;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        public GameObject SegmentObject => _segmentObject;

        public Segment(int startId, int endId, Vector3 controlLeft, Vector3 controlRight)
        {
            this.StartId = startId;
            this.EndId = endId;
            this.ControlLeft = controlLeft;
            this.ControlRight = controlRight;
        }

        private float? CalcLambda(Vector2 a, Vector2 b, Vector2 v, Vector2 u)
        {
            float bottom = (v.y * u.x - v.x * u.y);
            if (bottom == 0) return null;
            return (a.x * u.y - b.x*u.y - a.y*u.x + b.y*u.x) / bottom;
        }

        private Vector3 CalcOffsetControl(Vector3 start, Vector3 startEnd, Vector3 end, Vector3 endEnd,
            Vector3 control)
        {
           
            Vector3 v = control - start;
            Vector3 u = control - end;
            if (v.normalized == u.normalized || v.normalized == -u.normalized) return (startEnd + endEnd) / 2;
            float lambda = CalcLambda(new Vector2(startEnd.x, startEnd.y), new Vector2(endEnd.x, endEnd.y),
                new Vector2(v.x, v.y), new Vector2(u.x, u.y)) ??
                           CalcLambda(new Vector2(startEnd.x, startEnd.z), new Vector2(endEnd.x, endEnd.z),
                               new Vector2(v.x, v.z), new Vector2(u.x, u.z)) ??
                           CalcLambda(new Vector2(startEnd.y, startEnd.z), new Vector2(endEnd.y, endEnd.z),
                               new Vector2(v.y, v.z), new Vector2(u.y, u.z)) ?? throw new Exception("Cant calculate control");
            return startEnd + lambda * v;
        }

        public Mesh BuildMesh()
        {
            Mesh m = new Mesh();
            Bezier left = new Bezier(Start.LeftEnd - Pos, ControlLeft - Pos, End.LeftEnd - Pos);
            Bezier right = new Bezier(Start.RightEnd - Pos, ControlRight - Pos, End.RightEnd - Pos);
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
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            right.transform.localScale *= 0.5f;
            left.transform.localScale *= 0.5f;
            right.transform.position = ControlRight;
            left.transform.position = ControlLeft;
            right.GetComponent<MeshRenderer>().material.SetColor(Color1, Color.red);
            left.GetComponent<MeshRenderer>().material.SetColor(Color1, Color.green);
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