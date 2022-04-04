using System.Linq;
using UnityEngine;
using Utils;

namespace Networks.Road
{
    
    public class Segment
    {
        private const int BEZIER_POINTS = 10;
        private Node start;
        private Node end;
        private Vector3 control;

        public Segment(Node start, Node end, Vector3 control)
        {
            this.start = start;
            this.end = end;
            this.control = control;
        }

        public Mesh BuildMesh()
        {
            Mesh m = new Mesh();
            Vector3 startLeftOff = start.leftEnd - start.pos;
            Vector3 endLeftOff = end.leftEnd - end.pos;
            Vector3 leftOff = (startLeftOff + endLeftOff) / 2;
            Bezier left = new Bezier(start.leftEnd, leftOff + control, end.leftEnd);
            
            Vector3 startRightOff = start.rightEnd - start.pos;
            Vector3 endRightOff = end.rightEnd - end.pos;
            Vector3 rightOff = (startRightOff + endRightOff) / 2;
            Bezier right = new Bezier(start.rightEnd, rightOff + control, end.rightEnd);

            int[] triangles = new int[(BEZIER_POINTS - 1) * 6];
            Vector3[] vertices = new Vector3[BEZIER_POINTS * 2];
            
            for (int i = 0, trianglePos=0; i < BEZIER_POINTS; i++)
            {
                float t = i / ((float) BEZIER_POINTS - 1);
                int ii = i + BEZIER_POINTS;
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
                if (i != BEZIER_POINTS - 1)
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