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

        public static int Add(int start, int end, Vector3 control, Material m)
        {
            Segment s = new Segment(start, end, control);
            s.CreateGameObject(m);
            return Instance.segments.Push(s);
        }

        public static Segment Get(int i)
        {
            return Instance.segments.Get(i);
        }

        public static void Remove(int i)
        {
            Instance.segments.Remove(i).DestroyGameObject();
        }
    }
}