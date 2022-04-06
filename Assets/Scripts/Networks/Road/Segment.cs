using System.Linq;
using Managers;
using UnityEngine;
using Utils;

namespace Networks.Road
{
    
    public class Segment
    {
        private const int BezierPoints = 10;
        private readonly int _start;
        private readonly int _end;
        
        private Node Start => NodeManager.Instance.Get(_start);


        private Node End => NodeManager.Instance.Get(_end);

        private Vector3 control;

        public Segment(int start, int end, Vector3 control)
        {
            this._start = start;
            this._end = end;
            this.control = control;
        }

        public Mesh BuildMesh()
        {
            Mesh m = new Mesh();
            Vector3 startLeftOff = Start.leftEnd - Start.pos;
            Vector3 endLeftOff = End.leftEnd - End.pos;
            Vector3 leftOff = (startLeftOff + endLeftOff) / 2;
            Bezier left = new Bezier(Start.leftEnd, leftOff + control, End.leftEnd);
            
            Vector3 startRightOff = Start.rightEnd - Start.pos;
            Vector3 endRightOff = End.rightEnd - End.pos;
            Vector3 rightOff = (startRightOff + endRightOff) / 2;
            Bezier right = new Bezier(Start.rightEnd, rightOff + control, End.rightEnd);

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
    }
}