using System;
using Networks.Road;
using UnityEngine;
using Utils;

namespace Managers
{
    public class SegmentManager
    {
        private IdList<Segment> segments = new IdList<Segment>();
        private static SegmentManager _instance;
        public static SegmentManager Instance {
            get {
                return _instance ??= new SegmentManager();
            }
        }

        public static int Add(int start, int end, Vector3 controlLeft, Vector3 controlRight, Material m)
        {
            Segment s = new Segment(start, end, controlLeft, controlRight);
            s.CreateGameObject(m);
            int id = Instance.segments.Push(s);
            s.SegmentObject.name = "Segment " + id;
            NodeManager.Instance.Get(start).ConnectSegment(id);
            NodeManager.Instance.Get(end).ConnectSegment(id);
            return id;
        }

        public static Segment Get(int i)
        {
            return Instance.segments.Get(i);
        }

        public static void Remove(int i)
        {
            Segment s = Instance.segments.Remove(i);
            s.DestroyGameObject();
            NodeManager.Instance.Get(s.StartId).RemoveSegment(i);
            NodeManager.Instance.Get(s.EndId).RemoveSegment(i);
        }
    }
}