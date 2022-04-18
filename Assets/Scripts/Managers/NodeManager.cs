using System.Collections;
using System.Collections.Generic;
using Networks.Road;
using UnityEngine;
using Utils;

namespace Managers
{
    public class NodeManager: IdList<Node>
    {
        private static NodeManager _singleton;
        public static NodeManager Instance
        {
            get { return _singleton ??= new NodeManager(); }
        }

        public static int? GetClosestNode(Vector2 pos)
        {
            return GetClosestNode(pos, out _);
        }
        
        public static int? GetClosestNode(Vector2 pos, out float dist)
        {
            int? closest = null;
            float closestDist = float.MaxValue;
            foreach (int i in Instance.Keys)
            {
                Node n = Instance.Get(i);
                Vector2 p = Camera.main!.WorldToScreenPoint(n.Pos);
                float d = Vector2.Distance(p, pos);
                if (d < closestDist && !n.IsFull())
                {
                    closest = i;
                    closestDist = d;
                }
            }

            dist = closestDist;
            return closest;
        }
    }
}