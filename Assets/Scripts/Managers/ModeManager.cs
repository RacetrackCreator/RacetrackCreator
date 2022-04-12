using System;
using Networks.Road;
using UnityEngine;

namespace Managers
{
    public enum Mode
    {
        Selecting,
        NewSegment
    }
    
    public class ModeManager : MonoBehaviour
    {
        
        private GameObject _sphere;
        private Mode _mode;
        private int _nodeStart = 0;
        public Material segmentMaterial;

        public float snapDistance = 50;
        private void Start()
        {
            _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _mode = Mode.Selecting;
        }

        private void Update()
        {
            switch (_mode)
            {
                case Mode.Selecting:
                    int? closest = NodeManager.GetClosestNode(Input.mousePosition, out float d);
                    if (closest != null)
                    {
                        if (d < snapDistance)
                        {
                            _sphere.SetActive(true);
                            int a = (int) closest;
                            Vector3 pos = NodeManager.Instance.Get(a).Pos;
                            _sphere.transform.position = pos;
                            if (Input.GetMouseButtonDown(0))
                            {
                                _nodeStart = a;
                                _mode = Mode.NewSegment;
                            }
                        }
                        else
                        {
                            _sphere.SetActive(false);
                        }
                    }

                    break;
                case Mode.NewSegment:
                    int nodeStartNotNull = _nodeStart;
                    Node start = NodeManager.Instance.Get(nodeStartNotNull);
                    Segment prev = SegmentManager.Get(start.ConnectedSegments[0]);
                    Vector3 c = CalculateControlPoint(start.Pos, Input.mousePosition, prev.Control, start.LeftEnd,
                        out Vector3 end);
                    _sphere.transform.position = c;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 right = end - (start.RightEnd - start.Pos).magnitude * Vector3.Cross(end - c, new Plane(start.Pos, prev.Control, start.LeftEnd).normal).normalized;
                        Vector3 left = end + (start.LeftEnd - start.Pos).magnitude * Vector3.Cross(end - c, new Plane(start.Pos, prev.Control, start.LeftEnd).normal).normalized;
                        int endId = NodeManager.Instance.Push(new Node(left, right));
                        SegmentManager.Add(nodeStartNotNull, endId, c, segmentMaterial);
                        _nodeStart = 0;
                        _mode = Mode.Selecting;
                    }
                    break;
            }
        }

        private static Vector3 CalculateControlPoint(Vector3 start, Vector2 mouseEnd, Vector3 prevControlPoint,
            Vector3 nodeEdge, out Vector3 end)
        {
            Vector3 a = start - prevControlPoint;
            Plane pi = new Plane(start, prevControlPoint, nodeEdge);
            Ray r = Camera.main.ScreenPointToRay(mouseEnd);
            pi.Raycast(r, out float t);
            end = r.GetPoint(t);
            Vector3 n = pi.normal;
            Vector3 b = Vector3.Cross(end - start, n);
            float lambda = ((start.x - end.x) / (2 * b.x) - (start.y - end.y) / (2 * b.y)) / (a.y / b.y - a.x / b.x);
            float lambda2 = ((start.x - end.x) / (2 * b.x) - (start.z - end.z) / (2 * b.z)) / (a.z / b.z - a.x / b.x);
            return start + a * (float.IsInfinity(lambda) || float.IsNaN(lambda) ? lambda2 : lambda);
        }
    }
}